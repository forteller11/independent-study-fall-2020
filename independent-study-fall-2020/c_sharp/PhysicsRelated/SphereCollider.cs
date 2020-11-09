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
            get
            {
                if (TransformRelative)
                   return Entity.WorldScale.ComponentMean() * _localRadius;
                return _localRadius;
            }
            

        }
        public Vector3 Offset { set; private get; }

        public Vector3 WorldPosition
        {
            get
            {
                if (TransformRelative)
                    return Entity.WorldPosition + (Offset * Entity.WorldScale);
                return Offset;
            }
        }

        public SphereCollider( Entity entity, bool isTransformRelative, float radius, Vector3 offset) : base(entity,isTransformRelative)
        {
            Radius = radius;
            Offset = offset;
        }
        
        public SphereCollider( Entity entity, bool isTransformRelative, float radius) 
            : this(entity,isTransformRelative, radius, Vector3.Zero) { }

    }
}