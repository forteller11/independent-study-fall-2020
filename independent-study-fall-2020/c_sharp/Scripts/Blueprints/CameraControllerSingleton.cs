using System;
using CART_457.EntitySystem;
using CART_457.Helpers;
using CART_457.Renderer;
using OpenTK.Mathematics;

namespace CART_457.Scripts.Blueprints
{
    public class CameraControllerSingleton : EntitySystem.Entity
    {
        private float acceleration = 1.5f;
        private float angularAcceleration = 0.2f;
        
        private float _horziontalVelocity = .05f;




        public CameraControllerSingleton() : base( null) { }
        
        public override void OnLoad()
        {
            float near = 0.1f;
            float far = 100f;

            Globals.PlayerCameraRoom1.CopyFrom(Camera.CreatePerspective(Vector3.Zero, Quaternion.Identity,  MathHelper.DegreesToRadians(90), near, far));
            Globals.PlayerCameraRoom1.Position = new Vector3(0,0,2);
            Globals.PlayerCameraRoom1.Rotation = Quaternion.Identity;
  
            Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(90), 1, near, far, out var webCamPerspective);
            Globals.WebCamRoom1.CopyFrom(Camera.CreatePerspective(new Vector3(-.3f,1.8f,.6f), Quaternion.Identity, MathHelper.DegreesToRadians(45), near, far));
            
            Globals.ShadowCastingLightRoom1.CopyFrom(Camera.CreateOrthographic(new Vector3(0,10,0), Quaternion.FromAxisAngle(Vector3.UnitX, -MathF.PI/2), 25, near, far));
            
            Globals.PlayerCameraRoom2.CopyFrom(Globals.PlayerCameraRoom1);
            Globals.ShadowCastingLightRoom2.CopyFrom( Globals.ShadowCastingLightRoom1);

            Globals.MainCamera.CopyFrom(Globals.PlayerCameraRoom1);

        }

        
        public override void OnUpdate(EntityUpdateEventArgs eventArgs)
        {  
            Rotate(eventArgs);
            Move(eventArgs);
            
            Globals.PlayerCameraRoom2.CopyFrom(Globals.PlayerCameraRoom1);
            Globals.ShadowCastingLightRoom2.CopyFrom( Globals.ShadowCastingLightRoom1);
            Globals.WebCamRoom2.CopyFrom( Globals.WebCamRoom1);

        }

        void Rotate(EntityUpdateEventArgs eventArgs) //todo can't rotate around
        {
            float angularAccelerationThisFrame = (float) eventArgs.DeltaTime * angularAcceleration;
            Quaternion rotationVert = Quaternion.Identity;
            Quaternion rotationHorz = Quaternion.Identity;

            Vector2 accelerationInput = eventArgs.MouseDelta * angularAccelerationThisFrame;

            rotationVert = Quaternion.FromAxisAngle(Vector3.UnitX, accelerationInput.Y);
            rotationHorz = Quaternion.FromAxisAngle(Vector3.UnitY, -accelerationInput.X);

            Globals.PlayerCameraRoom1.Rotation =  rotationHorz * Globals.PlayerCameraRoom1.Rotation * rotationVert;
            LocalRotation = Globals.PlayerCameraRoom1.Rotation;
            // todo dont allow rotations past 90 degrees DOWN
        }
        void Move(EntityUpdateEventArgs eventArgs)
        {
            var input = eventArgs.InputState;
            float sprintMultiplier = eventArgs.InputState.AltL.IsHeldDown ? 3 : 1;
            float accelerationThisFrame = acceleration * (float) eventArgs.DeltaTime * sprintMultiplier;

            int horzInput = 0;
            int depthInput = 0;
            int verticalInput = 0;

            if (input.W.IsHeldDown)      depthInput--;
            if (input.S.IsHeldDown)      depthInput++;
            if (input.A.IsHeldDown)      horzInput--;
            if (input.D.IsHeldDown)      horzInput++;
            if (input.ShiftL.IsHeldDown) verticalInput--;
            if (input.Space.IsHeldDown)  verticalInput++;

            Vector3 inputVector = new Vector3(horzInput, 0, depthInput);
            Vector3 movementRelative = Globals.PlayerCameraRoom1.Rotation * inputVector;

            Vector2 movementHorzontal = Vector2.Zero; //make horizontal speed consistent no matter the rotation of the camera
            if (horzInput != 0 || depthInput != 0) 
            {
                Vector2 movementHorzontalInput = new Vector2(movementRelative.X, movementRelative.Z);
                movementHorzontal = Vector2.Normalize(movementHorzontalInput) * _horziontalVelocity * sprintMultiplier;
            }

            Globals.PlayerCameraRoom1.Position += new Vector3(movementHorzontal.X, verticalInput * accelerationThisFrame, movementHorzontal.Y);
            LocalPosition = Globals.PlayerCameraRoom1.Position;
        }

     
    }
}