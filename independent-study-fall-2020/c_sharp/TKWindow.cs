﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;


namespace Indpendent_Study_Fall_2020
{
    public class TKWindow : GameWindow
    {
//        float[] positions = {
//            -0.5f, -0.5f, 0.0f, //Bottom-left vertex
//            0.5f, -0.5f, 0.0f, //Bottom-right vertex
//            0.0f,  0.5f, 0.0f  //Top vertex
//        };
//        
//        float[] uvs = {
//            0.0f, 0.0f, //Bottom-left vertex
//            1.0f, 0.0f, //Bottom-right vertex
//            0.5f, 1.0f  //Top vertex
//        };

//todo combine multiple datas into single  vbo and auto space in vao
//todo vbo that is static, vbo that aint static and is changed a meme temps
//todo flyweight vbo (mesh) and also per instance vbo
        
        float[] vertices = //interweaving of position and tex coords into same vbo and array is done for perf reasons (less state-changes in opengl)
        {
            //Position          Texture coordinates
            0.5f,  0.5f, 0.0f, 1.0f, 1.0f, // top right
            0.5f, -0.5f, 0.0f, 1.0f, 0.0f, // bottom right
            -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, // bottom left
            -0.5f,  0.5f, 0.0f, 0.0f, 1.0f  // top left
        };

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
            GL.DebugMessageCallback(GLErrorListener, IntPtr.Zero);
            
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
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, Entry.Image.Width, Entry.Image.Width, 0, PixelFormat.Rgba, PixelType.UnsignedByte,Entry.TexturePixels.ToArray());
            #endregion
            
            #region vao
            VAOHandle = GL.GenVertexArray();
            GL.BindVertexArray(VAOHandle);
            GL.VertexAttribPointer(
                _shaderProgram.GetAttribLocation("in_position"), 
                3,
                VertexAttribPointerType.Float,
                false,
                sizeof(float) * 5,
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
            
            #region tex settings
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int) TextureWrapMode.Repeat); //tex wrap mode
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int) TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int) TextureMagFilter.Linear); //scaling up, tex interp
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int) TextureMagFilter.Linear); //scaling down
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

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit); //I think this only clears colour and not depth texture for isntance 
            
            _shaderProgram.Use();
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


        static void GLErrorListener (DebugSource source, DebugType type, int id, DebugSeverity severity, int length, IntPtr message, IntPtr param) 
        {
            Console.WriteLine($"___________ GL Error Callback _________");
            Console.WriteLine($"source: {source}");
            Console.WriteLine($"type: {type}");
            Console.WriteLine($"severity: {severity}");
            Console.WriteLine($"message: {Marshal.PtrToStringUTF8(message)}");
            Console.WriteLine($"userParam: {param}");
            Console.WriteLine($"___________ End of Callback _________");

        }
    }
}