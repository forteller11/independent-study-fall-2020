using System;
using System.ComponentModel;
using FbxSharp;
using Indpendent_Study_Fall_2020.EntitySystem;
using Indpendent_Study_Fall_2020.EntitySystem.Gameobjects;
using Indpendent_Study_Fall_2020.MaterialRelated;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;

namespace Indpendent_Study_Fall_2020
{
    public class TKWindow : GameWindow
    {

        float[] positions = {
             1f,  -1f, -1.0f,  //rd
            -1f, -1f, -1.0f, //ld
            -1f, 1f, -1.0f,  //lu

            1f, -1f, -1.0f,  //rd  
            1f, 1f, -1.0f,  //ru
            -1f, 1f, -1.0f,  //rd
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
                420, 
                420, 
                GraphicsMode.Default, 
                $"Independent Study Fall 2020 - Charly Yan Miller ",
                GameWindowFlags.Default,
                DisplayDevice.Default);
            
            GL.Enable(EnableCap.DebugOutput);
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.DebugOutputSynchronous);

            GL.DebugMessageCallback(Debug.GLErrorCallback, IntPtr.Zero);
            
            newTKWindow.Run(60d);
            return newTKWindow;
        }
        #endregion

        protected override void OnLoad(EventArgs e)
        {
            Debug.Log($"");
            Debug.Log($"Renderer: {GL.GetString(StringName.Renderer)}");
            Debug.Log($"Version: {GL.GetString(StringName.Version)}");
            Debug.Log($"Vendor: {GL.GetString(StringName.Vendor)}");
            Debug.Log($"");
            
            //todo have function to handle creating new vbo and auto asignment to vao
            base.OnLoad(e);
            
            GL.ClearColor(1f,0f,1f,1f);
            
            Globals.Init();
            #region materials
            var testMat = new Material("test_mat", new ShaderProgram("textureless.vert", "textureless.frag"));

            float[] colorsFlat = new float[6 * 4];
            for (int i = 0; i < 6; i++)
            {
                var col = Helpers.Color.RandomColor(Globals.Random);
                int baseIndex = i * 4;
                colorsFlat[baseIndex + 0] = col.R;
                colorsFlat[baseIndex + 1] = col.G;
                colorsFlat[baseIndex + 2] = col.B;
                colorsFlat[baseIndex + 3] = 1;
            }
            
            var colAttrib = new AttributeBuffer("in_Color", 4, colorsFlat);
            var modelAttribs = testMat.GetAttribBuffersFromObjFile("boxBent.obj");
            AttributeBuffer[] attribMergedBuffer = new AttributeBuffer[1 + modelAttribs.Length];
            attribMergedBuffer[0] = colAttrib;
            modelAttribs.CopyTo(attribMergedBuffer, 1);
            
            testMat.FeedBufferAndIndicesData(null, modelAttribs);
//            testMat.FeedBufferAndIndicesData(
//                null,
//                new AttributeBuffer("in_uv", 2, uvs),
//                new AttributeBuffer("in_position", 3, positions)
//                );
//            testMat.SetupIndices();
            
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