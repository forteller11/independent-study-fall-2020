using CART_457.EntitySystem;
using OpenTK.Mathematics;

namespace CART_457.PhysicsRelated
{
    public struct CollisionResult
    {
        public bool Hit;
        public bool Inside;
        public Vector3 NearestOrHitPosition;
        public Entity HitEntity;

        public override string ToString()
        {
            return $"Collision Result\n" +
                   $"Hit{Hit}, Inside? {Inside}\n" +
                   $"NearestOrHitPosition {NearestOrHitPosition}";
        }
    }
}