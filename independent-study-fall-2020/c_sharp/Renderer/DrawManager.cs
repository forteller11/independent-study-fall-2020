using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using Indpendent_Study_Fall_2020.c_sharp.Renderer;
using Indpendent_Study_Fall_2020.Helpers;
using Indpendent_Study_Fall_2020.MaterialRelated;
using Indpendent_Study_Fall_2020.Scripts;
using OpenTK.Graphics.ES10;
using OpenTK.Graphics.OpenGL4;
using GL = OpenTK.Graphics.OpenGL4.GL;

namespace Indpendent_Study_Fall_2020.EntitySystem
{
    public class DrawManager
    {
        //deals with batching renders of gameobjects with same materials together

        public List<FBOBatch> BatchHierachies = new List<FBOBatch>();
        //todo loop through everything 

        public void SetupStaticRenderingHierarchy(FBO [] fbos, Material[] materials)
        {
            ThrowIfDuplicateTypeIDs(materials);
            ThrowIfDuplicateTypeIDs(fbos);
            
            for (int i = 0; i < fbos.Length; i++) 
                AddFBO(fbos[i]);
            
            for (int i = 0; i < materials.Length; i++)
                AddMaterial(materials[i]);
            
        }

        private void AddFBO(FBO fbo)
        {
            BatchHierachies.Add(new FBOBatch(fbo));
        }
        private void AddMaterial(Material material)
        {
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
        public void AddEntity(Entity entity) 
        {
            if (entity.MaterialType == CreateMaterials.MaterialType.ShadowMap)
                throw new DataException("Shadow material should not be set in material set, but instead via the entities BehaviorFlags enum");
            
            if (entity.MaterialType == CreateMaterials.MaterialType.None)
                return;

            bool foundMaterial = false;
            for (int fboI = 0; fboI < BatchHierachies.Count; fboI++)
            {
                FBOBatch fboBatch = BatchHierachies[fboI];
                for (int matI = 0; matI < fboBatch.MaterialBatches.Count; matI++)
                {
                    var materialBatch = fboBatch.MaterialBatches[matI];
                    switch (materialBatch.Material.Type)
                    {
                        case CreateMaterials.MaterialType.ShadowMap:
                            if (entity.HasFlags(Entity.BehaviorFlags.CreateCastShadows))
                                materialBatch.Entities.Add(entity);
                            break;
                        default:
                            if (entity.MaterialType == materialBatch.Material.Type)
                            {
                                materialBatch.Entities.Add(entity);
                                foundMaterial = true;
                            }
                            break;
                    }

                }

            }
            if (foundMaterial == false)
                throw new DataException($"Entity with material {entity.MaterialType} couldn't be found in draw manager!");
        }
        
        public void ThrowIfDuplicateTypeIDs<T>(T[] uniqueNames) where T : ITypeID
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
        


        public void RenderFrame()
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
                        
                         if (materialBatch.Material.Type != CreateMaterials.MaterialType.ShadowMap)
                            entity.SendUniformsPerObject(materialBatch.Material);
                         else 
                             UniformSender.SendTransformMatrices(entity, materialBatch.Material);

                        GL.DrawArrays(PrimitiveType.Triangles, 0, materialBatch.Material.VAO.VerticesCount);
                    }
                }
            }
            
        }
    }
}