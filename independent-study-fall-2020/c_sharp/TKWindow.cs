using System;
using System.Drawing;
using CART_457.EntitySystem;
using CART_457.Scripts;
using CART_457.Renderer;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;

namespace CART_457
{
    public class TKWindow : GameWindow
    {

        #region initialise
        public TKWindow(int width, int height, GraphicsMode mode, string title) : base(width, height, mode, title) { }
        public TKWindow(int width, int height, GraphicsMode mode, string title, GameWindowFlags options, DisplayDevice device) : base(width, height, mode, title, options, device) { }

        public static TKWindow CreateAndRun()
        {

            var newTKWindow = new TKWindow(
                1440, 
                1440, 
                GraphicsMode.Default, 
                $"Independent Study Fall 2020 - Charly Yan Miller ",
                GameWindowFlags.Default,
                DisplayDevice.Default);
            
            GL.Enable(EnableCap.DebugOutput);
            GL.Enable(EnableCap.DebugOutputSynchronous);

            GL.DebugMessageCallback(Debug.GLErrorCallback, IntPtr.Zero);
            
            Debug.Log($"");
            Debug.Log($"Renderer: {GL.GetString(StringName.Renderer)}");
            Debug.Log($"Version: {GL.GetString(StringName.Version)}");
            Debug.Log($"Vendor: {GL.GetString(StringName.Vendor)}");
            Debug.Log($"");
            
            newTKWindow.Run(60d);

            return newTKWindow;
        }
        #endregion

        
        protected override void OnLoad(EventArgs e)
        {

            base.OnLoad(e);
            DrawManager.Init(this);
            
            Globals.Init();
            SceneSetup.CreateGlobals();

            DrawManager.SetupStaticRenderingHierarchy();
            DrawManager.TKWindowSize = new Size(Width, Height);
            
            EntityManager.AddRangeToWorldAndRenderer(SceneSetup.CreateGameObjects());
            EntityManager.InvokeOnLoad();
        }

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
//todo 
//            GL.BindBuffer(BufferTarget.ArrayBuffer, 0); //reset binding to null
//            GL.DeleteBuffer(VBOVertHandle);
//            GL.DeleteBuffer(VAOHandle);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {

            EntityManager.RefreshUpdateEventArgs(e);
      
            Globals.Update(EntityManager.EntityUpdateEventArgs);
            EntityManager.InvokeOnUpdate();
            
            if (EntityManager.EntityUpdateEventArgs.KeyboardState.IsKeyDown(Key.Escape))
                Exit();
            
            base.OnUpdateFrame(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            DrawManager.RenderFrame();

            base.OnRenderFrame(e);
            SwapBuffers();
            
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            DrawManager.TKWindowSize = new Size(Width, Height);
            GL.Viewport(DrawManager.TKWindowSize);
        }
        
    }
}