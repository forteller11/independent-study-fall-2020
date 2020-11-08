using CART_457.EntitySystem;
using OpenTK.Mathematics;

namespace CART_457.PhysicsRelated
{
    public class TriangleCollider
    {
        public Vector3 P1Local;
        public Vector3 P2Local;
        public Vector3 P3Local;
        public Entity Entity;
        
        public Vector3 P1World => LocalToWorld(P1Local);
        public Vector3 P2World => LocalToWorld(P2Local);
        public Vector3 P3World => LocalToWorld(P3Local);

        public Vector3 GetNormal()
        {
            var p1cache = P1World;
            var p2Origin = P2World - p1cache;
            var p3Origin = P3World - p1cache;
            var normal = Vector3.Cross(p2Origin, p3Origin);
            return normal;
        }

        public float GetDistance()
        {
            //raycast down to plane at origin to get distance?
            //question: ray origin at p1 or at mean between p1,p2 and p3???
            var normal = GetNormal();
            var p1World = P1World;
            var ray = new Ray(p1World, -normal);
            var plane = new PlaneCollider(Entity, 0, normal); //dont have to raycast to get pos, just distance (optimize potent)
            var result = CollisionHelper.RayPlaneCollision(ray, plane);

            var dist = Vector3.Distance(result.NearestOrHitPosition, p1World);
            return dist;
        }

        public PlaneCollider GetPlane()
        {
            return new PlaneCollider(Entity, GetDistance(), GetNormal());
        }

        public TriangleCollider(Vector3 p1, Vector3 p2, Vector3 p3, Entity entity)
        {
            P1Local = p1;
            P2Local = p2;
            P3Local = p3;
            
            Entity = entity;
        }

        Vector3 LocalToWorld(Vector3 localPosition)
        {
            var parentRot = Entity.LocalRotation;
            var localToWorldPosition = parentRot * (localPosition * Entity.WorldScale);
            return Entity.WorldPosition + localToWorldPosition;
        }
    }
}