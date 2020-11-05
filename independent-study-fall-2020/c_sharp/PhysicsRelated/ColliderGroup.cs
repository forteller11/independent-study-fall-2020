using System.Collections.Generic;

namespace CART_457.PhysicsRelated
{
    public class ColliderGroup
    {
        public List<SphereCollider> Spheres { get; private set;}
        public List<PlaneCollider> Planes { get; private set; }

        public ColliderGroup()
        {
            Spheres = new List<SphereCollider>();
            Planes = new List<PlaneCollider>();
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
        public void AddCollider(SphereCollider collider) =>Spheres.Add(collider);
        public void AddCollider(PlaneCollider collider) => Planes.Add(collider);
        
        
        public bool Raycast(Ray ray, out List<CollisionResult> results)
        {
            results = new List<CollisionResult>(); //TODO remove potentially huge per frame GC causer --> CACHE

            bool atLeastOneCollision = false;
            for (int i = 0; i < Spheres.Count; i++)
            {
                var result = CollisionHelper.RaySphereCollision(ray, Spheres[i]);
                results.Add(result);
                if (result.Hit) atLeastOneCollision = true;
            }
            
            for (int i = 0; i < Planes.Count; i++)
            {
                var result = CollisionHelper.RayPlaneCollision(ray, Planes[i]);
                results.Add(result);
                if (result.Hit) atLeastOneCollision = true;
            }

            return atLeastOneCollision;
        }
    }
}