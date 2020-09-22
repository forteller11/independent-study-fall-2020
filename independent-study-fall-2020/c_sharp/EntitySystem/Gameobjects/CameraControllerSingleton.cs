
using OpenTK;
using OpenTK.Input;

namespace Indpendent_Study_Fall_2020.EntitySystem.Gameobjects
{
    public class CameraControllerSingleton : GameObject
    {
        private float acceleration = 1f;
        private float angularAcceleration = 1.8f;

        public override void OnLoad()
        {
            Globals.CameraPosition = Vector3.Zero;
            Globals.CameraRotation = Quaternion.Identity;
        }

        public override void OnUpdate(GameObjectUpdateEventArgs eventArgs)
        {
            var keyboardState = eventArgs.KeyboardState;
            bool invert = keyboardState.IsKeyDown(Key.ShiftLeft);
            float accelerationThisFrame = acceleration * (float) eventArgs.DeltaTime;
            float angularAccelerationThisFrame = angularAcceleration * (float) eventArgs.DeltaTime;
            
            if (keyboardState.IsKeyDown(Key.X))
            {
                if (invert) Globals.CameraPosition += Vector3.UnitX * accelerationThisFrame;
                else Globals.CameraPosition -= Vector3.UnitX * accelerationThisFrame;
            }
            if (keyboardState.IsKeyDown(Key.Y))
            {
                if (invert) Globals.CameraPosition += Vector3.UnitY * accelerationThisFrame;
                else Globals.CameraPosition -= Vector3.UnitY * accelerationThisFrame;
            }
            if (keyboardState.IsKeyDown(Key.Z))
            {
                if (invert) Globals.CameraPosition -= Vector3.UnitZ * accelerationThisFrame;
                else Globals.CameraPosition += Vector3.UnitZ * accelerationThisFrame;
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
            
            Globals.CameraRotation =  rotationHorz * Globals.CameraRotation * rotationVert;


        }
    }
}