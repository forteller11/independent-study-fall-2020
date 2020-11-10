using System;
using CART_457.EntitySystem;
using CART_457.Helpers;
using CART_457.PhysicsRelated;
using CART_457.Renderer;
using OpenTK.Mathematics;

namespace CART_457.Scripts.Blueprints
{
    public class CameraControllerSingleton : EntitySystem.Entity
    {
        private float acceleration = 1.5f;
        private float angularAcceleration = 0.2f;

        public Camera Camera;
        public ColliderGroup Floor;
        
        private float _horziontalVelocity = .05f;

        public Vector3 Velocity;
        public float PlayerHeight = 1.5f;
        public float Acceleration = 0.05f;
        public float SprintMultiplier = 3f;
        public float MaxVelocity = .2f;
        public float Drag = 0.8f;




        public CameraControllerSingleton(ColliderGroup floor, Camera camera) : base(null)
        {
            Camera = camera;
            Floor = floor;
        }
        
        public override void OnLoad()
        {
            float near = 0.1f;
            float far = 100f;

            Globals.PlayerCameraRoom1.CopyFrom(Camera.CreatePerspective(Vector3.Zero, Quaternion.Identity,  MathHelper.DegreesToRadians(90), near, far));

            Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(90), 1, near, far, out var webCamPerspective);
            Globals.WebCamRoom1.CopyFrom(Camera.CreatePerspective(new Vector3(-.3f,1.8f,.6f), Quaternion.Identity, MathHelper.DegreesToRadians(45), near, far));
            
            Globals.ShadowCastingLightRoom1.CopyFrom(Camera.CreateOrthographic(new Vector3(0,10,0), Quaternion.FromAxisAngle(Vector3.UnitX, -MathF.PI/2), 25, near, far));
            
            Globals.PlayerCameraRoom2.CopyFrom(Globals.PlayerCameraRoom1);
            Globals.ShadowCastingLightRoom2.CopyFrom( Globals.ShadowCastingLightRoom1);

            Globals.MainCamera.CopyFrom(Globals.PlayerCameraRoom1);
            
            LocalPosition = new Vector3(0,6,0);

        }

        
        public override void OnUpdate(EntityUpdateEventArgs eventArgs)
        {  
            Rotate(eventArgs);
            Move3D(eventArgs);
            // MoveWalkingSim(eventArgs);
            
            Globals.PlayerCameraRoom1.ToEntityOrientation(this);
            
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
            
            if (eventArgs.InputState.R.IsPressed)
                rotationHorz *= Quaternion.FromAxisAngle(Vector3.UnitY, MathF.PI/4f);
            
            LocalRotation = rotationHorz * Camera.Rotation * rotationVert;
            
            // todo dont allow rotations past 90 degrees DOWN
        }

        void MoveWalkingSim(EntityUpdateEventArgs eventArgs)
        {
            Velocity *= Drag;
            
            InputState input = eventArgs.InputState;
            Vector3 absoluteInput = Vector3.Zero;
            if (input.W.IsHeldDown) absoluteInput.Z--;
            if (input.S.IsHeldDown) absoluteInput.Z++;
            if (input.A.IsHeldDown) absoluteInput.X--;
            if (input.D.IsHeldDown) absoluteInput.X++;

            float sprintMultiplier = (input.ShiftL.IsHeldDown) ? SprintMultiplier : 1;

            if (absoluteInput.X != 0 || absoluteInput.Z != 0) 
            {
                Vector3 movementRelative = LocalRotation * absoluteInput;
                Vector2 accelerationRelative = Vector2.Normalize(new Vector2(movementRelative.X, movementRelative.Z)) * Acceleration * sprintMultiplier;
                Velocity += new Vector3(accelerationRelative.X, 0, accelerationRelative.Y);
            }

            float velMag = Velocity.Length; //clamp
            if (velMag > MaxVelocity * sprintMultiplier)
                Velocity = (Velocity/velMag) * MaxVelocity * sprintMultiplier;

            var targetPos = WorldPosition + Velocity;

            Velocity = Vector3.Zero;

            int maxAttempts = 10;
            float lerpAmount = 1f;
            while (maxAttempts > 0)
            {
                var attemptedPos = Vector3.Lerp(WorldPosition, targetPos, lerpAmount);
                var ray = new Ray(attemptedPos, Vector3.UnitY);
                if (Floor.Raycast(ray, out var results, true))
                {
                    LocalPosition = results[0].NearestOrHitPosition + new Vector3(0,PlayerHeight,0);
                    break;
                }
                
                Velocity *= Drag * Drag * Drag * Drag * Drag;
                lerpAmount -= 0.1f;
                maxAttempts--;
            }
 
            

        }
        void Move3D(EntityUpdateEventArgs eventArgs)
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
            

            Vector2 movementHorzontal = Vector2.Zero; //make horizontal speed consistent no matter the rotation of the camera
            if (horzInput != 0 || depthInput != 0) 
            {
                Vector3 inputVector = new Vector3(horzInput, 0, depthInput);
                Vector3 movementRelative = Camera.Rotation * inputVector;
                Vector2 movementHorzontalInput = new Vector2(movementRelative.X, movementRelative.Z);
                movementHorzontal = Vector2.Normalize(movementHorzontalInput) * _horziontalVelocity * sprintMultiplier;
            }

            LocalPosition += new Vector3(movementHorzontal.X, verticalInput * accelerationThisFrame, movementHorzontal.Y);
        }

     
    }
}