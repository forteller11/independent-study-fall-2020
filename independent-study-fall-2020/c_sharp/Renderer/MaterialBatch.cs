using System.Collections.Generic;
using Indpendent_Study_Fall_2020.EntitySystem;
using Indpendent_Study_Fall_2020.Helpers;
using Indpendent_Study_Fall_2020.MaterialRelated;

namespace Indpendent_Study_Fall_2020.c_sharp.Renderer
{
    public struct MaterialBatch
    {
        public readonly Material Material;
        public List<Entity> Entities;

        public MaterialBatch(Material material)
        {
            Material = material;
            Entities = new List<Entity>();
        }


        
    }
}