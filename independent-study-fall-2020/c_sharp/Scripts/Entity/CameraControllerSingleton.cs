
using System;
using System.Numerics;
using CART_457.Helpers;
using CART_457.Renderer;
using CART_457.Scripts;
using OpenTK.Input;
using Quaternion = OpenTK.Quaternion;
using Vector2 = OpenTK.Vector2;
using Vector3 = OpenTK.Vector3;

namespace CART_457.EntitySystem.Scripts.EntityPrefab
{
    public class CameraControllerSingleton : EntitySystem.Entity
    {
        private float acceleration = 1.5f;
        private float angularAcceleration = 0.2f;
        
        private float _horziontalVelocity = .05f;

        private KeyState PlayerCamKey;
        private KeyState WebCamKey;
        private float _camInterpIndex = 0;
        private const float CAM_INTERP_AMOUNT = 0.05f;


        public CameraControllerSingleton() : base(BehaviorFlags.None, null) { }
        
        public override void OnLoad()
        {
            Globals.PlayerCamera.Position = new Vector3(0,0,2);
            Globals.PlayerCamera.Rotation = Quaternion.Identity;
            
            Globals.MainCamera = Globals.PlayerCamera;

            PlayerCamKey = new KeyState(Key.Number1);
            WebCamKey = new KeyState(Key.Number2);

            PlayerCamKey.OnHeldDown += () => _camInterpIndex = MathInd.Lerp(_camInterpIndex, 0, CAM_INTERP_AMOUNT);
            WebCamKey.OnHeldDown    += () => _camInterpIndex = MathInd.Lerp(_camInterpIndex, 1, CAM_INTERP_AMOUNT);
            
        }

        
        public override void OnUpdate(EntityUpdateEventArgs eventArgs)
        {  
            Rotate(eventArgs);
            Move(eventArgs);

            CameraInterpolation(eventArgs);
        }

        void Rotate(EntityUpdateEventArgs eventArgs) //todo can't rotate around
        {
            float angularAccelerationThisFrame = (float) eventArgs.DeltaTime * angularAcceleration;
            Quaternion rotationVert = Quaternion.Identity;
            Quaternion rotationHorz = Quaternion.Identity;

            Vector2 accelerationInput = eventArgs.MouseDelta * angularAccelerationThisFrame;

            rotationVert = Quaternion.FromAxisAngle(Vector3.UnitX, accelerationInput.Y);
            rotationHorz = Quaternion.FromAxisAngle(Vector3.UnitY, -accelerationInput.X);

            Globals.PlayerCamera.Rotation =  rotationHorz * Globals.PlayerCamera.Rotation * rotationVert;
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

            if (input.W.IsHeldDown) depthInput--;
            if (input.S.IsHeldDown) depthInput++;
            if (input.A.IsHeldDown) horzInput--;
            if (input.D.IsHeldDown) horzInput++;
            if (input.ShiftL.IsHeldDown) verticalInput--;
            if (input.Space.IsHeldDown)  verticalInput++;

            Vector3 inputVector = new Vector3(horzInput, 0, depthInput);
            Vector3 movementAbsolute = inputVector * accelerationThisFrame;

            Vector3 movementRelative = Globals.PlayerCamera.Rotation * inputVector;

            Vector2 movementHorzontal = Vector2.Zero; //make horizontal speed consistent no matter the rotation of the camera
            if (horzInput != 0 || depthInput != 0) 
            {
                Vector2 movementHorzontalInput = new Vector2(movementRelative.X, movementRelative.Z);
                movementHorzontal = Vector2.Normalize(movementHorzontalInput) * _horziontalVelocity * sprintMultiplier;
            }

            Globals.PlayerCamera.Position += new Vector3(movementHorzontal.X, verticalInput * accelerationThisFrame, movementHorzontal.Y);
        }

        void CameraInterpolation(EntityUpdateEventArgs eventArgs)
        {

            // Globals.MainCamera = Camera.Lerp(Globals.MainCamera, Globals.WebCam, _camInterpIndex);
            Globals.MainCamera = Globals.PlayerCamera;
        }
    }
}