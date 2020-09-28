using System;
using System.ComponentModel;
using FbxSharp;
using Indpendent_Study_Fall_2020.c_sharp.EntitySystem.Renderer;
using Indpendent_Study_Fall_2020.EntitySystem;
using Indpendent_Study_Fall_2020.EntitySystem.Gameobjects;
using Indpendent_Study_Fall_2020.Helpers;
using Indpendent_Study_Fall_2020.MaterialRelated;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using Vector3 = OpenTK.Vector3;

namespace Indpendent_Study_Fall_2020
{
    public class TKWindow : GameWindow
    {

        float[] positions = {
             1f/2,  -1f/2, -1/2f,  //rd
            -1f/2, -1f/2, -1/2f, //ld
            -1f/2, 1f/2, -1/2f,  //
            1f/2,  -1f/2, 1/2f,  //rd
            -1f/2, -1f/2, 1/2f, //ld
            -1f/2, 1f/2, 1/2f,  //lu
            1f, 2f, 0,
            -1, 2f, 0,
            0, -2f, 0
        };
        
        uint[] indices = {
            0, 1, 3, // The first triangle will be the bottom-right half of the triangle
            1, 2, 3  // Then the second will be the top-right half of the triangle
        };
        
        float[] uvs = {
            1.0f, 0.0f,
            0.0f, 0.0f,
            0.0f, 1.0f,
            
            1.0f, 0.0f,
            1.0f, 1.0f,
            0.0f, 1.0f,
        };
        
//        // For documentation on this, check Texture.cs
//        private Texture _texture;


        private GameObjectManager _gameObjectManager;
        
        #region initialise
        public TKWindow(int width, int height, GraphicsMode mode, string title) : base(width, height, mode, title) { }
        public TKWindow(int width, int height, GraphicsMode mode, string title, GameWindowFlags options, DisplayDevice device) : base(width, height, mode, title, options, device) { }

        public static TKWindow CreateAndRun()
        {

            var newTKWindow = new TKWindow(
                640, 
                640, 
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
            
            GL.ClearColor(1f,0f,1f,1f);
            
            Globals.Init();
            Globals.DirectionLights.Add(
                new DirectionLight(new Vector3(0,1,0), 1f)
                );
            
            Globals.PointLights.Add(
                new PointLight(new Vector3(0,3,4), 1f)
            );
            
            #region materials
            var testMat = new Material("test_mat", new ShaderProgram("shaded"));

            var modelAttribs = ModelImporter.GetAttribBuffersFromObjFile("cube_test", true, true, true);

            
            testMat.FeedBufferAndIndicesData(null, modelAttribs);
//            testMat.FeedBufferAndIndicesData(null, new AttributeBuffer("in_position", 3, positions));

            testMat.SetupATexture("unwrap_helper.jpg", "texture0", TextureUnit.Texture0, 0);
            testMat.SetupATexture("face.jpg", "texture1", TextureUnit.Texture1, 1);

            Globals.DrawManager.SetupAllMaterials(
                testMat
                );
            #endregion
            
            _gameObjectManager = new GameObjectManager();
            _gameObjectManager.Add(
                new CameraControllerSingleton(),
                new TestTriangleTexture()
            );
            
            _gameObjectManager.LoadAllGameObjects();
     
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

            //todo cache this object allocation so not creating objects everyframe
            var eventArgs = new GameObjectUpdateEventArgs(
                e.Time,
                Keyboard.GetState()
            );
      
            Globals.Update(eventArgs);
            _gameObjectManager.UpdateAllGameObjects(eventArgs);
            
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