using CART_457.EntitySystem;
using CART_457.PhysicsRelated;
using CART_457.Renderer;
using CART_457.Scripts.Blueprints;
using CART_457.Scripts.Setups;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Indpendent_Study_Fall_2020.c_sharp.Scripts.Blueprints
{
    public class RotateOnHit : Entity
    {
       // private EmptySolid VisulizerHit = new EmptySolid(Vector4.Zero, .02f, SetupMaterials.SolidSphereR1);

        private bool IsDragging;
        private float _rotationSensitivity = 0.03f;

        public override void OnRaycastHit()
        {
            IsDragging = true;
        }

        public RotateOnHit(Vector3 position, Quaternion rotation)
        {
            LocalPosition = position;
            LocalRotation = rotation;
        }
        

        public override void OnUpdate(EntityUpdateEventArgs eventArgs)
        {
            bool mouseDown = eventArgs.MouseState.IsButtonDown(MouseButton.Left);

            if (!mouseDown)
            {
                IsDragging = false;
                return;
            }

            if (IsDragging)
            {
                var radiansToMove = eventArgs.MouseDelta * _rotationSensitivity;
                var rotHorz = Quaternion.FromAxisAngle(Vector3.UnitY,  radiansToMove.X );
                var rotVert = Quaternion.FromAxisAngle(Vector3.UnitX, -radiansToMove.Y );
                LocalRotation = rotVert * LocalRotation * rotHorz;

            }

            //VisulizerHit.Color = IsDragging ? new Vector4(.8f,.7f,0f,1) :  new Vector4(.4f,.4f,.4f,1);
            //VisulizerHit.LocalScale = IsDragging ? new Vector3(0.01f) :  new Vector3(0.002f);


        }
    }
}