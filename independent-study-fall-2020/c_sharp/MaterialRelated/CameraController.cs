using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics.OpenGL4;

namespace Indpendent_Study_Fall_2020
{
    public class CameraController
    {
        private float acceleration = 1f;
        private float angularAcceleration = 1.8f;
        public Vector3 Position { get; private set; } = new Vector3(0,0,0);
        public Quaternion Rotation { get; private set; } = Quaternion.Identity;
        public void OnUpdate(double deltaTime, KeyboardState keyboardState)
        {
             
            bool invert = keyboardState.IsKeyDown(Key.ShiftLeft);
            float accelerationThisFrame = acceleration * (float) deltaTime;
            float angularAccelerationThisFrame = angularAcceleration * (float) deltaTime;
            
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

            Quaternion rotationVert = Quaternion.Identity;
            Quaternion rotationHorz = Quaternion.Identity;
            if (keyboardState.IsKeyDown(Key.Up))
                rotationVert = Quaternion.FromAxisAngle(Vector3.UnitX, angularAccelerationThisFrame);
            if (keyboardState.IsKeyDown(Key.Down))
                rotationVert = Quaternion.FromAxisAngle(Vector3.UnitX, -angularAccelerationThisFrame);
            if (keyboardState.IsKeyDown(Key.Left))
                rotationHorz = Quaternion.FromAxisAngle(Vector3.UnitY, angularAccelerationThisFrame);
            if (keyboardState.IsKeyDown(Key.Right))
                rotationHorz = Quaternion.FromAxisAngle(Vector3.UnitY, -angularAccelerationThisFrame);
            
            Rotation =  rotationHorz * Rotation * rotationVert;


        }
    }
}