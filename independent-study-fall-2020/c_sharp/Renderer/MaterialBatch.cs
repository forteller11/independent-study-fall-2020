using System.Collections.Generic;
using CART_457.EntitySystem;
using CART_457.MaterialRelated;

namespace CART_457.Renderer
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