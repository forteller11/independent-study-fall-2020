using System;
using System.ComponentModel;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;

namespace Indpendent_Study_Fall_2020
{
    public class TKWindow : GameWindow
    {

//todo combine multiple datas into single  vbo and auto space in vao
//todo vbo that is static, vbo that aint static and is changed a meme temps
//todo flyweight vbo (mesh) and also per instance vbo
        

        
                float[] positions = {
                    -1.0f, -1.0f, 0.0f, //Bottom-left vertex
                    1.0f, -1.0f, 0.0f, //Bottom-right vertex
                    0.0f,  1.0f, 0.0f,  //Top vertex
        };
        
        float[] uvs = {
            0.0f, 0.0f, //Bottom-left vertex
            1.0f, 0.0f, //Bottom-right vertex
            0.5f, 1.0f  //Top vertex
        };
        
//        // For documentation on this, check Texture.cs
//        private Texture _texture;

        private Texture Texture1;
        private Texture Texture2;
        private int VBOVertHandle;
        private int VBOUVHandle;
        private int VAOHandle;
        
        private Material _material;
        
        #region initialise
        public TKWindow(int width, int height, GraphicsMode mode, string title) : base(width, height, mode, title) { }
        public TKWindow(int width, int height, GraphicsMode mode, string title, GameWindowFlags options, DisplayDevice device) : base(width, height, mode, title, options, device) { }

        public static TKWindow CreateAndRun()
        {

            var newTKWindow = new TKWindow(
                420, 
                420, 
                GraphicsMode.Default, 
                "Independent Study Fall 2020 - Charly Yan Miller",
                GameWindowFlags.Default,
                DisplayDevice.Default);
            
            GL.Enable(EnableCap.DebugOutput);
            GL.Enable(EnableCap.Texture2D);
            
            GL.DebugMessageCallback(Debug.GLErrorCallback, IntPtr.Zero);
            
            newTKWindow.Run(60d);

            return newTKWindow;
        }
        #endregion

        protected override void OnLoad(EventArgs e)
        {
            
            //todo have function to handle creating new vbo and auto asignment to vao
            base.OnLoad(e);
            
            GL.ClearColor(1f,0f,1f,1f);
            
            _material = new Material( new ShaderProgram("test.vert", "test.frag"));
            _material.SetupVAO(
                new AttributeBuffer("in_position", 3, positions),
                new AttributeBuffer("in_uv", 2, uvs )
            );
            _material.SetupATexture("unwrap_helper.jpg", "texture0", TextureUnit.Texture0, 0);
            _material.SetupATexture("face.jpg", "texture1", TextureUnit.Texture1, 1);

        }

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0); //reset binding to null
            GL.DeleteBuffer(VBOVertHandle);
            GL.DeleteBuffer(VAOHandle);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            var keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Key.Escape))
                Exit();
            
            base.OnUpdateFrame(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit); //I think this clears main color texture (buffer) AND depth texture (buffer)

            _material.SetMatrix4("transform", Matrix4.CreateRotationZ(50));
           _material.Draw();
            
            base.OnRenderFrame(e);
            SwapBuffers();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0,0, Width, Height);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
        }


        
    }
}