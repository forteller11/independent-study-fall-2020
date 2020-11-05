using CART_457.EntitySystem;
using OpenTK.Mathematics;

namespace CART_457.PhysicsRelated
{
    public struct CollisionResult
    {
        public bool Hit;
        public bool Inside;
        public Vector3 HitPosition;
        public Vector3 NearestHitPosition;
        public Entity HitEntity;

        public override string ToString()
        {
            return $"Collision Result\n" +
                   $"Hit{Hit}, Inside? {Inside}\n" +
                   $"HitPosition {HitPosition}, NearestHitPosition{NearestHitPosition}";
        }
    }
}