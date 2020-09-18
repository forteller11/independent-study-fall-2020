using System;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL4;

namespace Indpendent_Study_Fall_2020
{
    public static class Debug
    {
        private static ConsoleColor PreviousConsoleBackgroundColor = ConsoleColor.Black;
        private static ConsoleColor DefaultForegroundColor = ConsoleColor.Gray;
        public static void LogWarning(string message)
        {
            DrawSeperator();
            Console.ForegroundColor = ConsoleColor.Yellow;
            
            Console.WriteLine("Warning: " + message);
            
            Console.ForegroundColor = DefaultForegroundColor;
        }
        
        static public void GLErrorCallback (DebugSource source, DebugType type, int id, DebugSeverity severity, int length, IntPtr message, IntPtr param)
        {
            DrawSeperator();
            
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
//            Console.WriteLine($"userParam: {param}");

            Console.ForegroundColor = DefaultForegroundColor;
        }

        private static void DrawSeperator()
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("                                                                                                                                              ");
            Console.BackgroundColor = ConsoleColor.Black;
//            ConsoleColor newColor = 
//                PreviousConsoleBackgroundColor == ConsoleColor.Black
//                    ? ConsoleColor.White
//                    : ConsoleColor.Black;
//            Console.BackgroundColor = newColor;
//            PreviousConsoleBackgroundColor = newColor;
        }
    }
}