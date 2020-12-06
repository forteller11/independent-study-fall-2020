
using CART_457.EntitySystem;
using CART_457.Helpers;
using CART_457.Renderer;


namespace CART_457.Scripts.Blueprints
{
    public class CameraInterperlator : CART_457.EntitySystem.Entity
    {
        private bool _isInterpolating;
        private Camera _interpolationTarget;
        private float _interpolationIndex;
        // private Camera _;
        private const float INTERPOLATION_STEP = 0.02f;
        public override void OnLoad()
        {
            _interpolationTarget = Globals.PlayerCamera;
        }

        public override void OnUpdate(EntityUpdateEventArgs eventArgs)
        {

            // CheckForBeginInterpolation(eventArgs.InputState.Num1, Globals.PlayerCameraRoom1);
            // CheckForBeginInterpolation(eventArgs.InputState.Num2, Globals.WebCamRoom1);
            // CheckForBeginInterpolation(eventArgs.InputState.Num3, Globals.UberDriver);
            //
            //
            // if (_isInterpolating)
            // {
            //     Camera.Lerp(Globals.MainCamera, _interpolationTarget, MathInd.SmoothStep(_interpolationIndex),  Globals.MainCamera);
            //     _interpolationIndex += INTERPOLATION_STEP;
            //     
            //     if (_interpolationIndex > 1)
            //     {
            //         _isInterpolating = false;
            //     }
            // }
            // else
            // {
            //     Globals.MainCamera.CopyFrom(_interpolationTarget);
            // }

        }

        private void CheckForBeginInterpolation(KeyState keyState, Camera target)
        {
            if (keyState.IsPressed)
            {
                _isInterpolating = true;
                _interpolationTarget = target;
                _interpolationIndex = 0;
            }
        }
        
    }
}