using OpenTK.Input;

namespace Indpendent_Study_Fall_2020.EntitySystem
{
    public class GameObjectUpdateEventArgs
    {
        public readonly double DeltaTime;
//        public KeyboardKeyEventArgs KeyboardKeyEventArgs;
        public readonly KeyboardState KeyboardState;

        public GameObjectUpdateEventArgs(double deltaTime, KeyboardState keyboardState)
        {
            DeltaTime = deltaTime;
            KeyboardState = keyboardState;
        }
    }
}