﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Indpendent_Study_Fall_2020.c_sharp.Renderer;
using Indpendent_Study_Fall_2020.Helpers;
using Indpendent_Study_Fall_2020.MaterialRelated;
using Indpendent_Study_Fall_2020.Scripts;
using Indpendent_Study_Fall_2020.Scripts.Materials;
using OpenTK;
using OpenTK.Graphics.ES10;
using OpenTK.Graphics.OpenGL4;
using ClearBufferMask = OpenTK.Graphics.OpenGL4.ClearBufferMask;
using GL = OpenTK.Graphics.OpenGL4.GL;
using TextureUnit = OpenTK.Graphics.OpenGL4.TextureUnit;

namespace Indpendent_Study_Fall_2020.EntitySystem
{
    public static class DrawManager
    {
        public static Size TKWindowSize;
        
        public static List<FBOBatch> BatchHierachies = new List<FBOBatch>();
        public static Material[] PostProcessingMaterials;

        private static FBO _postProcessingFBO = FBO.Custom(CreateFBOs.FBOType.PostProcessing, FramebufferAttachment.ColorAttachment0,
            Texture.EmptyRGBA(TKWindowSize.Width, TKWindowSize.Width, TextureUnit.Texture3), 
            null);

        private static int _blitOffscreenFBOsIndex = -1; //where -1 == default buffer no blit

        public static void CycleFBOBlit()
        {
            _blitOffscreenFBOsIndex++;
            if (_blitOffscreenFBOsIndex >= BatchHierachies.Count-1)
                _blitOffscreenFBOsIndex = -1;
        }

        public static void SetupStaticRenderingHierarchy(FBO [] fbos, Material[] materials, Material[] postProcessingMaterials)
        {
            ThrowIfDuplicateTypeIDs(materials);
            ThrowIfDuplicateTypeIDs(fbos);
            for (int i = 0; i < postProcessingMaterials.Length; i++)
                if (postProcessingMaterials[i].Type != MaterialFactory.MaterialType.PostProcessing)
                    throw new DataException("Material must be of type post-processing!");
            
            for (int i = 0; i < fbos.Length; i++) 
                AddFBO(fbos[i]);
            
            for (int i = 0; i < materials.Length; i++)
                AddMaterial(materials[i]);
            
            PostProcessingMaterials = postProcessingMaterials;
            
        }

        private static void AddFBO(FBO fbo)
        {
            BatchHierachies.Add(new FBOBatch(fbo));
        }
        private static void AddMaterial(Material material)
        {
            if (material.Type == MaterialFactory.MaterialType.PostProcessing)
                throw new DataException("Cannot add a post processing material to rendering hierarchy!");
            
            for (int i = 0; i < BatchHierachies.Count; i++)
            {
                FBOBatch fboBatch = BatchHierachies[i];
                if (fboBatch.FBO.Type == material.FBOType)
                {
                    fboBatch.MaterialBatches.Add(new MaterialBatch(material));
                    return;
                }
            }
        }

        //add entity to appropriate material as sepcefied by material type, and if has createcastshadows flag, add to shadowMap material as well
        public static void AddEntity(Entity entity) 
        {

            if (entity.MaterialTypes == null)
                return;
            
            if (entity.ContainsMaterial(MaterialFactory.MaterialType.PostProcessing))
                throw new DataException($"Entity ${entity.GetType().Name} has material type {MaterialFactory.MaterialType.PostProcessing}, which is invalid!");

            int expectedFoundMaterials = entity.MaterialTypes.Length;
            int foundMaterials = 0;
            
            for (int fboI = 0; fboI < BatchHierachies.Count; fboI++)
            {
                FBOBatch fboBatch = BatchHierachies[fboI];
                for (int matI = 0; matI < fboBatch.MaterialBatches.Count; matI++)
                {
                    var materialBatch = fboBatch.MaterialBatches[matI];

                    if (entity.ContainsMaterial(materialBatch.Material.Type))
                    { 
                        materialBatch.Entities.Add(entity);
                        foundMaterials++;
                    }
                }
            }
            
            if (foundMaterials != expectedFoundMaterials)
                throw new DataException($"Entity with materials {Debug.GraphList(new List<MaterialFactory.MaterialType>())} couldn't be found in draw manager!");
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

                //gen mipmaps for fbo textures if needed
                if (fboBatch.FBO.Type != CreateFBOs.FBOType.Default) //todo do i have to do this?
                {
                    GL.BindTexture(TextureTarget.Texture2D, fboBatch.FBO.TextureHandle);
                    GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
                }
            }

            RenderPostProcessingEffects();
            DebugFBODrawing();
        }

        static void RenderPostProcessingEffects()
        {
            FBO.Blit(CreateFBOs.Default, _postProcessingFBO, ClearBufferMask.ColorBufferBit, BlitFramebufferFilter.Linear);
            
            for (int i = 0; i < PostProcessingMaterials.Length; i++)
            {
                PostProcessingMaterials[i].SetDrawingStates();
                GL.DrawArrays(PrimitiveType.Triangles, 0,PostProcessingMaterials[i].VAO.VerticesCount);
            }   
            
            FBO.Blit(_postProcessingFBO, CreateFBOs.Default, ClearBufferMask.ColorBufferBit, BlitFramebufferFilter.Linear);
        }

        
        private static void DebugFBODrawing()
        {
            if (_blitOffscreenFBOsIndex == -1)
                return;

            for (int i = 0; i < BatchHierachies.Count-1; i++) //assuming last fbo is always default
            {
                FBO.Blit(BatchHierachies[i].FBO, CreateFBOs.Default, ClearBufferMask.ColorBufferBit, BlitFramebufferFilter.Linear);
            }
            
        }
    }
}