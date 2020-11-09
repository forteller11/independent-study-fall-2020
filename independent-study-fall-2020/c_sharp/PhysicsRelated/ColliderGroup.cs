using System.Collections.Generic;
using OpenTK.Mathematics;

namespace CART_457.PhysicsRelated
{
    public class ColliderGroup
    {
        public List<SphereCollider> Spheres { get; private set;}
        public List<PlaneCollider> Planes { get; private set; }
        public List<TriangleCollider> Tris { get; private set; }
        
        private List<CollisionResult> CollisionsUnsorted = new List<CollisionResult>();
        // private List<CollisionResult> CollisionsSorted = new List<CollisionResult>();

        public ColliderGroup()
        {
            Spheres = new List<SphereCollider>();
            Planes = new List<PlaneCollider>();
            Tris = new List<TriangleCollider>();
        }

        public void AddColliders(Collider[] colliders)
        {
            foreach (var collider in colliders)
            {
                if (collider is SphereCollider)
                    AddCollider((SphereCollider)collider);
                
                if (collider is PlaneCollider)
                    AddCollider((PlaneCollider)collider);
                
                if (collider is TriangleCollider)
                    AddCollider((TriangleCollider)collider);
            }
        }
        public void AddCollider(SphereCollider collider)   => Spheres.Add(collider);
        public void AddCollider(PlaneCollider collider)    =>  Planes.Add(collider);
        public void AddCollider(TriangleCollider collider) =>  Tris.Add(collider);
        
        
        public bool Raycast(Ray ray, out List<CollisionResult> results, bool sortByDistanceToRay=false)
        {
            ClearCacheLists();

            bool atLeastOneCollision = false;
            for (int i = 0; i < Spheres.Count; i++)
            {
                var result = CollisionHelper.RaySphereCollision(ray, Spheres[i]);
                if (result.Hit)
                {
                    CollisionsUnsorted.Add(result);
                    atLeastOneCollision = true;
                }
            }
            
            for (int i = 0; i < Planes.Count; i++)
            {
                var result = CollisionHelper.RayPlaneCollision(ray, Planes[i]);
                if (result.Hit)
                {
                    CollisionsUnsorted.Add(result);
                    atLeastOneCollision = true;
                }
            }
            
            for (int i = 0; i < Tris.Count; i++)
            {
                var result = CollisionHelper.RayTriangleCollision(ray, Tris[i]);
                if (result.Hit)
                {
                    CollisionsUnsorted.Add(result);
                    atLeastOneCollision = true;
                }
            }

            results = CollisionsUnsorted;
            
            if (sortByDistanceToRay)
                results = SortByDistance(ray.Origin, results); //uncessary return value

            return atLeastOneCollision;
        }
        
        

        public List<CollisionResult> SortByDistance(Vector3 position, List<CollisionResult> unsorted)
        {
            for (int i = 0; i < unsorted.Count; i++)
            {
                float toSwapDistance = Vector3.Distance(unsorted[i].NearestOrHitPosition, position); //can be distance squared right?
                int toSwapIndex = i;

                float currentLowestDistance = toSwapDistance;
                int currentLowestIndex = toSwapIndex;
                for (int j = i; j < unsorted.Count; j++)
                {
                    float currentDistance = Vector3.Distance(unsorted[i].NearestOrHitPosition, position);
                    if (currentDistance < currentLowestDistance)
                    {
                        currentLowestDistance = currentDistance;
                        currentLowestIndex = j;
                    }
                }
                
                //swap
                var toSwapCache = unsorted[toSwapIndex];
                var lowestDistanceThisPassCache   = unsorted[currentLowestIndex];

                unsorted[currentLowestIndex] = toSwapCache;
                unsorted[toSwapIndex] = lowestDistanceThisPassCache;
            }

            return unsorted;
        }
        private void ClearCacheLists()
        {
            CollisionsUnsorted.Clear();
            // CollisionsSorted.Clear();
        }
    }
}