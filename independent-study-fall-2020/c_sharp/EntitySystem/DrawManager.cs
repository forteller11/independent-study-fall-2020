using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Indpendent_Study_Fall_2020
{
    public class DrawManager
    {
        //deals with batching renders of gameobjects with same materials together

        public Dictionary<string, Dictionary<Guid, GameObject>> Batches { get; private set; }
        private string[] _materialKeys;
        //todo store material

        public void SetupAllMaterials(params Material[] materials)
        {
            Batches = new Dictionary<string, Dictionary<Guid, GameObject>>(materials.Length);
            _materialKeys = new string [materials.Length];
            
            for (int i = 0; i < materials.Length; i++)
            {
                if (Batches.ContainsKey(materials[i].Name))
                    throw new Exception($"There are multiple materials with the name \"{materials[i].Name}\"");
                
                Batches.Add(materials[i].Name, new Dictionary<Guid, GameObject>());
                _materialKeys[i] = materials[i].Name;
            }

            
            
        }
        
        public void UseMaterial(GameObject gameObject, Material material)
        {
            if (!Batches.ContainsKey(material.Name))
                throw new Exception($"You're trying to render a GameObject with \"{material.Name}\" but it hasn't been setup in the DrawManager!");
            Batches[material.Name].Add(gameObject.GUID, gameObject);
        }

        public void StopUsingMaterial(GameObject gameObject, Material material)
        {
            if (!Batches.ContainsKey(material.Name))
                throw new Exception($"You're trying to render a GameObject with \"{material.Name}\" but it hasn't been setup in the DrawManager!");
            if (!Batches[material.Name].ContainsKey(gameObject.GUID))
               Debug.LogWarning($"You're trying to remove a GameObject from \"{material.Name}\" material batch but it was never added!");

            Batches[material.Name].Remove(gameObject.GUID);
        }
        public void Draw()
        {
            for (int i = 0; i < _materialKeys.Length; i++)
            {
                //BEGIN USING MATERIAL, UPLOAD VAO HERE
                Dictionary<Guid, GameObject> batch = Batches[_materialKeys[i]];
                foreach (var gameobjectKeyValue in batch)
                {
                    batch[gameobjectKeyValue.Key].OnRender(); //upload uniforms here
                    //remder
                }
              
            }
        }
    }
}