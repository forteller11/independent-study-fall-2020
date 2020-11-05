using System.Collections.Generic;

namespace CART_457.PhysicsRelated
{
    public class ColliderGroup
    {
        private List<SphereCollider> _spheres;
        private List<PlaneCollider> _planes;

        public ColliderGroup()
        {
            _spheres = new List<SphereCollider>();
            _planes = new List<PlaneCollider>();
        }

        public void AddColliders(Collider[] colliders)
        {
            foreach (var collider in colliders)
            {
                if (collider is SphereCollider)
                    AddCollider((SphereCollider)collider);
                
                if (collider is PlaneCollider)
                    AddCollider((PlaneCollider)collider);
            }
        }
        public void AddCollider(SphereCollider collider) =>_spheres.Add(collider);
        public void AddCollider(PlaneCollider collider) => _planes.Add(collider);
        
        
        public bool Raycast(Ray ray, out List<CollisionResult> results)
        {
            results = new List<CollisionResult>(); //TODO remove potentially huge per frame GC causer --> CACHE

            bool atLeastOneCollision = false;
            for (int i = 0; i < _spheres.Count; i++)
            {
                var result = CollisionHelper.RaySphereCollision(ray, _spheres[i]);
                results.Add(result);
                if (result.Hit) atLeastOneCollision = true;
            }
            
            for (int i = 0; i < _planes.Count; i++)
            {
                var result = CollisionHelper.RayPlaneCollision(ray, _planes[i]);
                results.Add(result);
                if (result.Hit) atLeastOneCollision = true;
            }

            return atLeastOneCollision;
        }
    }
}