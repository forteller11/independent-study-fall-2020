using OpenTK.Input;

namespace Indpendent_Study_Fall_2020.EntitySystem
{
    public class GameObjectUpdateEventArgs
    {
        public double DeltaTime;
        public KeyboardKeyEventArgs KeyboardKeyEventArgs;
        public KeyboardState KeyboardState;
    }
}