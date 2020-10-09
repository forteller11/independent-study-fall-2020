using System.Collections.Generic;
using Indpendent_Study_Fall_2020.EntitySystem;
using Indpendent_Study_Fall_2020.Helpers;
using Indpendent_Study_Fall_2020.MaterialRelated;

namespace Indpendent_Study_Fall_2020.c_sharp.Renderer
{
    public class MaterialBatch : IUniqueName
    {
        public readonly Material Material;
        public List<GameObject> GameObjects = new List<GameObject>();
        
        public MaterialBatch(Material material) => Material = material;
        public string GetUniqueName() => Material.Name;

        public void SetState()
        {
            //todo bind opengl to material, assign unfiroms etc.... 
        }
    }
}