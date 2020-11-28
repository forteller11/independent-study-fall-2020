using System;
using System.Drawing;
using CART_457.EntitySystem;
using CART_457.Helpers;
using CART_457.Scripts;
using CART_457.Renderer;
using CART_457.Scripts.Setups;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace CART_457
{
    public class TKWindow : GameWindow
    {

        #region initialise
        public TKWindow(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings) { }
        // public TKWindow(int width, int height, GraphicsMode mode, string title, GameWindowFlags options, DisplayDevice device) : base(width, height, mode, title, options, device) { }

        public static TKWindow CreateAndRun()
        {
            var gameWindowSettings = GameWindowSettings.Default;


            var nativeWindowSettings =  NativeWindowSettings.Default;
            nativeWindowSettings.Size = new Vector2i(1440, 1440);
            
            nativeWindowSettings.Title = "Independent Study Fall 2020 - Charly Yan Miller";
            nativeWindowSettings.WindowBorder = WindowBorder.Hidden;
            nativeWindowSettings.Location = new Vector2i(2560/2 - nativeWindowSettings.Size.X/2, 0);
            nativeWindowSettings.StartFocused = true;


            var newTKWindow = new TKWindow(gameWindowSettings, nativeWindowSettings);

            newTKWindow.CursorGrabbed = true;
            newTKWindow.CursorVisible = false;

            GL.Enable(EnableCap.DebugOutput);
            GL.Enable(EnableCap.DebugOutputSynchronous);

            GL.DebugMessageCallback(Debug.GLErrorCallback, IntPtr.Zero);
      
            Debug.Log($"");
            Debug.Log($"Renderer: {GL.GetString(StringName.Renderer)}");
            Debug.Log($"Version: {GL.GetString(StringName.Version)}");
            Debug.Log($"Vendor: {GL.GetString(StringName.Vendor)}");
            Debug.Log($"");
            
            newTKWindow.Run();

            return newTKWindow;
        }
        #endregion

        protected override void OnLoad()
        {
            base.OnLoad();
            DrawManager.Init(this);
            
            Globals.Init(this);
            SetupEntities.SetupGlobals();

            DrawManager.SetupStaticRenderingHierarchy();
            // DrawManager.TKWindowSize = new Size(, Height);
            
            SetupEntities.CreateGameObjects();
            EntityManager.InvokeOnLoad();
        }


        protected override void OnUnload()
        {
            base.OnUnload();
//todo 
//            GL.BindBuffer(BufferTarget.ArrayBuffer, 0); //reset binding to null
//            GL.DeleteBuffer(VBOVertHandle);
//            GL.DeleteBuffer(VAOHandle);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {

            EntityManager.RefreshUpdateEventArgs(this, e);

            Globals.Update(EntityManager.EntityUpdateEventArgs);
            EntityManager.InvokeOnUpdate();
            
            if (EntityManager.EntityUpdateEventArgs.KeyboardState.IsKeyDown(Keys.Escape))
                Close();
            
            MousePosition = MathInd.Convert(DrawManager.TKWindowSize)/2f;
            
            base.OnUpdateFrame(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            DrawManager.RenderFrame();

            base.OnRenderFrame(e);
            SwapBuffers();
            
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            DrawManager.TKWindowSize = e.Size;
            GL.Viewport(new Size(e.Width, e.Height));
        }

    }
}