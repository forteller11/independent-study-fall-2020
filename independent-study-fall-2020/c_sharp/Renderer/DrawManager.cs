using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using Indpendent_Study_Fall_2020.c_sharp.Attributes;
using Indpendent_Study_Fall_2020.c_sharp.Renderer;
using Indpendent_Study_Fall_2020.Helpers;
using Indpendent_Study_Fall_2020.MaterialRelated;
using Indpendent_Study_Fall_2020.Scripts;
using Indpendent_Study_Fall_2020.Scripts.Materials;
using OpenTK;
using OpenTK.Graphics.ES10;
using OpenTK.Graphics.OpenGL4;
using ClearBufferMask = OpenTK.Graphics.OpenGL4.ClearBufferMask;
using EnableCap = OpenTK.Graphics.OpenGL4.EnableCap;
using GL = OpenTK.Graphics.OpenGL4.GL;
using TextureUnit = OpenTK.Graphics.OpenGL4.TextureUnit;

namespace Indpendent_Study_Fall_2020.EntitySystem
{
    public static class DrawManager
    {
        public static Size TKWindowSize = new Size(100, 2000);
        
        public static List<FBOBatch> BatchHierachies = new List<FBOBatch>();
        public static List<Material> PostProcessingMaterials;
        
        public static FBO FBOToDebugDraw; //where -1 == default buffer no blit
        

        public static void Init(TKWindow window)
        {
            TKWindowSize = window.Size;
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            GL.BlendEquation(BlendEquationMode.FuncAdd);
            GL.ClearColor(0f,0f,0f,1f);
        }

        public static void SetupStaticRenderingHierarchy(FBO [] fbos)
        {
            #region error checking
            ThrowIfDuplicateTypeIDs(fbos);
            for (int i = 0; i < fbos.Length; i++)
                if (fbos[i].ID == FboSetup.FBOID.PostProcessing)
                    throw new DataException("fbos can't be of type post-processing!");

            #endregion
            
            for (int i = 0; i < fbos.Length; i++) 
                AddFBO(fbos[i]);

            SetupMaterialsUsingReflection();
        }
        
        public static void SetupMaterialsUsingReflection()
        {
            var materials = typeof(MaterialSetup).GetFields();
            foreach (FieldInfo fieldInfo in materials)
            {
                if (fieldInfo.GetValue(null) is Material == false)
                    continue;
                
                Material material  = (Material) fieldInfo.GetValue(null);

                bool atLeastOneIncludeAttribute = false;
                if (Attribute.IsDefined(fieldInfo, typeof(IncludeInDrawLoop)))
                {
                    AddMaterialToMainDrawLoop(material);
                    atLeastOneIncludeAttribute = true;
                }

                if (Attribute.IsDefined(fieldInfo, typeof(IncludeInPostFX)))
                {
                    PostProcessingMaterials.Insert(0, material);
                    atLeastOneIncludeAttribute = true;
                }
                
                if (atLeastOneIncludeAttribute == false)
                    throw new Exception(fieldInfo.Name + $"has no include attributes, will not be included in render loop and will therefore have no effect on the game!");
            }
        }

        private static void AddFBO(FBO fbo)
        {
            BatchHierachies.Insert(0, new FBOBatch(fbo));
        }
        private static void AddMaterialToMainDrawLoop(Material material)
        {

            if (material.FBOTARGET == FboSetup.FBOID.PostProcessing)
                throw new DataException("Cannot add a post processing material to rendering hierarchy!");
            
            if (material.FBOTARGET == FboSetup.FBOID.Default)
                throw new DataException("Cannot render directly to default frame buffer!");
            
            for (int i = 0; i < BatchHierachies.Count; i++)
            {
                FBOBatch fboBatch = BatchHierachies[i];
                if (fboBatch.FBO.ID == material.FBOTARGET)
                {
                    fboBatch.MaterialBatches.Add(new MaterialBatch(material));
                    return;
                }
            }
        }

