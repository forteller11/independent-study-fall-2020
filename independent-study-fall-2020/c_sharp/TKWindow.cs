using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;

namespace Indpendent_Study_Fall_2020
{
    public class TKWindow : GameWindow
    {
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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            
            GL.ClearColor(1f,0f,1f,1f);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            var keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Key.Escape))
                Exit();
            
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit); //I think this only clears colour and not depth texture for isntance 
            
            base.OnUpdateFrame(e);
            
            
            Context.SwapBuffers();

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