using System;
using System.Runtime.InteropServices;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

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
            
            newTKWindow.Run();

            return newTKWindow;
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