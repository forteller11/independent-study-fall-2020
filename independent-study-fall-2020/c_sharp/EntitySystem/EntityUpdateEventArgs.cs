
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace CART_457.EntitySystem
{
    public class EntityUpdateEventArgs
    {
        public double DeltaTime;
        public KeyboardState KeyboardState;
        public MouseState MouseState;
        public Vector2 MouseDelta;
        public InputState InputState = new InputState();
    }
}