using CART_457.Attributes;
using CART_457.MaterialRelated;
using CART_457.Renderer;
using OpenTK.Graphics.OpenGL4;

namespace CART_457.Scripts
{
    public class FboSetup
    {

        [IncludeInDrawLoop] public static FBO Shadow1; 
         
        [IncludeInDrawLoop] public static FBO Room1;
        [IncludeInDrawLoop] public static FBO Room2;
        // [IncludeInDrawLoop] public static FBO Shadow2;
        [IncludeInDrawLoop] public static FBO Default;
        
        [IncludeInPostFX] public static FBO PostProcessing;
        // [IncludeInPostFX] public static FBO PassThroughPostFX;
        static FboSetup ()
        {
        
            Shadow1 = FBO.Serial("Shadow1", DrawManager.TKWindowSize*4, Globals.ShadowCastingLightRoom1, true,false, true,  ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit,
                () =>
                {
                    GL.Enable(EnableCap.Texture2D);
                GL.Enable(EnableCap.DepthTest);
                GL.Enable(EnableCap.CullFace);
                GL.DepthFunc(DepthFunction.Less);
                });
            
            Room1 = FBO.Serial("Room1", DrawManager.TKWindowSize, Globals.MainCamera, true,true, true, ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit, () => {
                GL.Enable(EnableCap.Texture2D);
                GL.Enable(EnableCap.DepthTest);
                GL.Enable(EnableCap.CullFace);
                GL.DepthFunc(DepthFunction.Less);
            });
            
            // Shadow2 = FBO.Custom("Shadow2", DrawManager.TKWindowSize*4, Globals.ShadowCastingLightRoom1, TextureUnit.Texture7,false, true,  ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit,
            //     () =>
            //     {
            //         GL.Enable(EnableCap.Texture2D);
            //         GL.Enable(EnableCap.DepthTest);
            //         GL.Enable(EnableCap.CullFace);
            //         GL.DepthFunc(DepthFunction.Less);
            //     });
            
            Room2 = FBO.Custom("Room2", DrawManager.TKWindowSize, Globals.MainCamera, TextureUnit.Texture6,TextureUnit.Texture7, TextureUnit.Texture8, ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit, () => {
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
            
            PostProcessing  = FBO.Serial("Post-FX", DrawManager.TKWindowSize, null, true, true,false,
                ClearBufferMask.ColorBufferBit, () =>
                {
                     GL.Disable(EnableCap.DepthTest);
                     GL.Disable(EnableCap.CullFace);
                     GL.DepthFunc(DepthFunction.Always);  
                     
                });
               
            
        }
    }
}