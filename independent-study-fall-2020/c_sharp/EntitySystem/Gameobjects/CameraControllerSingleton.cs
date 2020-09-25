﻿
using System;
using OpenTK;
using OpenTK.Input;

namespace Indpendent_Study_Fall_2020.EntitySystem.Gameobjects
{
    public class CameraControllerSingleton : GameObject
    {
        private float acceleration = 1f;
        private float angularAcceleration = 1.8f;
        
        private float _horziontalMaxVelocity = .05f;
        private float _verticalMaxVelocity = 1f;

        public override void OnLoad()
        {
            Globals.CameraPosition = Vector3.Zero;
            Globals.CameraRotation = Quaternion.Identity;
        }

        public override void OnUpdate(GameObjectUpdateEventArgs eventArgs)
        {
            Rotate(eventArgs);
            Move(eventArgs);
        }

        void Rotate(GameObjectUpdateEventArgs eventArgs) //todo can't rotate around
        {
            var keyboardState = eventArgs.KeyboardState;
            float angularAccelerationThisFrame = (float) eventArgs.DeltaTime * angularAcceleration;
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
        void Move(GameObjectUpdateEventArgs eventArgs)
        {
            var keyboardState = eventArgs.KeyboardState;
            float accelerationThisFrame = acceleration * (float) eventArgs.DeltaTime;

            Vector3 input = Vector3.Zero;
            
            if (keyboardState.IsKeyDown(Key.W)) input -= Vector3.UnitZ;
            if (keyboardState.IsKeyDown(Key.S)) input += Vector3.UnitZ;
            if (keyboardState.IsKeyDown(Key.A)) input -= Vector3.UnitX;
            if (keyboardState.IsKeyDown(Key.D)) input += Vector3.UnitX;
            if (keyboardState.IsKeyDown(Key.LShift)) input -= Vector3.UnitY;
            if (keyboardState.IsKeyDown(Key.Space))  input += Vector3.UnitY;

            Vector3 movementAbsolute = input * accelerationThisFrame;
            Vector3 movementRelative = Globals.CameraRotation * movementAbsolute;


            Vector2 movementHorzontal = Vector2.Zero; //make horizontal speed consistent no matter the rotation of the camera
            Vector2 inputHorziontal = new Vector2(input.X, input.Z);
            if (!inputHorziontal.EqualsAprox(Vector2.Zero))
            {
                movementHorzontal = new Vector2(movementRelative.X, movementRelative.Z);
                movementHorzontal = Vector2.Normalize(movementHorzontal);
                movementHorzontal *= _horziontalMaxVelocity;

            }
       
;
            
            Globals.CameraPosition += new Vector3(movementHorzontal.X, movementAbsolute.Y, movementHorzontal.Y);
        }
    }
}