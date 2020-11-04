using OpenTK.Mathematics;

namespace CART_457.PhysicsRelated
{
    public struct RayCollider : ICollider
    {
        public ColliderType ColliderType => ColliderType.Raycast;
        public Vector3 Origin;
        public Vector3 Direction;
    }
}