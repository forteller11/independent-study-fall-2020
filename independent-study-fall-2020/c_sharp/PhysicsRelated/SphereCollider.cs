using CART_457.EntitySystem;
using OpenTK.Mathematics;

namespace CART_457.PhysicsRelated
{
    public class SphereCollider : Collider
    {
        public float Radius;
        private Vector3 _offset;
        public Vector3 WorldPosition => Entity.WorldPosition + _offset * Entity.WorldScale;

        public SphereCollider(Entity entity, float radius, Vector3 offset = new Vector3()) : base(entity)
        {
            Radius = radius;
            _offset = offset;
        }

    }
}