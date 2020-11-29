using System;
using CART_457;
using CART_457.EntitySystem;
using CART_457.PhysicsRelated;
using CART_457.Scripts.Setups;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Indpendent_Study_Fall_2020.c_sharp.Scripts.Blueprints
{
    public class DoorHandle : Entity
    {
        public bool BeingHeld;
        public bool RaycastHitThisFrame;
        private float _rotationAcceleration;
        private DoorOpen _doorOpen;

        public DoorHandle(DoorOpen doorOpen) : base(new []{SetupMaterials.DoorOpenHandle})
        {
            _doorOpen = doorOpen;
        }
   

        public override void OnRaycastHit()
        {
            RaycastHitThisFrame = true;
        }

        public override void OnUpdate(EntityUpdateEventArgs eventArgs)
        {
            bool mouseDown = eventArgs.MouseState.IsButtonDown(MouseButton.Left);
            if (RaycastHitThisFrame && mouseDown && !BeingHeld)
            {
                BeingHeld = true;
                _doorOpen.BeginOpen();
            }
            else if (!mouseDown && BeingHeld)
            {
                BeingHeld = false;
                _doorOpen.EndOpen();
            }

            if (BeingHeld)
                LocalRotation = Quaternion.Slerp(LocalRotation, Quaternion.FromEulerAngles(0f,MathHelper.DegreesToRadians(-10.4f),-MathF.PI/2), 0.1f);
            else
                LocalRotation = Quaternion.Slerp(LocalRotation, Quaternion.FromEulerAngles(0f,MathHelper.DegreesToRadians(-10.4f),0), 0.02f);
            
            RaycastHitThisFrame = false;
        }
    }
}