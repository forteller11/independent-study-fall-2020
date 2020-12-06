using System;
using CART_457.EntitySystem;
using CART_457.Helpers;
using CART_457.PhysicsRelated;
using CART_457.Renderer;
using CART_457.Scripts.Setups;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace CART_457.Scripts.Blueprints
{
    public class PlayerController : EntitySystem.Entity
    {
        private float acceleration = 1.5f;
        private float angularAcceleration = 0.2f;

        public Camera Camera;
        public ColliderGroup Floor;
        
        private float _horziontalVelocity = .05f;

        public Vector3 Velocity;
        public float PlayerHeight = 5f;
        public float Acceleration = 0.08f;
        public float SprintMultiplier = 3f;
        public float MaxVelocity = .2f;
        public float Drag = 0.95f;
        
        public EmptySolid RayHitVisualizer = new EmptySolid(new Vector4(0.6f), 1f, SetupMaterials.SolidSphereR1);



        public PlayerController(ColliderGroup floor, Camera camera) : base(null)
        {
            Camera = camera;
            Floor = floor;
        }
        
        public override void OnLoad()
        {
            LocalPosition = new Vector3(0,6,0);

        }

        
        public override void OnUpdate(EntityUpdateEventArgs eventArgs)
        {  
            Rotate(eventArgs);
            //Move3D(eventArgs);
            MoveWalkingSim(eventArgs);

            RaycastAndVisualize(eventArgs.MouseState.IsButtonDown(MouseButton.Left));

            Globals.PlayerCamera.ToEntityOrientation(this);

        }

        void Rotate(EntityUpdateEventArgs eventArgs) //todo can't rotate around
        {
            float angularAccelerationThisFrame = (float) eventArgs.DeltaTime * angularAcceleration;
            Quaternion rotationVert = Quaternion.Identity;
            Quaternion rotationHorz = Quaternion.Identity;

            Vector2 accelerationInput = eventArgs.MouseDelta * angularAccelerationThisFrame;

            rotationVert = Quaternion.FromAxisAngle(Vector3.UnitX, -accelerationInput.Y);
            rotationHorz = Quaternion.FromAxisAngle(Vector3.UnitY, -accelerationInput.X);
            
            if (eventArgs.InputState.R.IsPressed)
                rotationHorz *= Quaternion.FromAxisAngle(Vector3.UnitY, MathF.PI/4f);
            
            LocalRotation = rotationHorz * LocalRotation * rotationVert;
            
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
                lerpAmount -= 1f / maxAttempts;
                maxAttempts--;
            }
 
            

        }
        void Move3D(EntityUpdateEventArgs eventArgs)
        {
            var input = eventArgs.InputState;
            float sprintMultiplier = eventArgs.InputState.AltL.IsHeldDown ? 6 : 1;
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

        void RaycastAndVisualize(bool mouseDown)
        {
            var dir = WorldRotation * Vector3.UnitZ;
            var ray = new Ray(WorldPosition, dir);
            if (CollisionWorld.ColliderGroup.Raycast(ray, out var hits, true, true))
            {
                RayHitVisualizer.LocalPosition = hits[0].NearestOrHitPosition;
                
                if (!mouseDown)
                {
                    RayHitVisualizer.LocalScale = new Vector3(.1f);
                    RayHitVisualizer.Color = new Vector4(0.6f);
                }
                else
                {
                    RayHitVisualizer.LocalScale = new Vector3(.06f);
                    RayHitVisualizer.Color = new Vector4(230, 245, 60, 255)/255;
                }

            }
            else 
                RayHitVisualizer.LocalScale = new Vector3(0);
        }

     
    }
}