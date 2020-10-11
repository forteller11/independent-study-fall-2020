using System;
using Indpendent_Study_Fall_2020.c_sharp.EntitySystem.Renderer;
using Indpendent_Study_Fall_2020.EntitySystem;
using Indpendent_Study_Fall_2020.Scripts;
using Indpendent_Study_Fall_2020.Scripts.Materials;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;

namespace Indpendent_Study_Fall_2020
{
    public class TKWindow : GameWindow
    {

        private GameObjectManager _gameObjectManager;
        
        #region initialise
        public TKWindow(int width, int height, GraphicsMode mode, string title) : base(width, height, mode, title) { }
        public TKWindow(int width, int height, GraphicsMode mode, string title, GameWindowFlags options, DisplayDevice device) : base(width, height, mode, title, options, device) { }

        public static TKWindow CreateAndRun()
        {

            var newTKWindow = new TKWindow(
                1000, 
                1000, 
                GraphicsMode.Default, 
                $"Independent Study Fall 2020 - Charly Yan Miller ",
                GameWindowFlags.Default,
                DisplayDevice.Default);
            
            GL.Enable(EnableCap.DebugOutput);
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.DebugOutputSynchronous);
//            GL.Enable(EnableCap.Blend);
////            GL.BlendEquation(BlendEquationMode.FuncAdd);
//            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            GL.DebugMessageCallback(Debug.GLErrorCallback, IntPtr.Zero);
            
            newTKWindow.Run(60d);
            
            Debug.Log($"");
            Debug.Log($"Renderer: {GL.GetString(StringName.Renderer)}");
            Debug.Log($"Version: {GL.GetString(StringName.Version)}");
            Debug.Log($"Vendor: {GL.GetString(StringName.Vendor)}");
            Debug.Log($"");
            
            return newTKWindow;
        }
        #endregion

        
        protected override void OnLoad(EventArgs e)
        {

            base.OnLoad(e);
            
            GL.ClearColor(0f,0f,0f,1f);
            
            Globals.Init();
            SceneSetup.CreateGlobals();
            
            #region materials
            Globals.DrawManager.SetupDrawHierarchy(CreateFBOs.Create(), CreateMaterials.Create());
            #endregion
            
            _gameObjectManager = new GameObjectManager();
            _gameObjectManager.AddRange(SceneSetup.CreateGameObjects());
            _gameObjectManager.InvokeOnLoad();
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

            var  m = Mouse.GetState();
            
            //todo cache this object allocation so not creating objects everyframe
            var eventArgs = new GameObjectUpdateEventArgs(
                e.Time,
                Keyboard.GetState(),
                m,
                new Vector2(m.X-Globals.MousePositionLastFrame.X,-m.Y+Globals.MousePositionLastFrame.Y)
            );
      
            Globals.Update(eventArgs);
            _gameObjectManager.InvokeOnUpdate(eventArgs);
            
            if (eventArgs.KeyboardState.IsKeyDown(Key.Escape))
                Exit();
            
            base.OnUpdateFrame(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit); //I think this clears main color texture (buffer) AND depth texture (buffer)

            Globals.DrawManager.RenderFrame();

            base.OnRenderFrame(e);
            SwapBuffers();
            
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0,0, Width, Height);
        }
        
    }
}