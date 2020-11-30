using System;
using CART_457;
using CART_457.EntitySystem;
using CART_457.Helpers;
using CART_457.Renderer;
using CART_457.Scripts.Blueprints;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Indpendent_Study_Fall_2020.c_sharp.Scripts.Blueprints
{
    public class UberDriver : Entity
    {
        private Camera _camera;
        public bool BeginDriving;
        public Vector3 VelocityCurrent;
        public Vector3 MaxVelocity;
        public float _acceleration;
        public float _accelerationIndex;
        public UberDriver(Camera camera, Vector3 position, Quaternion rotation, Vector3 maxVelocity, float percentageAcceleration)
        {
            MaxVelocity = maxVelocity;
            _acceleration = percentageAcceleration;
            
            _camera = camera;
            LocalPosition = position;
            LocalRotation = rotation;
            
        }

        public override void OnUpdate(EntityUpdateEventArgs eventArgs)
        {
            _camera.ToEntityOrientation(this);

            // if (eventArgs.KeyboardState.IsKeyDown(Keys.J))
            // {
            //     BeginDriving = true;
            //     ScreenManager.SetTarget(Globals.UberDriver);
            // }

            if (BeginDriving)
            {
                //Debug.Log(Globals.DeltaTimeF);
                _accelerationIndex += _acceleration * Globals.DeltaTimeF;
                _accelerationIndex = MathHelper.Clamp(_accelerationIndex, 0, 1f);
                VelocityCurrent = Vector3.Lerp(Vector3.Zero, MaxVelocity, MathInd.SmoothStep(_accelerationIndex));
                LocalPosition += VelocityCurrent * Globals.DeltaTimeF;
            }
        }
    }
}