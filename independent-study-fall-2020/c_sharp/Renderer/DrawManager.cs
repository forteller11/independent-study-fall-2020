using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using Indpendent_Study_Fall_2020.c_sharp.Renderer;
using Indpendent_Study_Fall_2020.Helpers;
using Indpendent_Study_Fall_2020.MaterialRelated;
using OpenTK.Graphics.OpenGL4;


namespace Indpendent_Study_Fall_2020.EntitySystem
{
    public class DrawManager
    {
        //deals with batching renders of gameobjects with same materials together


        
        public List<FBOBatch> RootBatches { get; private set; }

        //sort by
        //fbos
        //-->materials
        //---->gameobjects

        public void
            SetupDrawHierarchy(FBO[] fbos,
                Material[] materials) //todo setup so creation is oop and done within classes?
        {
            ThrowIfDuplicateNames(fbos);
            ThrowIfDuplicateNames<IUniqueName>(materials);

            RenderBatches
            SetupAllFrameBuffers(fbos);
            SetupAllMaterials(materials);
        }

        public void SetupAllFrameBuffers(params FBO[] frameBuffers)
        {
            ThrowIfDuplicateNames(frameBuffers);

            FBOBatches = new List<FBOBatch>(frameBuffers.Length);
            for (int i = 0; i < frameBuffers.Length; i++)
                FBOBatches.Add(new FBOBatch(frameBuffers[i]));
        }

        public void SetupAllMaterials(params Material[] materials)
        {
            ThrowIfDuplicateNames<IUniqueName>(materials);

            //todo 
            for (int i = 0; i < FBOBatches.Count; i++)
            for (int j = 0; j < materials.Length; j++)
            {
                if (materials[j].FBOName == FBOBatches[i].GetUniqueName())
                {
                    FBOBatches[i].MaterialBatches.Add(new MaterialBatch(materials[j]));
                }
            }

        }

        public void AddEntity()
        {
            //todo go through all mats and add appropriate
        }

        public void AddFbo()
        {

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



        public void UseMaterial(Entity entity, string materialName)
        {
            if (materialName == String.Empty)
                return;

            for (int i = 0; i < FBOBatches.Count; i++)
            for (int j = 0; j < FBOBatches[i].MaterialBatches.Count; j++)
            {
                MaterialBatch materialBatch = FBOBatches[i].MaterialBatches[j];
                if (materialName == materialBatch.Material.Name)
                {
                    materialBatch.SameTypeEntities.Add(entity);
                    entity.Material = materialBatch.Material;
                    return;
                }
            }

            throw new Exception(
                $"You're trying to render a GameObject with \"{materialName}\" but it hasn't been setup/doesn't exist in the DrawManager! Is there a typo?");

        }

        // public void StopUsingMaterial(GameObject gameObject, Material material)
        // {
        //     if (!Batches.ContainsKey(material.Name))
        //         throw new Exception($"You're trying to render a GameObject with \"{material.Name}\" but it hasn't been setup in the DrawManager!");
        //     
        //     gameObject.Material = null;
        //     var batch = Batches[material.Name];
        //     
        //     for (int i = 0; i < batch.Count; i++)
        //     {
        //         if (batch[i] == gameObject)
        //         {
        //             batch.RemoveAt(i);
        //             return;
        //         }
        //     }
        //     
        //     Debug.LogWarning($"You're trying to remove a GameObject from \"{material.Name}\" material batch but it was never added!");
        // }


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
                    
                    for (int sameEntityIndex = 0; sameEntityIndex < materialBatch.SameTypeEntities.Count; sameEntityIndex++)
                    {
                        SameTypeEntityBatch sameTypeEntityBatch = materialBatch.SameTypeEntities[sameEntityIndex];
                        sameTypeEntityBatch.SetGLStates();
                        
                        for (int entityIndex = 0; entityIndex < sameTypeEntityBatch.Entities.Count; entityIndex++)
                        {
                            Entity entity = sameTypeEntityBatch.Entities[entityIndex];
                            entity.SendUniformsPerEntityType();
                            
                            GL.DrawArrays(PrimitiveType.Triangles, 0, materialBatch.Material.VAO.VerticesCount);
                        }
                    }
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