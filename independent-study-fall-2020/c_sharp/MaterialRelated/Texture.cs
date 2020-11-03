using System;
using System.Security.Cryptography;
using OpenTK.Graphics.OpenGL4;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace CART_457.MaterialRelated
{
    public class Texture
    {

        public TextureUnit TextureUnit { get; private set; }
        
        public readonly int Handle;
        public bool IsStatic;
        public string Name { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        
        private byte[] _colors; //todo make array for perf

        private Image<Rgba32> _laborsImage;
        
        
        public Texture()
        {
            Handle = GL.GenTexture();
        }
        
        public static Texture FromFile(string fileName, TextureUnit textureUnit, Action texSettingsCustom=null) 
        {
            var texture = new Texture();
            texture.Name = fileName;
            texture.TextureUnit = textureUnit;
            texture.IsStatic = true;
            texture.Use();
            if (texSettingsCustom == null)
                StandardTextureSettings();
            else
                texSettingsCustom.Invoke();
            
            texture.LoadImage("\\"+fileName);
            texture.CookSixLaborsImageToByteArray();

            texture.UploadPixelArrayToGPU(PixelInternalFormat.Rgba, PixelFormat.Rgba, PixelType.UnsignedByte);

            return texture;
        }
        
        public static Texture GPURGBA(int width, int height, TextureUnit textureUnit)
        {
            var texture = EmptyFormatless(width, height, textureUnit, FrameBufferTextureSettings);
            texture.Name = "GPURGBA: " + textureUnit;
            texture.GenerateStorageOnGPU(PixelInternalFormat.Rgba, PixelFormat.Rgba, PixelType.UnsignedByte);
            
            return texture;
        }
        
        public static Texture GPULuminance(int width, int height, TextureUnit textureUnit)
        {
            var texture = EmptyFormatless(width, height, textureUnit, FrameBufferTextureSettings);
            texture.Name = "GPULumninance: " + textureUnit;
            texture.GenerateStorageOnGPU(PixelInternalFormat.Luminance, PixelFormat.Luminance, PixelType.UnsignedByte);
            
            return texture;
        }
        
        
        public static Texture GPUDepth(int width, int height, TextureUnit textureUnit)
        {
            var texture = EmptyFormatless(width, height, textureUnit, DepthTextureSettings);
            texture.Name = "GPUDepth: " + textureUnit;
            texture.GenerateStorageOnGPU(PixelInternalFormat.DepthComponent, PixelFormat.DepthComponent, PixelType.Float);
            
            return texture;
        }

        private static Texture EmptyFormatless(int width, int height, TextureUnit textureUnit, Action textureSettings)
        {
            var texture = new Texture();
            texture.TextureUnit = textureUnit;
            texture.IsStatic = false;
            texture.Use();
            textureSettings.Invoke();
            texture.Width = width;
            texture.Height = height;
            return texture;
        }
        

        public void LoadImage(string fileName)
        {
            string path = SerializationManager.TexturePath + fileName;
            _laborsImage = Image.Load<Rgba32>(path);
            _laborsImage.Mutate(x => x.Flip(FlipMode.Vertical)); //ImageSharp loads from the top-left pixel, whereas OpenGL loads from the bottom-left, causing the texture to be flipped vertically.
            Width  = _laborsImage.Width;
            Height = _laborsImage.Height;
        }

        public void CreateEmptyByteArray(int channels)
        {
            int area = Width * Height;
            _colors = new byte[area * channels];
        }
        
        public void CookSixLaborsImageToByteArray()
        {
            if (!_laborsImage.TryGetSinglePixelSpan(out var tempPixels))
                throw new Exception("Image Loading Error: Is Texture Corrupt?");

            int area = Width * Height;
            if (_colors == null || _colors.Length != area * 4) //init colors to proper length if not already
                _colors = new byte[area * 4];

            for (int i = 0; i < tempPixels.Length; i++)
            {
                int indexStart = i * 4;
                _colors[indexStart + 0] = tempPixels[i].R;
                _colors[indexStart + 1] = tempPixels[i].G;
                _colors[indexStart + 2] = tempPixels[i].B;
                _colors[indexStart + 3] = tempPixels[i].A;
            }
        }

        public void Use()
        {
            GL.ActiveTexture(TextureUnit);
            GL.BindTexture(TextureTarget.Texture2D, Handle);
        }

        public void UseAndGenerateMipMaps()
        {
            GL.ActiveTexture(TextureUnit);
            GL.BindTexture(TextureTarget.Texture2D, Handle);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
        }

        private void UploadPixelArrayToGPU(PixelInternalFormat pixelInternalFormat, PixelFormat pixelFormat, PixelType pixelType)
        {
            Use();
            GL.TexImage2D(TextureTarget.Texture2D, 0, pixelInternalFormat, Width, Height, 0, pixelFormat, pixelType, _colors);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
        }

        public void GenerateStorageOnGPU(PixelInternalFormat pixelInternalFormat, PixelFormat pixelFormat, PixelType pixelType)
        {
            Use();
            GL.TexImage2D(TextureTarget.Texture2D, 0, pixelInternalFormat, Width, Height, 0, pixelFormat, pixelType, IntPtr.Zero);
        }

        private static void StandardTextureSettings() //todo make it so doesn't always have to be rgba image
        {
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int) TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int) TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int) TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int) TextureMinFilter.NearestMipmapLinear);
        }

        public static void StandardNearestSettings()
        {
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int) TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int) TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int) TextureMagFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int) TextureMinFilter.Nearest);
        }

        private static void FrameBufferTextureSettings()
        {
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int) TextureWrapMode.ClampToBorder);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int) TextureWrapMode.ClampToBorder);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int) TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int) TextureMinFilter.NearestMipmapLinear);
        }

        private static void DepthTextureSettings()
        {
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)All.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)All.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)All.ClampToBorder);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)All.ClampToBorder);
            
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureCompareMode, (int) TextureCompareMode.CompareRToTexture );
            // GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureCompareFunc,TextureCompareFunc.None );
            // glTexParameteri( GL_TEXTURE2D, GL_TEXTURE_COMPARE_MODE_ARB, GL_COMPARE_R_TO_TEXTURE_ARB );
            // glTexParameteri( GL_TEXTURE2D, GL_TEXTURE_COMPARE_FUNC_ARB, GL_LEQUAL );

        }


    }
}