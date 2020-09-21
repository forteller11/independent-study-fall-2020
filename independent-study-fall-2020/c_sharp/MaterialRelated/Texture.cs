using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Metadata;
using OpenTK.Graphics.OpenGL4;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Indpendent_Study_Fall_2020
{
    public class Texture : Uniform
    {
        public string ShaderName { get; set; }
        public TextureUnit TextureUnit;
        
        public readonly int Handle;
        
        public byte[] Colors; //todo make array for perf
        
        public Image<Rgba32> LaborsImage;
        public int Width => LaborsImage.Width;
        public int Height => LaborsImage.Height;
        public int Area => Width * Height;

        public Texture(string fileName, string shaderName, TextureUnit textureUnit, bool cookOnLoad = true) //turn cookOnLoad off if you're going to manipulate image on cpu before uploading to openGL
        {
            Handle = GL.GenTexture();
            ShaderName = shaderName;
            TextureUnit = textureUnit;
            
            Use();
            ApplyTextureSettings();
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

        public void Use()
        {
       
            GL.ActiveTexture(TextureUnit);
            GL.BindTexture(TextureTarget.Texture2D, Handle);
        }
        
        public void UploadToShader()
        {
            Use();
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, Width, Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, Colors);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
        }

        private void ApplyTextureSettings()
        {
            Use();
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int) TextureWrapMode.Repeat); //tex wrap mode
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int) TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int) TextureMagFilter.Linear); //scaling up, tex interp
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int) TextureMagFilter.Linear); //scaling down
        }


    }
}