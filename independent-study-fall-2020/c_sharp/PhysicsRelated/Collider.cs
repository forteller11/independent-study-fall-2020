using CART_457.EntitySystem;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace CART_457.PhysicsRelated
{
    public abstract class Collider
    {
        public Entity Entity { get; protected set; }

        protected Collider(Entity entity)
        {
            Entity = entity;
        }
    }
}