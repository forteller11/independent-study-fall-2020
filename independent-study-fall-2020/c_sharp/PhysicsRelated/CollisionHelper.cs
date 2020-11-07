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
        

        
        // Returns t if collision happened, -1 if it didnt
        //https://gdbooks.gitbooks.io/3dcollisions/content/Chapter3/raycast_plane.html
        public static CollisionResult RayPlaneCollision(Ray ray, PlaneCollider plane)
        {

            var rayDir = -ray.Direction;
            float directionsProjection = Vector3.Dot(rayDir, plane.Normal);
          
            float pn = Vector3.Dot(ray.Origin, plane.Normal);

            float distAlongRay = (plane.Distance - pn) / directionsProjection;

            if (directionsProjection == 0) //ray parralllel to plane
            {
                if (plane.Position == ray.Origin) //inside ray
                {
                    return new CollisionResult
                    {
                        Inside = true,
                        Hit = true,
                        HitEntity = plane.Entity,
                        NearestOrHitPosition = ray.Origin
                    };
                }

                return CollisionResult.NoHitNoInfo();
                
            }
            
            if (distAlongRay < 0)
            {
                return CollisionResult.NoHitNoInfo();
            }

            return new CollisionResult
                {
                    Inside = (distAlongRay == 0),
                    Hit = true,
                    HitEntity = plane.Entity,
                    NearestOrHitPosition = distAlongRay * rayDir + ray.Origin
                };
            

        }
        
        
        
        public static CollisionResult SphereSphereCollision(SphereCollider r, SphereCollider s)
        {
            throw new NotImplementedException();
        }

   
    }
}