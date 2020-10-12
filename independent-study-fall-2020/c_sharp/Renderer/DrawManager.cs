using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using Indpendent_Study_Fall_2020.c_sharp.Renderer;
using Indpendent_Study_Fall_2020.Helpers;
using Indpendent_Study_Fall_2020.MaterialRelated;
using Indpendent_Study_Fall_2020.Scripts;
using OpenTK.Graphics.OpenGL4;


namespace Indpendent_Study_Fall_2020.EntitySystem
{
    public class DrawManager
    {
        //deals with batching renders of gameobjects with same materials together


        
        public List<FBOBatch> RootBatches { get; private set; } = new List<FBOBatch>();

        //sort by
        //fbos
        //-->materials
        //---->gameobjects

        public void
            SetupDrawHierarchy(FBO[] fbos, Material[] materials, Entity[] entities)
        {
            ThrowIfDuplicateNames(fbos);
            ThrowIfDuplicateNames<IUniqueName>(materials);

            for (int i = 0; i < fbos.Length; i++)
                AddFBO(fbos[i]);

            for (int i = 0; i < materials.Length; i++)
                AddMaterial(materials[i]);

            for (int i = 0; i < entities.Length; i++)
                AddEntity(entities[i]);
            
        }

        public void AddFBO(FBO fbo)
        {
            RootBatches.Add(new FBOBatch(fbo));
        }
        
        public void AddMaterial(Material material)
        {
            for (int i = 0; i < RootBatches.Count; i++)
            {
                if (material.FBOName == RootBatches[i].FBO.Name)
                {
                    RootBatches[i].MaterialBatches.Add(new MaterialBatch(material));
                    return;
                }
            }
            
            throw new DataException($"There are no FBO's which match the intended material fbo of {material.FBOName}. Check for typos.");
        }

        public void AddEntity(Entity entity)
        {
            if (entity.MaterialName == CreateMaterials.MaterialName.None)
                return;
            
            for (int i = 0; i < RootBatches.Count; i++)
            {
                for (int j = 0; j < RootBatches[i].MaterialBatches.Count; j++)
                {
                    var materialBatch = RootBatches[i].MaterialBatches[j];
                    if (entity.MaterialName == materialBatch.Material.Name)
                    {
                        materialBatch.Entities.Add(entity);
                        return;
                    }
                }
            }
            
            throw new DataException($"Entity is trying to use material {entity.MaterialName} but it doesn't exist! Check for typos.");
        }
        

        public void ThrowIfDuplicateNames<T>(T[] uniqueNames) where T : IUniqueName
        {
            for (int i = 0; i < uniqueNames.Length; i++)
            {
                int identicalNames = 0;
                for (int j = 0; j < uniqueNames.Length; j++)
                {
                    if (uniqueNames[i].GetUniqueName() == uniqueNames[j].GetUniqueName())
                        identicalNames++;
                }

                if (identicalNames != 1)
                    throw new DataException($"There are multiple {uniqueNames[i].GetType().Name} with the same name!");
            }
        }


        public void RenderFrame()
        {
            for (int fboIndex = 0; fboIndex < RootBatches.Count; fboIndex++)
            {
                FBOBatch currentFboBatch = RootBatches[fboIndex];
                currentFboBatch.FBO.PrepareForDrawing();
                
                for (int matIndex = 0; matIndex < currentFboBatch.MaterialBatches.Count; matIndex++)
                {
                    MaterialBatch materialBatch = currentFboBatch.MaterialBatches[matIndex];
                    materialBatch.Material.PrepareBatchForDrawing();
                    
                    // for (int sameEntityIndex = 0; sameEntityIndex < materialBatch.SameTypeEntities.Count; sameEntityIndex++)
                    // {
                    //     SameTypeEntityBatch sameTypeEntityBatch = materialBatch.SameTypeEntities[sameEntityIndex];
                    //     sameTypeEntityBatch.SetGLStates();
                        
                        for (int entityIndex = 0; entityIndex < materialBatch.Entities.Count; entityIndex++)
                        {
                            Entity entity = materialBatch.Entities[entityIndex];
                            entity.SendUniformsPerEntityType(materialBatch.Material);
                            
                            GL.DrawArrays(PrimitiveType.Triangles, 0, materialBatch.Material.VAO.VerticesCount);
                        }
                    // }
                }
            }
//             if (VAO.UseIndices == false)
// //            {
// ////                Debug.Log("draw arrays");
                // GL.DrawArrays(PrimitiveType.Triangles, 0, VAO.VerticesCount);
//            }
//            else
//            {
//                Debug.Log("draw elements");
//                GL.DrawElements(PrimitiveType.Triangles, VAO.IndicesBuffer.Length, DrawElementsType.UnsignedInt);
//                GL.DrawElementsInstanced(PrimitiveType.Triangles,
//                    0,
//                    DrawElementsType.UnsignedInt,
//                    Indices,
//                    ref VAO.VerticesCount);
//            }

            //todo
//            GL.DrawElementsInstanced(PrimitiveType.Triangles, 3, DrawElementsType.UnsignedInt, ref Indices, int 0);
        }
}
}