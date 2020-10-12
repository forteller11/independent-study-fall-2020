using System;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Indpendent_Study_Fall_2020.MaterialRelated
{
    public class Texture
    {

        public TextureUnit TextureUnit;
        
        public readonly int Handle;
        
        public byte[] Colors; //todo make array for perf
        
        public Image<Rgba32> LaborsImage;
        public int Width { get; private set; }
        public int Height { get; private set; }
        
        public Texture()
        {
            Handle = GL.GenTexture();
        }
  
        /// <summary>
        /// loads file using sixlabors library... turn cookOnLoad off if you're going to manipulate image on cpu before uploading to openGL
        /// </summary>
        public static Texture FromFile(string fileName, TextureUnit textureUnit, bool cookOnLoad = true) //turn cookOnLoad off if you're going to manipulate image on cpu before uploading to openGL
        {
            var texture = new Texture();

            texture.TextureUnit = textureUnit;
            texture.Use();
            texture.ApplyTextureSettings();
            texture.LoadImage(fileName);

            if (cookOnLoad)
            {
                texture.CookSixLaborsImageToByteArray();
                texture.UploadToGPUTextureUnit();
            }

            return texture;
        }
        
        public static Texture Empty(int width, int height, TextureUnit textureUnit)
        {
            var texture = new Texture();

            texture.TextureUnit = textureUnit;
            texture.Use();
            texture.ApplyTextureSettings();
            texture.Width = width;
            texture.Height = height;
            texture.CreateEmptyByteArray();
            
            texture.UploadToGPUTextureUnit();

            return texture;
        }
        

        public void LoadImage(string fileName)
        {
            string path = SerializationManager.TexturePath + fileName;
            LaborsImage = Image.Load<Rgba32>(path);
            LaborsImage.Mutate(x => x.Flip(FlipMode.Vertical)); //ImageSharp loads from the top-left pixel, whereas OpenGL loads from the bottom-left, causing the texture to be flipped vertically.
            Width  = LaborsImage.Width;
            Height = LaborsImage.Height;
        }

        public void CreateEmptyByteArray()
        {
            int area = Width * Height;
            Colors = new byte[area * 4];
        }
        
        public void CookSixLaborsImageToByteArray()
        {
            if (!LaborsImage.TryGetSinglePixelSpan(out var tempPixels))
                throw new Exception("Image Loading Error: Is Texture Corrupt?");

            int area = Width * Height;
            if (Colors == null || Colors.Length != area * 4) //init colors to proper length if not already
                Colors = new byte[area * 4];

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
        
        public void UploadToGPUTextureUnit()
        {
            Use();
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, Width, Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, Colors);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
        }

        private void ApplyTextureSettings() //todo make it so doesn't always have to be rgba image
        {
            Use();
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int) TextureWrapMode.Repeat); //tex wrap mode
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int) TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int) TextureMagFilter.Linear); //scaling up, tex interp
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int) TextureMagFilter.Linear); //scaling down
        }


    }
}