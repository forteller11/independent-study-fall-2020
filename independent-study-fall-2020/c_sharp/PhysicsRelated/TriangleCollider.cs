using System.Numerics;
using CART_457.EntitySystem;
using Vector3 = OpenTK.Mathematics.Vector3;

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