        //add entity to appropriate material as sepcefied by material type, and if has createcastshadows flag, add to shadowMap material as well
        public static void AddEntity(Entity entity) 
        {

            if (entity.Materials == null)
                return;
            
            if (entity.ContainsMaterial(MaterialSetup.PostProcessing))
                throw new DataException($"Entity ${entity.GetType().Name} has material type {MaterialSetup.PostProcessing}, which is invalid!");

            int expectedFoundMaterials = entity.Materials.Length;
            int foundMaterials = 0;
            
            for (int fboI = 0; fboI < BatchHierachies.Count; fboI++)
            {
                FBOBatch fboBatch = BatchHierachies[fboI];
                for (int matI = 0; matI < fboBatch.MaterialBatches.Count; matI++)
                {
                    var materialBatch = fboBatch.MaterialBatches[matI];

                    if (entity.ContainsMaterial(materialBatch.Material))
                    { 
                        materialBatch.Entities.Add(entity);
                        foundMaterials++;
                    }
                }
            }
            
            if (foundMaterials != expectedFoundMaterials)
                throw new DataException($"Entity with materials {Debug.GraphList(new List<Material>())} couldn't be found in draw manager!");
        }

        public static void ThrowIfDuplicateTypeIDs<T>(T[] uniqueNames) where T : ITypeID
        {
            for (int i = 0; i < uniqueNames.Length; i++)
            {
                int identicalNames = 0;
                for (int j = 0; j < uniqueNames.Length; j++)
                {
                    if (uniqueNames[i].GetTypeID() == uniqueNames[j].GetTypeID())
                        identicalNames++;
                }

                if (identicalNames != 1)
                    throw new DataException($"There are multiple {uniqueNames[i].GetType().Name} with the same name!");
            }
        }
        


        public static void RenderFrame()
        {
            for (int fboIndex = 0; fboIndex < BatchHierachies.Count; fboIndex++)
            {
                FBOBatch fboBatch = BatchHierachies[fboIndex];
                fboBatch.FBO.SetDrawingStates();
                
                for (int materialIndex = 0; materialIndex < fboBatch.MaterialBatches.Count; materialIndex++)
                {
                    MaterialBatch materialBatch = fboBatch.MaterialBatches[materialIndex];
                    materialBatch.Material.SetDrawingStates();

                    for (int entityIndex = 0; entityIndex < materialBatch.Entities.Count; entityIndex++)
                    {
                        Entity entity = materialBatch.Entities[entityIndex];
                        
                        entity.SendUniformsPerObject(materialBatch.Material);

                        GL.DrawArrays(PrimitiveType.Triangles, 0, materialBatch.Material.VAO.VerticesCount);
                    }
                }
                
            }

            RenderPostProcessingEffects();
            DebugFBODrawing();
        }

        static void RenderPostProcessingEffects()
        {
            // FBO.Blit(FboSetup.Main, FboSetup.Default, ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit, BlitFramebufferFilter.Nearest);
            
            FboSetup.PostProcessing.SetDrawingStates();
            for (int i = 0; i < PostProcessingMaterials.Count; i++)
            {
                PostProcessingMaterials[i].SetDrawingStates();
                GL.DrawArrays(PrimitiveType.Triangles, 0,PostProcessingMaterials[i].VAO.VerticesCount);
            }   
            // FBO.Blit(FboSetup.Main, FboSetup.Default, ClearBufferMask.ColorBufferBit, BlitFramebufferFilter.Nearest);
            FBO.Blit(FboSetup.PostProcessing, FboSetup.Default, ClearBufferMask.ColorBufferBit , BlitFramebufferFilter.Nearest);
        }

        
        private static void DebugFBODrawing()
        {
            if (FBOToDebugDraw == null)
                return;
            
            FBO.Blit(FBOToDebugDraw, FboSetup.Default, ClearBufferMask.ColorBufferBit, BlitFramebufferFilter.Linear);

        }
    }
}