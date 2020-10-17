using OpenTK;
using OpenTK.Input;

namespace CART_457.EntitySystem
{
    public class EntityUpdateEventArgs
    {
        public double DeltaTime;
        public KeyboardState KeyboardState;
        public MouseState MouseState;
        public Vector2 MouseDelta;
    }
}