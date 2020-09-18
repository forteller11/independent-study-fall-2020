using System;
using System.Collections.Generic;
using System.Diagnostics;
using OpenTK.Graphics.OpenGL4;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Indpendent_Study_Fall_2020
{
    public class Texture
    {
        public byte[] Colors; //todo make array for perf
        public Image<Rgba32> LaborsImage;
        public int Width => LaborsImage.Width;
        public int Height => LaborsImage.Height;
        public int Area => Width * Height;

        public Texture(string fileName, bool cookOnLoad = true)
        {
            LoadImage(fileName);
            if (cookOnLoad)
                CookImageToByteArray();
        }

        public void LoadImage(string fileName)
        {
            string path = SerializationManager.AssetPath + fileName;
            LaborsImage = Image.Load<Rgba32>(path);
            LaborsImage.Mutate(x => x.Flip(FlipMode.Vertical)); //ImageSharp loads from the top-left pixel, whereas OpenGL loads from the bottom-left, causing the texture to be flipped vertically.
        }

        public void CookImageToByteArray()
        {
            if (!LaborsImage.TryGetSinglePixelSpan(out var tempPixels))
                throw new Exception("Image Loading Error: Is Texture Corrupt?");
            
            if (Colors == null || Colors.Length != Area * 4) //init colors to proper length if not already
                Colors = new byte[Area * 4];

            for (int i = 0; i < tempPixels.Length; i++)
            {
                int indexStart = i * 4;
                Colors[indexStart + 0] = tempPixels[i].R;
                Colors[indexStart + 1] = tempPixels[i].G;
                Colors[indexStart + 2] = tempPixels[i].B;
                Colors[indexStart + 3] = tempPixels[i].A;
            }
        }

        public void SendToOpenGL()
        {
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, Width, Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, Colors);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
        }

        public void BindToUniform(TextureUnit unit, ShaderProgram shader, string attribName, int texOrder)
        {
            GL.ActiveTexture(unit);
            shader.SetUniform(attribName, texOrder);
            GL.BindTexture(TextureTarget.Texture2D, texOrder);
        }
        
        public void Use(TextureUnit unit, int texOrder)
        {
            GL.ActiveTexture(unit);
            GL.BindTexture(TextureTarget.Texture2D, texOrder);
        }
    }
}