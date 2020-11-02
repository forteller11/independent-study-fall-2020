
using System;
using CART_457.Renderer;

using OpenTK;
using OpenTK.Input;

namespace CART_457.EntitySystem.Scripts.Blueprints
{
    public class CameraControllerSingleton : EntitySystem.Entity
    {
        private float acceleration = 1.5f;
        private float angularAcceleration = 0.2f;
        
        private float _horziontalVelocity = .05f;




        public CameraControllerSingleton() : base(BehaviorFlags.None, null) { }
        
        public override void OnLoad()
        {
            float near = 0.1f;
            float far = 100f;
            
            Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(90), 1, near, far, out var playerCamPerspective);
            Globals.PlayerCameraRoom1.CopyFrom(new Camera(Vector3.Zero, Quaternion.Identity, playerCamPerspective, near, far));
            Globals.PlayerCameraRoom1.Position = new Vector3(0,0,2);
            Globals.PlayerCameraRoom1.Rotation = Quaternion.Identity;
            
            Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45), 1, near, far, out var webCamPerspective);
            Globals.WebCamRoom1.CopyFrom(new Camera(new Vector3(-.3f,1.8f,.6f), Quaternion.Identity, webCamPerspective, near, far));
            
            Matrix4.CreateOrthographic(25, 25, near, far, out var shadowLightPerspective);
            Globals.ShadowCastingLightRoom1.CopyFrom(new Camera(new Vector3(0,10,0), Quaternion.FromAxisAngle(Vector3.UnitX, -MathF.PI/2), shadowLightPerspective, near, far));
            
            
            
            Globals.MainCamera.CopyFrom(Globals.PlayerCameraRoom1);

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

            Globals.PlayerCameraRoom1.Rotation =  rotationHorz * Globals.PlayerCameraRoom1.Rotation * rotationVert;
            // todo dont allow rotations past 90 degrees DOWN
        }
        void Move(EntityUpdateEventArgs eventArgs)
        {
            var input = eventArgs.InputState;
            float sprintMultiplier = eventArgs.KeyboardState.IsKeyDown(Key.AltLeft) ? 3 : 1;
            float accelerationThisFrame = acceleration * (float) eventArgs.DeltaTime * sprintMultiplier;

            if (eventArgs.KeyboardState.IsKeyDown(Key.AltLeft))
                accelerationThisFrame *= 5;


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
        }

     
    }
}