using System;
using Indpendent_Study_Fall_2020.Helpers;
using OpenTK.Graphics.OpenGL4;

//watch thinmatrix videos
//how to draw to buffer in shaders?
//setup test shader
namespace Indpendent_Study_Fall_2020.MaterialRelated
{
    public class FBO 
    {
        public readonly int Handle;
        public readonly string Name;
        public Texture Texture { get; private set; }

        public FBO(string name, int width, int height, TextureUnit textureUnit)
        {
            Handle = GL.GenFramebuffer();
            Name = name;
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