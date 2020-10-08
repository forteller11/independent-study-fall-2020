using System;
using OpenTK.Graphics.OpenGL4;

namespace Indpendent_Study_Fall_2020.MaterialRelated
{
    public class FrameBuffer
    {
        private readonly int Handle;
        public Texture Texture { get; private set; }

        public FrameBuffer(int width, int height, TextureUnit textureUnit)
        {
            Handle = GL.GenFramebuffer();
            Use();
            AssignTexture(Texture.Empty(width, height, textureUnit));
        }

        public void Use()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, Handle);
        }
        
        public void AssignTexture(Texture texture)
        {
            Texture = texture;
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.Color, TextureTarget.Texture2D, texture.Handle, 0);
        } 

    }
}