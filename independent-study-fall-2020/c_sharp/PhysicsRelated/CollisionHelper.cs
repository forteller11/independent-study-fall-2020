using System;
using System.Collections.Generic;
using OpenTK.Mathematics;

namespace CART_457.PhysicsRelated
{
    public static class CollisionHelper
    {
        
        public static CollisionResult RaySphereCollision(Ray ray, SphereCollider sphere)
        {
            
            //https://gdbooks.gitbooks.io/3dcollisions/content/Chapter3/raycast_sphere.html

            Vector3 rayPos = ray.Origin;
            Vector3 rayDir = ray.Direction;
            Vector3 spherePos = sphere.WorldPosition;
            float sRadius = sphere.Radius;

            Vector3 sphereToRay = spherePos - rayPos;
            // Using Length here would cause floating point error to creep in
            float sphereToRayDistSquared = sphereToRay.LengthSquared;
            float raySphere2RayProjection = Vector3.Dot(sphereToRay, rayDir);
            float b = MathF.Sqrt(sphereToRayDistSquared - (raySphere2RayProjection * raySphere2RayProjection));
            float f = MathF.Sqrt(((sRadius * sRadius) - (b * b)));

            // No collision
            if (sRadius * sRadius - sphereToRayDistSquared + raySphere2RayProjection * raySphere2RayProjection < 0f) 
            {
                return new CollisionResult()
                {
                    Hit = false,
                    Inside = false,
                    NearestOrHitPosition = (raySphere2RayProjection - f) * ray.Direction + ray.Origin,
                    HitEntity = sphere.Entity
                };
            }
            // Ray is inside
            else if (sphereToRayDistSquared < sRadius * sRadius) {
                return new CollisionResult()
                {
                    Hit = true,
                    Inside = true,
                    NearestOrHitPosition = (raySphere2RayProjection - f) * ray.Direction+ ray.Origin,
                    HitEntity = sphere.Entity
                };
            }
            // else Normal intersection
            return new CollisionResult()
            {
                Hit = true,
                Inside = false,
                NearestOrHitPosition = (raySphere2RayProjection + f) * ray.Direction+ ray.Origin,
                HitEntity = sphere.Entity
            };
            
        }
        

        
        public static CollisionResult RayPlaneCollision(Ray r, PlaneCollider p)
        {
            // Vector3 rayToPlane = p.Position - r.Origin;
            // // Returns t if collision happened, -1 if it didnt
            // float Raycast(Ray ray, PlaneCollider plane) {
            //     float nd = Vector3.Dot(r.Direction, plane.Normal);
            //     float pn = Vector3.Dot(r.Origin, plane.Normal);
            //
            //     if (nd >= 0f) {
            //         return -1;
            //     }
            //
            //     t = (plane.Position - pn) / nd;
            //
            //     if (t >= 0f) {
            //         return t;
            //     }
            //     return -1;
            // }
            throw new NotImplementedException();
        }
        
        
        
        public static CollisionResult SphereSphereCollision(SphereCollider r, SphereCollider s)
        {
            throw new NotImplementedException();
        }

   
    }
}