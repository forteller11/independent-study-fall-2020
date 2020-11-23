using System;
using CART_457;
using CART_457.EntitySystem;
using CART_457.PhysicsRelated;
using CART_457.Renderer;
using CART_457.Scripts.Blueprints;
using CART_457.Scripts.Setups;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Indpendent_Study_Fall_2020.c_sharp.Scripts.Blueprints
{
    public class WebcamRotater : Entity
    {
        private EmptySolid VisulizerHit = new EmptySolid(Vector4.Zero, .02f, SetupMaterials.SolidSphereR1);

        private bool IsDragging;
        private Entity _draggedEntity;
        private float _rotationSensitivity = 0.03f;
        private Entity _toCastAgainst;


        public WebcamRotater(Entity toCastAgainst)
        {
            _toCastAgainst = toCastAgainst;
        }
        public override void OnUpdate(EntityUpdateEventArgs eventArgs)
        {
            Parent = null;
            LocalRotation = Globals.MainCamera.Rotation;
            LocalPosition = Globals.MainCamera.Position;
            var dir = WorldRotation * Vector3.UnitZ;
            
            var ray = new Ray(WorldPosition, dir);
            bool hit = _toCastAgainst.ColliderGroup.Raycast(ray, out var results);

            bool mouseDown = eventArgs.MouseState.IsButtonDown(MouseButton.Left);

            if (hit && mouseDown)
            {
                IsDragging = true;
                _draggedEntity = results[0].HitEntity;
            }
            
            if (!mouseDown)
                IsDragging = false;
            
            if (IsDragging)
            {
                var radiansToMove = eventArgs.MouseDelta * _rotationSensitivity;
                var rotHorz = Quaternion.FromAxisAngle(Vector3.UnitY,  radiansToMove.X );
                var rotVert = Quaternion.FromAxisAngle(Vector3.UnitX, -radiansToMove.Y );

                _draggedEntity.LocalRotation = rotVert * _draggedEntity.LocalRotation * rotHorz;
            }
            
            
            VisulizerHit.Color = IsDragging ? new Vector4(.8f,.7f,0f,1) :  new Vector4(.4f,.4f,.4f,1);
            VisulizerHit.LocalScale = IsDragging ? new Vector3(0.01f) :  new Vector3(0.002f);

            if (hit)
                VisulizerHit.LocalPosition = results[0].NearestOrHitPosition;
            
            
        }
    }
}