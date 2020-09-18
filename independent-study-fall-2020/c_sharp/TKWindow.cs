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
        
        float[] vertices = //interweaving of position and tex coords into same vbo and array is done for perf reasons (less state-changes in opengl)
        {
            //Position          Texture coordinates
            -1.0f, -1.0f, 0.0f, 0.0f, 0.0f, // Bottom-left vertex
             1.0f, -1.0f, 0.0f, 1.0f, 0.0f, // Bottom-right vertex
             0.0f,  1.0f, 0.0f, 0.5f, 1.0f //Top vertex
        };
        
//        // For documentation on this, check Texture.cs
//        private Texture _texture;

        private Texture Texture1;
        private Texture Texture2;
        private int VBOVertHandle;
        private int VBOUVHandle;
        private int VAOHandle;
        
        private ShaderProgram _shaderProgram;
        
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
            
            _shaderProgram = new ShaderProgram("test.vert", "test.frag");

            #region vbos
            VBOVertHandle = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBOVertHandle);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
            #endregion
       
            #region texture
            Texture1 = new Texture("unwrap_helper.jpg");
            Texture1.UploadToOpenGLUniform("texture0", TextureUnit.Texture0, _shaderProgram);
            _shaderProgram.SetUniformInt("texture0", 0);
            
            Texture2 = new Texture("face.jpg");
            Texture2.UploadToOpenGLUniform("texture1", TextureUnit.Texture1, _shaderProgram);
            _shaderProgram.SetUniformInt("texture1", 1);
   
            #endregion
            
            #region vao
            VAOHandle = GL.GenVertexArray();
            GL.BindVertexArray(VAOHandle);
            GL.VertexAttribPointer(
                _shaderProgram.GetAttribLocation("in_position"), 
                3,
                VertexAttribPointerType.Float,
                false,
                5 * sizeof(float),
                0);
            GL.EnableVertexAttribArray(_shaderProgram.GetAttribLocation("in_position"));
            
            GL.VertexAttribPointer(
                _shaderProgram.GetAttribLocation("in_uv"), 
                2,
                VertexAttribPointerType.Float,
                false,
                5 * sizeof(float), //total size of a vertex
                3 * sizeof(float)); //memory offset within stride
            GL.EnableVertexAttribArray(_shaderProgram.GetAttribLocation("in_uv"));
            #endregion
            

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
            
            _shaderProgram.Use();

            Texture1.Use(TextureUnit.Texture0);
            Texture2.Use(TextureUnit.Texture1);
            
            GL.BindVertexArray(VAOHandle);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
            
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