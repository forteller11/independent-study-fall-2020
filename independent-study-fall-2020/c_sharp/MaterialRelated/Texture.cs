using System;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using TextureCompareMode = OpenTK.Graphics.OpenGL.TextureCompareMode;

namespace Indpendent_Study_Fall_2020.MaterialRelated
{
    public class Texture
    {

        public TextureUnit TextureUnit { get; private set; }

        public readonly int Handle;
        
        public byte[] Colors; //todo make array for perf
        public int[] Depth;
        
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
        public static Texture FromFile(string fileName, TextureUnit textureUnit) //turn cookOnLoad off if you're going to manipulate image on cpu before uploading to openGL
        {
            var texture = new Texture();
            texture.TextureUnit = textureUnit;
            texture.Use();
            StandardTextureSettings();
            texture.LoadImage(fileName);
            texture.CookSixLaborsImageToByteArray();

            texture.UploadToShader(PixelInternalFormat.Rgba, PixelFormat.Rgba, PixelType.UnsignedByte, false);

            return texture;
        }
        
        public static Texture EmptyRGBA(int width, int height, TextureUnit textureUnit)
        {
            var texture = EmptyFormatless(width, height, textureUnit, 4, StandardTextureSettings, false);
            texture.UploadToShader(PixelInternalFormat.Rgba, PixelFormat.Rgba, PixelType.UnsignedByte, false);
            
            return texture;
        }
        
        public static Texture EmptyLuminance(int width, int height, TextureUnit textureUnit)
        {
            var texture = EmptyFormatless(width, height, textureUnit, 1, StandardTextureSettings, false);
            texture.UploadToShader(PixelInternalFormat.Luminance, PixelFormat.Luminance, PixelType.UnsignedByte, false);
            
            return texture;
        }
        
        public static Texture EmptyDepth(int width, int height, TextureUnit textureUnit)
        {
            var texture = EmptyFormatless(width, height, textureUnit, 1, DepthTextureSettings, true);
            texture.UploadToShader(PixelInternalFormat.DepthComponent, PixelFormat.DepthComponent, PixelType.Float, true);
            
            return texture;
        }

        private static Texture EmptyFormatless(int width, int height, TextureUnit textureUnit, int channels, Action textureSettings, bool int32)
        {
            var texture = new Texture();
            texture.TextureUnit = textureUnit;
            texture.Use();
            textureSettings.Invoke();
            texture.Width = width;
            texture.Height = height;
            texture.CreateEmptyByteArray(channels, int32);
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

        public void CreateEmptyByteArray(int channels, bool int32)
        {
            int area = Width * Height;
            if (int32)
                Depth = new int[area * channels];
            else
                Colors = new byte[area * channels];
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
        
        public void UploadToShader(PixelInternalFormat pixelInternalFormat, PixelFormat pixelFormat, PixelType pixelType, bool depth)
        {

            
            Use();
            if (depth == false)
            {
                GL.TexImage2D(TextureTarget.Texture2D, 0, pixelInternalFormat, Width, Height, 0, pixelFormat, pixelType, Colors);
                GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            }
            else
            {
                GL.TexImage2D(TextureTarget.Texture2D, 0, pixelInternalFormat, Width, Height, 0, pixelFormat, pixelType, Depth);
            }
        }

        private static void StandardTextureSettings() //todo make it so doesn't always have to be rgba image
        {
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int) TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int) TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int) TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int) TextureMinFilter.NearestMipmapLinear);
        }

        private static void DepthTextureSettings()
        {
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)All.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)All.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)All.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)All.Repeat);
            
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureCompareMode, (int) TextureCompareMode.CompareRToTexture );
            // GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureCompareFunc,TextureCompareFunc.None );
            // glTexParameteri( GL_TEXTURE2D, GL_TEXTURE_COMPARE_MODE_ARB, GL_COMPARE_R_TO_TEXTURE_ARB );
            // glTexParameteri( GL_TEXTURE2D, GL_TEXTURE_COMPARE_FUNC_ARB, GL_LEQUAL );

        }


    }
}