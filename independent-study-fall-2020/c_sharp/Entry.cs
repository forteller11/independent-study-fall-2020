using System;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;

// alot of code is taken from the openTK tutorials here: https://opentk.net/learn

namespace Indpendent_Study_Fall_2020
{
    static class Entry
    {
        private static void Main(string[] _)
        {
            Console.WriteLine("Hello World!");

            var glWindow = TKWindow.CreateAndRun();
        }
    }
}