using System;
using System.Collections.Generic;
using System.IO;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

// alot of code is taken from the openTK tutorials here: https://opentk.net/learn

namespace Indpendent_Study_Fall_2020
{
    static class Entry
    {
        
        private static byte[] texturePixels;
        private static void Main(string[] _)
        {

//             var glWindow = TKWindow.CreateAndRun();
            
             
             //Load the image
             string path = SerializationManager.AssetPath + "unwrap_helper.jpg";
             var image =  Image.Load<Rgba32>(path);

//ImageSharp loads from the top-left pixel, whereas OpenGL loads from the bottom-left, causing the texture to be flipped vertically.
//This will correct that, making the texture display properly.
             image.Mutate(x => x.Flip(FlipMode.Vertical));

//Get an array of the pixels, in ImageSharp's internal format.
             image.TryGetSinglePixelSpan(out var tempPixels);

//Convert ImageSharp's format into a byte array, so we can use it with OpenGL.
             List<byte> pixels = new List<byte>();

             foreach (Rgba32 p in tempPixels)
             {
//                 Console.Write(p.R + "|");
                 pixels.Add(p.R);
                 pixels.Add(p.G);
                 pixels.Add(p.B);
                 pixels.Add(p.A);
             }
        }
    }
}