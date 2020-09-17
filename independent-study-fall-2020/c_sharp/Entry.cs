using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        
        public static List<byte> TexturePixels;
        public static Image<Rgba32> Image;
        private static void Main(string[] _)
        {

             
            
             #region image loading, manipulation, and conversion to byte array
             //Load the image
             string path = SerializationManager.AssetPath + "unwrap_helper.jpg";
             Image =  SixLabors.ImageSharp.Image.Load<Rgba32>(path);

            //ImageSharp loads from the top-left pixel, whereas OpenGL loads from the bottom-left, causing the texture to be flipped vertically.
            //This will correct that, making the texture display properly.
            Image.Mutate(x => x.Flip(FlipMode.Vertical));

            //Get an array of the pixels, in ImageSharp's internal format.
            Image.TryGetSinglePixelSpan(out var tempPixels);

            //Convert ImageSharp's format into a byte array, so we can use it with OpenGL.

            foreach (Rgba32 p in tempPixels)
             { 
                 //Console.Write(p.R + "|");
                 TexturePixels.Add(p.R);
                 TexturePixels.Add(p.G);
                 TexturePixels.Add(p.B);
                 TexturePixels.Add(p.A);
             }

             Debug.Assert(TexturePixels.Count / 4 == Image.Height * Image.Width);
             #endregion
             
             var glWindow = TKWindow.CreateAndRun();
        }
    }
}