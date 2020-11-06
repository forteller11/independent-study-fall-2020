﻿using CART_457;
using CART_457.EntitySystem;
using CART_457.PhysicsRelated;
using CART_457.Renderer;
using CART_457.Scripts.Blueprints;
using CART_457.Scripts.Setups;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Indpendent_Study_Fall_2020.c_sharp.Scripts.Blueprints
{
    public class RaycastChecker : Entity
    {
        private EmptySolid VisulizerHit = new EmptySolid(Vector4.Zero, .05f, SetupMaterials.SolidSphereR1);

        private bool IsDragging;
        private float _rotationSensitivity = 0.03f;
        public override void OnUpdate(EntityUpdateEventArgs eventArgs)
        {
            Parent = null;
            LocalRotation = Globals.MainCamera.Rotation;
            LocalPosition = Globals.MainCamera.Position;
            var dir = WorldRotation * Vector3.UnitZ;
            
            var ray = new Ray(WorldPosition, dir);
            CollisionWorld.ColliderGroup.Raycast(ray, out var results);

            var result = results[0];
            
            bool mouseDown = eventArgs.MouseState.IsButtonDown(MouseButton.Left);
            
            if (!result.Hit || !mouseDown)
                IsDragging = false;
            else
            {
                IsDragging = true;
                var radiansToMove = eventArgs.MouseState.Delta * _rotationSensitivity;
                var rotHorz = Quaternion.FromAxisAngle(Vector3.UnitY, radiansToMove.X);
                var rotVert = Quaternion.FromAxisAngle(Vector3.UnitX, -radiansToMove.Y);

                result.HitEntity.LocalRotation = rotHorz * result.HitEntity.LocalRotation * rotVert;
            }
            
            
            VisulizerHit.Color = IsDragging ? new Vector4(.2f,.2f,1f,1) :  new Vector4(.6f,.2f,.2f,1);

            if (result.Hit)
                VisulizerHit.LocalPosition = result.NearestOrHitPosition;
            
            
        }
    }
}