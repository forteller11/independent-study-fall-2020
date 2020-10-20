﻿using CART_457.Attributes;
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
        // [IncludeInPostFX] public static FBO PassThroughPostFX;
        static FboSetup ()
        {
        
            Shadow = FBO.Custom("Shadow", DrawManager.TKWindowSize*4, true,true, true,  ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit,
                () =>
                {
                    
                     GL.Enable(EnableCap.Texture2D);
                GL.Enable(EnableCap.DepthTest);
                GL.Enable(EnableCap.CullFace);
                GL.DepthFunc(DepthFunction.Less);
                });
            
            Main = FBO.Custom("Main", DrawManager.TKWindowSize, true,true, true, ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit, () => {
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
            
            PostProcessing  = FBO.Custom("Post-FX", DrawManager.TKWindowSize, true, true,false,
                ClearBufferMask.ColorBufferBit, () =>
                {
                     GL.Disable(EnableCap.DepthTest);
                     GL.Disable(EnableCap.CullFace);
                     GL.DepthFunc(DepthFunction.Always);  
                     
                });
               
            
        }
    }
}