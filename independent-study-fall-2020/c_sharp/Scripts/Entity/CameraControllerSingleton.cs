
using System;
using Indpendent_Study_Fall_2020.Scripts;
using OpenTK;
using OpenTK.Input;

namespace Indpendent_Study_Fall_2020.EntitySystem.Scripts.Gameobjects
{
    public class CameraControllerSingleton : Entity
    {
        private float acceleration = 1.5f;
        private float angularAcceleration = 0.2f;
        
        private float _horziontalMaxVelocity = .05f;


        public CameraControllerSingleton(CreateMaterials.MaterialType materialType) : base(materialType, BehaviorFlags.None) { }
        
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
            var keyboardState = eventArgs.KeyboardState;
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
            float sprintMultiplier = eventArgs.KeyboardState.IsKeyDown(Key.AltLeft) ? 6 : 1;
            float accelerationThisFrame = acceleration * (float) eventArgs.DeltaTime * sprintMultiplier;

            if (eventArgs.KeyboardState.IsKeyDown(Key.AltLeft))
                accelerationThisFrame *= 5;

            Vector3 input = Vector3.Zero;
            
            if (keyboardState.IsKeyDown(Key.W)) input -= Vector3.UnitZ;
            if (keyboardState.IsKeyDown(Key.S)) input += Vector3.UnitZ;
            if (keyboardState.IsKeyDown(Key.A)) input -= Vector3.UnitX;
            if (keyboardState.IsKeyDown(Key.D)) input += Vector3.UnitX;
            if (keyboardState.IsKeyDown(Key.LShift)) input -= Vector3.UnitY;
            if (keyboardState.IsKeyDown(Key.Space))  input += Vector3.UnitY;

            Vector3 movementAbsolute = input * accelerationThisFrame;
            Vector3 movementRelative = Globals.MainCamera.Rotation * movementAbsolute;


            Vector2 movementHorzontal = Vector2.Zero; //make horizontal speed consistent no matter the rotation of the camera
            Vector2 inputHorziontal = new Vector2(input.X, input.Z);
            if (!inputHorziontal.EqualsAprox(Vector2.Zero))
            {
                movementHorzontal = new Vector2(movementRelative.X, movementRelative.Z);
                movementHorzontal = Vector2.Normalize(movementHorzontal);
                movementHorzontal *= _horziontalMaxVelocity * sprintMultiplier;

            }
       
;
            
            Globals.MainCamera.Position += new Vector3(movementHorzontal.X, movementAbsolute.Y, movementHorzontal.Y);
        }

        
    }
}