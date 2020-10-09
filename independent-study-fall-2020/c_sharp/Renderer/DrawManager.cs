using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using Indpendent_Study_Fall_2020.c_sharp.Renderer;
using Indpendent_Study_Fall_2020.Helpers;
using Indpendent_Study_Fall_2020.MaterialRelated;
using OpenTK.Graphics.ES10;

namespace Indpendent_Study_Fall_2020.EntitySystem
{
    public class DrawManager
    {
        //deals with batching renders of gameobjects with same materials together


        public List<FBOBatch> FBOBatches { get; private set; }
        
        //sort by
        //fbos
        //-->materials
        //---->gameobjects

        public void Setup(FBO[] fbos, Material[] materials)
        {
            SetupAllFrameBuffers(fbos);
            SetupAllMaterials(materials);
        }
        public void SetupAllMaterials(params Material[] materials)
        {
            ThrowIfDuplicateNames<IUniqueName>(materials);
            
            for (int i = 0; i < FBOBatches.Count; i++)
            {
                for (int j = 0; j < materials.Length; j++)
                {
                    if (materials[j].FBOName == FBOBatches[i].GetUniqueName())
                    {
                        FBOBatches[i].MatchBatches.Add(new MaterialBatch(materials[j]));
                    }
                }
                
            }
   
        }
        
        public void SetupAllFrameBuffers(params FBO[] frameBuffers)
        {
            ThrowIfDuplicateNames(frameBuffers);
            
            FBOBatches = new List<FBOBatch>(frameBuffers.Length);
            for (int i = 0; i < frameBuffers.Length; i++)
                FBOBatches.Add(new FBOBatch(frameBuffers[i]));
            
            
        }

        public void ThrowIfDuplicateNames<T>(T [] uniqueNames) where T : IUniqueName
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


        
        public void UseMaterial(GameObject gameObject, string materialName)
        {
            if (materialName == String.Empty)
                return;

            for (int i = 0; i < FBOBatches.Count; i++)
            for (int j = 0; j < FBOBatches[i].MatchBatches.Count; j++)
            {
                MaterialBatch materialBatch = FBOBatches[i].MatchBatches[j];
                if (materialName == materialBatch.Material.Name){
                    materialBatch.GameObjects.Add(gameObject);
                    gameObject.Material = materialBatch.Material;
                    return;
                }
            }

            throw new Exception($"You're trying to render a GameObject with \"{materialName}\" but it hasn't been setup/doesn't exist in the DrawManager! Is there a typo?");

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
            //todo set state of fbobatch, then materialbatch, then draw.
            for (int i = 0; i < _materialKeys.Length; i++)
            {
                List<GameObject> batchObjects = Batches[_materialKeys[i]];
                
                Material materialForBatch = Materials[_materialKeys[i]];
                materialForBatch.PrepareBatchForDrawing();

                if (batchObjects.Count > 0)
                {
                    batchObjects[0].SendUniformsPerMaterial(); //todo this should really be like a static method

                    for (int j = 0; j < batchObjects.Count; j++)
                    {
                        batchObjects[j].SendUniformsPerObject();
                        materialForBatch.Draw();
                    }
                }

            }
        }
    }
}