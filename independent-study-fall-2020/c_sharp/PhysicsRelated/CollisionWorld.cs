using System.Collections.Generic;

namespace CART_457.PhysicsRelated
{
    public static class CollisionWorld
    {
        private static List<SphereCollider> _spheres = new List<SphereCollider>();
        
        public static void AddCollider(SphereCollider collider)
        {
            _spheres.Add(collider);
        }

        public static bool Raycast(Ray ray, out List<CollisionResult> results)
        {
            results = new List<CollisionResult>(); //TODO remove potentially huge per frame GC causer --> CACHE

            bool atLeastOneCollision = false;
            for (int i = 0; i < _spheres.Count; i++)
            {
                var result = CollisionHelper.RaySphereCollision(ray, _spheres[i]);
                results.Add(result);
                if (result.Hit)
                    atLeastOneCollision = true;
            }

            return atLeastOneCollision;
        }
    }
}