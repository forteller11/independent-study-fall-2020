using CART_457.Attributes;
using CART_457.MaterialRelated;
using CART_457.Renderer;
using OpenTK.Graphics.OpenGL4;

namespace CART_457.Scripts
{
    public class FboSetup
    {

        [IncludeInDrawLoop] public static FBO Shadow; 
        [IncludeInDrawLoop] public static FBO Room1;
        [IncludeInDrawLoop] public static FBO Default;
        
        [IncludeInPostFX] public static FBO PostProcessing;
        // [IncludeInPostFX] public static FBO PassThroughPostFX;
        static FboSetup ()
        {
        
            Shadow = FBO.Custom("Shadow", DrawManager.TKWindowSize*4, Globals.ShadowCastingLight, true,true, true,  ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit,
                () =>
                {
                    GL.Enable(EnableCap.Texture2D);
                GL.Enable(EnableCap.DepthTest);
                GL.Enable(EnableCap.CullFace);
                GL.DepthFunc(DepthFunction.Less);
                });
            
            Room1 = FBO.Custom("Room1", DrawManager.TKWindowSize, Globals.MainCamera, true,true, true, ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit, () => {
                GL.Enable(EnableCap.Texture2D);
                GL.Enable(EnableCap.DepthTest);
                GL.Enable(EnableCap.CullFace);
                GL.DepthFunc(DepthFunction.Less);
            });


            Default = FBO.Default("Default",() => {
                GL.Enable(EnableCap.Texture2D);
                GL.Enable(EnableCap.DepthTest);
                GL.Enable(EnableCap.CullFace);
                GL.DepthFunc(DepthFunction.Less);  
                
            });
            
            PostProcessing  = FBO.Custom("Post-FX", DrawManager.TKWindowSize, null, true, true,false,
                ClearBufferMask.ColorBufferBit, () =>
                {
                     GL.Disable(EnableCap.DepthTest);
                     GL.Disable(EnableCap.CullFace);
                     GL.DepthFunc(DepthFunction.Always);  
                     
                });
               
            
        }
    }
}