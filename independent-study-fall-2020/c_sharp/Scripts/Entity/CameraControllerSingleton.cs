
using System;
using CART_457.Renderer;
using CART_457.Scripts;
using OpenTK;
using OpenTK.Input;

namespace CART_457.EntitySystem.Scripts.Entity
{
    public class CameraControllerSingleton : EntitySystem.Entity
    {
        private float acceleration = 1.5f;
        private float angularAcceleration = 0.2f;
        
        private float _horziontalVelocity = .05f;


        public CameraControllerSingleton() : base(BehaviorFlags.None, null) { }
        
        public override void OnLoad()
        {
            Globals.MainCamera.Position = new Vector3(0,0,2);
            Globals.MainCamera.Rotation = Quaternion.Identity;
        }

        public override void OnUpdate(EntityUpdateEventArgs eventArgs)
        {  
            Rotate(eventArgs);
            Move(eventArgs);
        }

        void Rotate(EntityUpdateEventArgs eventArgs) //todo can't rotate around
        {
            float angularAccelerationThisFrame = (float) eventArgs.DeltaTime * angularAcceleration;
            Quaternion rotationVert = Quaternion.Identity;
            Quaternion rotationHorz = Quaternion.Identity;

            Vector2 accelerationInput = eventArgs.MouseDelta * angularAccelerationThisFrame;

            rotationVert = Quaternion.FromAxisAngle(Vector3.UnitX, accelerationInput.Y);
            rotationHorz = Quaternion.FromAxisAngle(Vector3.UnitY, -accelerationInput.X);

            Globals.MainCamera.Rotation =  rotationHorz * Globals.MainCamera.Rotation * rotationVert;
            // todo dont allow rotations past 90 degrees DOWN
        }
        void Move(EntityUpdateEventArgs eventArgs)
        {
            var keyboardState = eventArgs.KeyboardState;
            float sprintMultiplier = eventArgs.KeyboardState.IsKeyDown(Key.AltLeft) ? 3 : 1;
            float accelerationThisFrame = acceleration * (float) eventArgs.DeltaTime * sprintMultiplier;

            if (eventArgs.KeyboardState.IsKeyDown(Key.AltLeft))
                accelerationThisFrame *= 5;


            int horzInput = 0;
            int depthInput = 0;
            int verticalInput = 0;

            if (keyboardState.IsKeyDown(Key.W)) depthInput--;
            if (keyboardState.IsKeyDown(Key.S)) depthInput++;
            if (keyboardState.IsKeyDown(Key.A)) horzInput--;
            if (keyboardState.IsKeyDown(Key.D)) horzInput++;
            if (keyboardState.IsKeyDown(Key.LShift)) verticalInput--;
            if (keyboardState.IsKeyDown(Key.Space))  verticalInput++;

            Vector3 inputVector = new Vector3(horzInput, 0, depthInput);
            Vector3 movementAbsolute = inputVector * accelerationThisFrame;

            Vector3 movementRelative = Globals.MainCamera.Rotation * inputVector;


            Vector2 movementHorzontal = Vector2.Zero; //make horizontal speed consistent no matter the rotation of the camera
            if (horzInput != 0 || depthInput != 0) 
            {
                Vector2 movementHorzontalInput = new Vector2(movementRelative.X, movementRelative.Z);
                movementHorzontal = Vector2.Normalize(movementHorzontalInput) * _horziontalVelocity * sprintMultiplier;
            }

            Globals.MainCamera.Position += new Vector3(movementHorzontal.X, verticalInput * accelerationThisFrame, movementHorzontal.Y);
        }

        
    }
}