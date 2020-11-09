using CART_457.EntitySystem;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace CART_457.PhysicsRelated
{
    public abstract class Collider
    {
        public Entity Entity { get; protected set; }
        public bool TransformRelative = true;

        protected Collider(Entity entity, bool isTransformRelative)
        {
            TransformRelative = isTransformRelative;
            Entity = entity;
        }
    }
}