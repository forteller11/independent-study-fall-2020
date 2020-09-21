using System;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;

namespace Indpendent_Study_Fall_2020
{
    public static class Debug
    {
        private enum PreviousDebugCall
        {
            Log = default,
            Warning,
            GLError
        }

        private static PreviousDebugCall PreviousCall;
        private static ConsoleColor PreviousConsoleBackgroundColor = ConsoleColor.Black;
        private static ConsoleColor DefaultForegroundColor = ConsoleColor.Gray;
        public static void LogWarning(string message)
        {
            DrawSeperatorConditionally(PreviousDebugCall.Warning);
            Console.ForegroundColor = ConsoleColor.Yellow;
            
            Console.WriteLine("Warning: " + message.ToString());
            
            Console.ForegroundColor = DefaultForegroundColor;
        }
        
        public static void Log(object message)
        {
            DrawSeperatorConditionally(PreviousDebugCall.Log);
            Console.ForegroundColor = ConsoleColor.White;
            
            Console.WriteLine(message.ToString());
            
            Console.ForegroundColor = DefaultForegroundColor;
        }
        
        static public void GLErrorCallback (DebugSource source, DebugType type, int id, DebugSeverity severity, int length, IntPtr message, IntPtr param)
        {
            DrawSeperatorConditionally(PreviousDebugCall.GLError);
            
            ConsoleColor foregroundColor;
            switch (severity)
            {
                case DebugSeverity.DebugSeverityNotification:
                    foregroundColor = ConsoleColor.DarkGray;
                    break;
                case DebugSeverity.DebugSeverityMedium:
                    foregroundColor = ConsoleColor.Yellow;
                    break;
                case DebugSeverity.DebugSeverityHigh:
                    foregroundColor = ConsoleColor.Red;
                    break;
                default:
                    foregroundColor = ConsoleColor.Gray;
                    break;
            }
            Console.ForegroundColor = foregroundColor;
            
            Console.WriteLine($"source: {source}");
            Console.WriteLine($"type: {type}");
            Console.WriteLine($"severity: {severity}");
            Console.WriteLine($"message: {Marshal.PtrToStringUTF8(message)}");

            Console.ForegroundColor = DefaultForegroundColor;
        }

        private static void DrawSeperatorConditionally(PreviousDebugCall currentCall)
        {
            if (PreviousCall != PreviousDebugCall.Log)
            {
                Console.BackgroundColor = ConsoleColor.DarkGray;
//                Console.WriteLine(new string(' ', 98));
                Console.WriteLine("");
                Console.BackgroundColor = ConsoleColor.Black;
            }

            PreviousCall = currentCall;
        }
    }
}