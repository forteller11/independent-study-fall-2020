
using CART_457.EntitySystem;
using OpenTK.Mathematics;

namespace CART_457.PhysicsRelated
{
    public class PlaneCollider : Collider
    {
        public float LocalDistance { private get; set; } //along normal
        public Vector3 LocalNormal { private get; set; } //along normal
        
        public float WorldDistance
        {
            get
            {
                if (TransformRelative)
                    return LocalDistance + Vector3.Dot(WorldNormal, Entity.WorldPosition);
                return LocalDistance;
            }
        } //along normal

        public Vector3 WorldNormal
        {
            get
            {
                if (TransformRelative)
                    return Entity.WorldRotation * LocalNormal;
                return LocalNormal;
            }
        }
        
        public Vector3 WorldPosition => WorldDistance * WorldNormal;

        public PlaneCollider(Entity entity, bool isTransformRelative, float distance, Vector3 normal) : base(entity, isTransformRelative)
        {
            LocalDistance = distance;
            LocalNormal = normal;
            Entity = entity;
        }
    }
}