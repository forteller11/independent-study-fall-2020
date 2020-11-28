using System.Collections.Generic;
using OpenTK.Mathematics;

namespace CART_457.PhysicsRelated
{
    public class ColliderGroup
    {
        public List<SphereCollider> Spheres { get; private set;}
        public List<PlaneCollider> Planes { get; private set; }
        public List<TriangleCollider> Tris { get; private set; }
        public List<MeshCollider> Meshes { get; private set; }
        
        private List<CollisionResult> Collisions = new List<CollisionResult>();
        // private List<CollisionResult> CollisionsSorted = new List<CollisionResult>();

        public ColliderGroup()
        {
            Spheres = new List<SphereCollider>();
            Planes = new List<PlaneCollider>();
            Tris = new List<TriangleCollider>();
            Meshes = new List<MeshCollider>();
        }
        
        public void AddCollider(SphereCollider collider)   => Spheres.Add(collider);
        public void AddCollider(PlaneCollider collider)    =>  Planes.Add(collider);
        public void AddCollider(TriangleCollider collider) =>  Tris.Add(collider);
        public void AddCollider(MeshCollider collider) =>  Meshes.Add(collider);
        
        
        public bool Raycast(Ray ray, out List<CollisionResult> results, bool sortByDistanceToRay=false)
        {
            ClearCacheLists();

            bool atLeastOneCollision = false;
            for (int i = 0; i < Spheres.Count; i++)
            {
                var result = CollisionHelper.RaySphereCollision(ray, Spheres[i]);
                if (result.Hit)
                {
                    Collisions.Add(result);
                    atLeastOneCollision = true;
                }
            }
            
            for (int i = 0; i < Planes.Count; i++)
            {
                var result = CollisionHelper.RayPlaneCollision(ray, Planes[i]);
                if (result.Hit)
                {
                    Collisions.Add(result);
                    atLeastOneCollision = true;
                }
            }
            
            for (int i = 0; i < Tris.Count; i++)
            {
                var result = CollisionHelper.RayTriangleCollision(ray, Tris[i]);
                if (result.Hit)
                {
                    Collisions.Add(result);
                    atLeastOneCollision = true;
                }
            }
            
            for (int i = 0; i < Meshes.Count; i++)
            {
                for (int j = 0; j < Meshes[i].Triangles.Length; j++)
                {
                    var result = CollisionHelper.RayTriangleCollision(ray, Meshes[i].Triangles[j]);
                    if (result.Hit)
                    {
                        Collisions.Add(result);
                        atLeastOneCollision = true;
                    }
                }
            }

            if (sortByDistanceToRay)
                Collisions = SortByDistance(ray.Origin, Collisions); //uncessary return value
            
            results = Collisions;

            return atLeastOneCollision;
        }
        
        

        public List<CollisionResult> SortByDistance(Vector3 position, List<CollisionResult> unsorted)
        {
            for (int i = 0; i < unsorted.Count; i++)
            {
                int toSwapIndex = i;

                float currentLowestDistance = Vector3.DistanceSquared(unsorted[i].NearestOrHitPosition, position);
                int currentLowestIndex = toSwapIndex;
                for (int j = i; j < unsorted.Count; j++)
                {
                    float currentDistance = Vector3.DistanceSquared(unsorted[j].NearestOrHitPosition, position);
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
            Collisions.Clear();
            // CollisionsSorted.Clear();
        }
    }
}