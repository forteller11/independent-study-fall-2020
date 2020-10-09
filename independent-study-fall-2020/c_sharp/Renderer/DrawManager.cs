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

        public Dictionary<string, Material> Materials;
        public Dictionary<string, List<GameObject>> Batches { get; private set; }
        private string[] _materialKeys;
        
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
            Batches = new Dictionary<string, List<GameObject>>(materials.Length);
            _materialKeys = new string [materials.Length];
            Materials = new Dictionary<string, Material> (materials.Length);

            for (int i = 0; i < FBOs.Count; i++)
            {
                
            }
            for (int i = 0; i < materials.Length; i++)
            {
                if (Batches.ContainsKey(materials[i].Name))
                    throw new Exception($"There are multiple materials with the name \"{materials[i].Name}\"");
                
                Materials.Add(materials[i].Name, materials[i]);
                Batches.Add(materials[i].Name, new List<GameObject>());
                _materialKeys[i] = materials[i].Name;
            }
        }
        
        public void SetupAllFrameBuffers(params FBO[] frameBuffers)
        {
            FBOBatches = new List<FBOBatch>(frameBuffers.Length);

            for (int i = 0; i < frameBuffers.Length; i++)
                FBOBatches.Add(new FBOBatch(frameBuffers[i]));
            
            ThrowIfDuplicateNames(FBOBatches);
            
            
        }

        public void ThrowIfDuplicateNames<T>(List<T> uniqueNames) where T : IUniqueName
        {
            for (int i = 0; i < uniqueNames.Count; i++)
            {
                int identicalNames = 0;
                for (int j = 0; j < uniqueNames.Count; j++)
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
            
            if (!Batches.ContainsKey(materialName))
                throw new Exception($"You're trying to render a GameObject with \"{materialName}\" but it hasn't been setup/doesn't exist in the DrawManager! Is there a typo?");
            
            Batches[materialName].Add(gameObject);
            gameObject.Material = Materials[materialName];
        }

        public void StopUsingMaterial(GameObject gameObject, Material material)
        {
            if (!Batches.ContainsKey(material.Name))
                throw new Exception($"You're trying to render a GameObject with \"{material.Name}\" but it hasn't been setup in the DrawManager!");
            
            gameObject.Material = null;
            var batch = Batches[material.Name];
            
            for (int i = 0; i < batch.Count; i++)
            {
                if (batch[i] == gameObject)
                {
                    batch.RemoveAt(i);
                    return;
                }
            }
            
            Debug.LogWarning($"You're trying to remove a GameObject from \"{material.Name}\" material batch but it was never added!");
        }
        public void RenderFrame()
        {
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