using OpenTK;
using OpenTK.Input;

namespace Indpendent_Study_Fall_2020.EntitySystem
{
    public class EntityUpdateEventArgs
    {
        public double DeltaTime;
//        public KeyboardKeyEventArgs KeyboardKeyEventArgs;
        public KeyboardState KeyboardState;
        public MouseState MouseState;
        public Vector2 MouseDelta;

        public EntityUpdateEventArgs(double deltaTime, KeyboardState keyboardState, MouseState mouseState, Vector2 mouseDelta)
        {
            DeltaTime = deltaTime;
            KeyboardState = keyboardState;
            MouseState = mouseState;
            MouseDelta = mouseDelta;
        }
    }
}