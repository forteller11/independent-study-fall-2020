using CART_457.EntitySystem;
using OpenTK.Mathematics;

namespace CART_457.PhysicsRelated
{
    public class SphereCollider : Collider
    {
        private float _localRadius;
        public float Radius
        {
            set => _localRadius = value;
            get => Entity.WorldScale.ComponentMean() * _localRadius;
            
        }
        public Vector3 Offset { set; private get; }
        public Vector3 WorldPosition => Entity.WorldPosition + (Offset * Entity.WorldScale);

        public SphereCollider(Entity entity, float radius, Vector3 offset = new Vector3()) : base(entity)
        {
            Radius = radius;
            Offset = offset;
        }

    }
}