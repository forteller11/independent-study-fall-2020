using System;
using OpenTK.Mathematics;

namespace CART_457.PhysicsRelated
{
    public static class CollisionHelper
    {
        
        public static CollisionResult RaySphereCollision(Ray ray, SphereCollider sphere)
        {
            
            //https://gdbooks.gitbooks.io/3dcollisions/content/Chapter3/raycast_sphere.html
            // This function will return the value of t
            // if it returns negative, no collision!

                Vector3 rPos = ray.Origin;
                Vector3 rDir = ray.Direction;
                Vector3 sPos = sphere.WorldPosition;
                float sRadius = sphere.Radius;

                Vector3 e = sPos - rPos;
                // Using Length here would cause floating point error to creep in
                float Esq = e.LengthSquared;
                float a = Vector3.Dot(e, rDir);
                float b = MathF.Sqrt(Esq - (a * a));
                float f = MathF.Sqrt(((sRadius * sRadius) - (b * b)));

                // No collision
                if (sRadius * sRadius - Esq + a * a < 0f) 
                {
                    return new CollisionResult()
                    {
                        Hit = false,
                        Inside = false,
                        NearestOrHitPosition = (a - f) * ray.Direction,
                        HitEntity = sphere.Entity
                    };
                }
                // Ray is inside
                else if (Esq < sRadius * sRadius) {
                    return new CollisionResult()
                    {
                        Hit = true,
                        Inside = true,
                        NearestOrHitPosition = (a - f) * ray.Direction,
                        HitEntity = sphere.Entity
                    };
                }
                // else Normal intersection
                return new CollisionResult()
                {
                    Hit = true,
                    Inside = false,
                    NearestOrHitPosition = (a + f) * ray.Direction,
                    HitEntity = sphere.Entity
                };
            
        }
        
        public static CollisionResult RayPlaneCollision(Ray r, PlaneCollider s)
        {
            throw new NotImplementedException();
        }
        
        public static CollisionResult SphereSphereCollision(SphereCollider r, SphereCollider s)
        {
            throw new NotImplementedException();
        }

   
    }
}