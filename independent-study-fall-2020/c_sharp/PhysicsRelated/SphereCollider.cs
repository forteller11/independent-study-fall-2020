using CART_457.EntitySystem;

namespace CART_457.PhysicsRelated
{
    public class SphereCollider : Collider
    {
        public float Radius;

        private SphereCollider(Entity entity) : base(entity) { }

        public static SphereCollider CreateAdd(Entity entity, float radius)
        {
            var sphere = new SphereCollider(entity);
            sphere.Radius = radius;
            return sphere;
        }
        
        
        
        
    }
}