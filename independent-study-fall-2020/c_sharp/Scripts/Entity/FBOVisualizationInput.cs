
using CART_457.Renderer;
using OpenTK.Input;

namespace CART_457.EntitySystem.Scripts.Entity
{
    public class FBOVisualizationInput : EntitySystem.Entity
    {
        private bool _keyDownLastFrame = false;
        private int _blitOffscreenFBOsIndex = -1; //where -1 == default buffer no blit

        private void CycleFBOBlit()
        {
            _blitOffscreenFBOsIndex++;
            if (_blitOffscreenFBOsIndex >= DrawManager.BatchHierachies.Count)
                _blitOffscreenFBOsIndex = -1;
        }


        public override void OnUpdate(EntityUpdateEventArgs eventArgs)
        {
            bool keyStateDownThisFrame = eventArgs.KeyboardState.IsKeyDown(Key.F);
            if (_keyDownLastFrame == false && keyStateDownThisFrame == true)
            {
                CycleFBOBlit();
                if (_blitOffscreenFBOsIndex == -1)
                {
                    DrawManager.FBOToDebugDraw = null;
                    Debug.Log("Showing default fbo");
                }
                else
                {
                    DrawManager.FBOToDebugDraw = DrawManager.BatchHierachies[_blitOffscreenFBOsIndex].FBO;
                    Debug.Log("Showing FBO: " + DrawManager.FBOToDebugDraw.Name);
                }

            }
            
            _keyDownLastFrame = keyStateDownThisFrame;
        }
    }
}