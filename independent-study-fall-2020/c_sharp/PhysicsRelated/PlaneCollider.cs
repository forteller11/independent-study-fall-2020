using System.Numerics;
using CART_457.EntitySystem;
using Vector3 = OpenTK.Mathematics.Vector3;

namespace CART_457.PhysicsRelated
{
    public class PlaneCollider : Collider
    {
        public float LocalDistance; //along normal

        
        public Vector3 LocalNormal;
        
        public float WorldDistance
        {
            get
            {
                return LocalDistance + Vector3.Dot(WorldNormal, Entity.WorldPosition);
            }
        } //along normal
        
        public Vector3 WorldNormal => Entity.WorldRotation * LocalNormal;
        public Vector3 WorldPosition => WorldDistance * WorldNormal;

        public PlaneCollider(Entity entity, float distance, Vector3 normal) : base(entity)
        {
            LocalDistance = distance;
            LocalNormal = normal;
            Entity = entity;
            
        }
    }
}