using System;
using Indpendent_Study_Fall_2020.Helpers;
using OpenTK.Graphics.OpenGL4;

//watch thinmatrix videos
//how to draw to buffer in shaders?
//setup test shader
namespace Indpendent_Study_Fall_2020.MaterialRelated
{
    public class FBO : IUniqueName
    {
        public readonly int Handle = 0;
        public readonly string Name;
        public Texture Texture { get; private set; }

        public FBO(string name, int width, int height, FramebufferAttachment attachment, TextureUnit textureUnit)
        {
            Handle = GL.GenFramebuffer();
            Name = name;
            Use();
            AssignTexture(Texture.Empty(width, height, textureUnit), attachment);
        }

        /// <summary>
        /// Creates default fbo
        /// </summary>
        public FBO()
        {
            Name = "default";
            Handle = 0;
            Texture = null;
        }

        public void Use()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, Handle);
        }

        public void PrepareForDrawing()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, Handle);
        }

        public static void UseDefaultBuffer()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }
    
        
        public void AssignTexture(Texture texture, FramebufferAttachment attachment)
        {
            Texture = texture;
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, attachment, TextureTarget.Texture2D, texture.Handle, 0);
        }

        public string GetUniqueName() => Name;
    }
}