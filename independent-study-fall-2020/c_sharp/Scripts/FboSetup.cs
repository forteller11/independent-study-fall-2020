using CART_457.Attributes;
using CART_457.MaterialRelated;
using CART_457.Renderer;
using OpenTK.Graphics.OpenGL4;

namespace CART_457.Scripts
{
    public class FboSetup
    {

        [IncludeInDrawLoop] public static FBO Shadow; 
        [IncludeInDrawLoop] public static FBO Main;
        [IncludeInDrawLoop] public static FBO Default;
        
        [IncludeInPostFX] public static FBO PostProcessing;
        static FboSetup ()
        {
        
            Shadow = FBO.Custom("Shadow", DrawManager.TKWindowSize, true,true, true,  ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit, null);
            
            Main = FBO.Custom("Main", DrawManager.TKWindowSize, true,true, true, ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit, () => {
                GL.Enable(EnableCap.Texture2D);
                GL.Enable(EnableCap.DepthTest);
                GL.Enable(EnableCap.CullFace);});


            Default = FBO.Default("Default",() => {
                GL.Enable(EnableCap.Texture2D);
                GL.Enable(EnableCap.DepthTest);
                GL.Enable(EnableCap.CullFace);});
            
            PostProcessing  = FBO.Custom("Post-FX", DrawManager.TKWindowSize, true, true,true,
                ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit, () =>
                {
                    // GL.Disable(EnableCap.DepthTest);
                });
            
        }
    }
}