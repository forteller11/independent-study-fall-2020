namespace CART_457.PhysicsRelated
{
    public enum ColliderType
    {
        Sphere,
        Raycast, 
        None = default,
    }
    public interface ICollider
    {
        public ColliderType ColliderType { get; }
    }
}