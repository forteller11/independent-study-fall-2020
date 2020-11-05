using System;
using OpenTK.Mathematics;

namespace CART_457.PhysicsRelated
{
    public static class CollisionHelper
    {
        
        public static CollisionResult RaySphereCollision(Ray r, SphereCollider s)
        {
            //make ray origin the origin
            Vector3 ray2SphereCenter = s.WorldPosition - r.Origin;
            float ray2SphereCenterDistance = Vector3.Distance(s.WorldPosition, r.Origin);
            
            if (ray2SphereCenterDistance < s.Radius) //if inside sphere
            {
                return new CollisionResult()
                {
                    Hit = true,
                    Inside = true,
                    HitPosition = r.Origin,
                    NearestHitPosition = r.Origin
                };
            }

            float rayProjection = -1 * Vector3.Dot(r.Direction, ray2SphereCenter);
            float centerMinusRadius = ray2SphereCenterDistance - s.Radius;
            Vector3 nearestOrHitPosition = rayProjection * r.Direction;
            
            // Debug.Log(r.ToString());
            // Debug.Log($"Ray2SphereCenter : {ray2SphereCenter}");
            // Debug.Log($"Ray2SphereCenterDist : {ray2SphereCenterDistance}");
            // Debug.Log($"projection : {rayProjection}");
            // Debug.Log($"centerMinusRadius : {centerMinusRadius}");
            
            if (rayProjection > centerMinusRadius) //if hit from outside
            {
                return new CollisionResult()
                {
                    Hit = true,
                    Inside = false,
                    HitPosition = nearestOrHitPosition,
                    NearestHitPosition = nearestOrHitPosition
                };
            }

            else
            {
                return new CollisionResult() //if not hit
                {
                    Hit = false,
                    Inside = false,
                    HitPosition = nearestOrHitPosition,
                    NearestHitPosition = nearestOrHitPosition
                };
            }
        
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