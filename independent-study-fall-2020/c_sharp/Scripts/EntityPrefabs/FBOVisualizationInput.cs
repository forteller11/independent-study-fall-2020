
using CART_457.Renderer;
using OpenTK.Input;

namespace CART_457.EntitySystem.Scripts.EntityPrefabs
{
    public class FBOVisualizationInput : EntitySystem.Entity
    {
        public KeyEvent KeyCycle = new KeyEvent(Key.F);
        public KeyEvent KeySecondaryToggle = new KeyEvent(Key.CapsLock);
        private int _blitOffscreenFBOsIndex = -1; //where -1 == default buffer no blit

        public FBOVisualizationInput()
        {
            KeyCycle.OnPressed += ()=>
            {
                CycleFBOBlit();
                SetFBO();
            };
        }

        private void CycleFBOBlit()
        {
            _blitOffscreenFBOsIndex++;
            if (_blitOffscreenFBOsIndex >= DrawManager.BatchHierachies.Count)
                _blitOffscreenFBOsIndex = -1;
        }


        public override void OnUpdate(EntityUpdateEventArgs eventArgs)
        {
            KeyCycle.Update(eventArgs.KeyboardState);
            KeySecondaryToggle.Update(eventArgs.KeyboardState);
        }

        public void SetFBO()
        {
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
    }
}