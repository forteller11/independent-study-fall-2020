using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
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
        //todo loop through everything 

        public void SetupAllMaterials(params Material[] materials)
        {
            Batches = new Dictionary<string, List<GameObject>>(materials.Length);
            _materialKeys = new string [materials.Length];
            Materials = new Dictionary<string, Material> (materials.Length);
            
            for (int i = 0; i < materials.Length; i++)
            {
                if (Batches.ContainsKey(materials[i].Name))
                    throw new Exception($"There are multiple materials with the name \"{materials[i].Name}\"");
                
                Materials.Add(materials[i].Name, materials[i]);
                Batches.Add(materials[i].Name, new List<GameObject>());
                _materialKeys[i] = materials[i].Name;
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
                
                var materialForBatch = Materials[_materialKeys[i]];
                materialForBatch.PrepareBatchForDrawing();

                if (batchObjects.Count > 0)
                {
                    batchObjects[0].SendUniformsToShaderPerMaterial(); //todo this should really be like a static method

                    for (int j = 0; j < batchObjects.Count; j++)
                    {
                        batchObjects[j].SendUniformsToShaderPerObject();
                        materialForBatch.Draw();
                    }
                }

            }
        }
    }
}