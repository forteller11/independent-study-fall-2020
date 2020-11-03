using CART_457;
using CART_457.EntitySystem;
using CART_457.Renderer;
using OpenTK.Graphics.OpenGL4;


namespace CART_457Scripts.Blueprints
{
    public class FBOVisualizationInput : Entity
    {
        private int _blitOffscreenFBOsIndex = -1; //where -1 == default buffer no blit

        private void CycleFBOBlit()
        {
            _blitOffscreenFBOsIndex++;
            if (_blitOffscreenFBOsIndex >= DrawManager.BatchHierachies.Count)
                _blitOffscreenFBOsIndex = -1;
        }


        public override void OnUpdate(EntityUpdateEventArgs e)
        {
            if (e.InputState.F.IsPressed)
            {
                CycleFBOBlit();
                SetFBO();
            }

            if (e.InputState.G.IsPressed)
            {
                if (DrawManager.ReadBufferToDebugDraw == ReadBufferMode.ColorAttachment0)
                {
                    DrawManager.ReadBufferToDebugDraw = ReadBufferMode.ColorAttachment1;
                    Debug.Log("Switching to " + ReadBufferMode.ColorAttachment1);
                }

                else
                {
                    DrawManager.ReadBufferToDebugDraw = ReadBufferMode.ColorAttachment0;
                    Debug.Log("Switching to " + ReadBufferMode.ColorAttachment0);
                }
                    
            }
            
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