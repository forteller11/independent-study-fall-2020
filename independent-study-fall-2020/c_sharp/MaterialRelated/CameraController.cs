using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics.OpenGL4;

namespace Indpendent_Study_Fall_2020
{
    public class CameraController
    {
        private float acceleration = 1f;
        public Vector3 Position { get; private set; } = new Vector3(0,0,0);
        public void OnUpdate(double deltaTime, KeyboardState keyboardState)
        {
            bool invert = keyboardState.IsKeyDown(Key.ShiftLeft);
            float accelerationThisFrame = acceleration * (float) deltaTime;
            if (keyboardState.IsKeyDown(Key.X))
            {
                if (invert) Position += Vector3.UnitX * accelerationThisFrame;
                else Position -= Vector3.UnitX * accelerationThisFrame;
            }
            if (keyboardState.IsKeyDown(Key.Y))
            {
                if (invert) Position += Vector3.UnitY * accelerationThisFrame;
                else Position -= Vector3.UnitY * accelerationThisFrame;
            }
            if (keyboardState.IsKeyDown(Key.Z))
            {
                if (invert) Position -= Vector3.UnitZ * accelerationThisFrame;
                else Position += Vector3.UnitZ * accelerationThisFrame;
            }
        }
    }
}