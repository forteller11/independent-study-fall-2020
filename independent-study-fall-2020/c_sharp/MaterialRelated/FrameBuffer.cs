using System;
using OpenTK.Graphics.OpenGL4;

namespace Indpendent_Study_Fall_2020.MaterialRelated
{
    public class FrameBuffer
    {
        private readonly int Handle;

        public FrameBuffer(int width, int height)
        {
            Handle = GL.GenFramebuffer();
            Use();

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, width, height, 0, PixelFormat.Rgba,PixelType.UnsignedByte, IntPtr.Zero);
            ApplyTextureSettings();
        }

        public void Use()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, Handle);
        }
        public void ApplyTextureSettings()
        {
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int) TextureMagFilter.Linear); //scaling up, tex interp
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int) TextureMagFilter.Linear); //scaling down
        }
    }
}