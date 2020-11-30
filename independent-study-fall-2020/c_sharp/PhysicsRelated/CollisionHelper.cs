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

            var rayDir = -ray.Direction; //todo shouldn't have to do this, source of error???
            float directionsProjection = Vector3.Dot(rayDir, plane.WorldNormal);
          
            float pn = Vector3.Dot(ray.Origin, plane.WorldNormal);

            float distAlongRay = (plane.WorldDistance - pn) / directionsProjection;

            bool rayParrallelToPlane = MathHelper.ApproximatelyEqual(directionsProjection, 0, 4);
            if (rayParrallelToPlane) //ray parralllel to plane
            {
                if (plane.WorldPosition.EqualsAprox(ray.Origin)) //inside ray
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

            // Debug.Log("PlaneHit");
            // Debug.Log($"Plane Normal {plane.WorldNormal}");
            return new CollisionResult
                {
                    Inside = MathHelper.ApproximatelyEqual(directionsProjection, 0, 2),
                    Hit = true,
                    HitEntity = plane.Entity,
                    NearestOrHitPosition = distAlongRay * rayDir + ray.Origin
                };
            

        }

        public static CollisionResult RayTriangleCollision(Ray ray, TriangleCollider triangle)
        {
            var planeCollision = RayPlaneCollision(ray, triangle.GetPlane());
            if (planeCollision.Hit == false)
                return planeCollision;
            var planeHit = planeCollision.NearestOrHitPosition;
            
            // Debug.Log(triangle.GetBarycentric(planeHit).ToStringSmall(4));
            //https://gdbooks.gitbooks.io/3dcollisions/content/Chapter4/point_in_triangle.html
            
            //set plane hit to origin of triangle points
            var p1 = (triangle.P1World - planeHit);
            var p2 = (triangle.P2World - planeHit);
            var p3 = (triangle.P3World - planeHit);

            //todo somehow winding order here is not same
            var norm1 = GetNormalOfTriangle( p1, p2);
            var norm2 = GetNormalOfTriangle( p2, p3);
            var norm3 = GetNormalOfTriangle( p3, p1);

            // Debug.Log($"p1 {p1.ToStringSmall()}");
            // Debug.Log($"p2 {p2.ToStringSmall()}");
            // Debug.Log($"p3 {p3.ToStringSmall()}");
            //
            // Debug.Log($"Norm1 {norm1.ToStringSmall()}");
            // Debug.Log($"Norm2 {norm2.ToStringSmall()}");
            // Debug.Log($"Norm3 {norm3.ToStringSmall()}");
            if (norm1.EqualsAprox(norm2) && norm2.EqualsAprox(norm3) && norm3.EqualsAprox(norm1))
            {
                // Debug.Log("Inside Triangle");
                //if inside triangle
                return planeCollision;
            }
            // Debug.Log("Outside Triangle");
            return CollisionResult.NoHitNoInfo();
            
            Vector3 GetNormalOfTriangle(Vector3 p2, Vector3 p3)
            {
                return Vector3.Normalize(Vector3.Cross(p2, p3));
            }
            
        }
        
        
        
        
        public static bool SphereSphereCollision(SphereCollider s1, SphereCollider s2)
        {
            var dist = Vector3.Distance(s1.WorldPosition, s2.WorldPosition);
            if (dist < s1.Radius + s2.Radius)
            {
                return true;
            }

            return false;
        }

   
    }
}