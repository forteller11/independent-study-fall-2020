using System;
using System.Drawing;
using Indpendent_Study_Fall_2020.EntitySystem;
using Indpendent_Study_Fall_2020.Helpers;
using Indpendent_Study_Fall_2020.Scripts.Materials;
using OpenTK.Graphics.OpenGL4;

//watch thinmatrix videos
//how to draw to buffer in shaders?
//setup test shader
namespace Indpendent_Study_Fall_2020.MaterialRelated
{
    public class FBO : ITypeID
    {
        public int Handle { get; private set; }
        public CreateFBOs.FBOType Type { get; private set; }
        private Action _renderCapSettings;

        public int Width => Texture?.Width ?? DrawManager.TKWindowSize.Width;
        public int Height => Texture?.Height ?? DrawManager.TKWindowSize.Height;
        public int TextureHandle => Texture.Handle;
        private Texture Texture { get; set; }

        private FBO()
        {
            
        }
        public static FBO Custom(CreateFBOs.FBOType type, FramebufferAttachment attachment, Texture texture, Action renderCapSettings)
        {
            var fbo = new FBO();
            fbo.Handle = GL.GenFramebuffer();
            fbo.Type = type;
            fbo._renderCapSettings = renderCapSettings;
            
            fbo.Use();
            fbo.AssignTexture(texture, attachment);
            
            var fboStatus = GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer);
            if (fboStatus != FramebufferErrorCode.FramebufferComplete)
                throw new Exception($"Frame Buffer Exception! {fboStatus}");

            return fbo;
        }
        
        public static FBO Default(Action renderCapSettings) //texture is main viewport
        {
            var fbo = new FBO();
            fbo.Type = CreateFBOs.FBOType.Default;
            fbo.Handle = 0;
            fbo.Texture = null;
            fbo._renderCapSettings = renderCapSettings;
            return fbo;
        }

        public void Use()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, Handle);
        }

        public void SetDrawingStates()
        {
            Use();
            if (Type == CreateFBOs.FBOType.Default)
                GL.Viewport(DrawManager.TKWindowSize);
            else
                GL.Viewport(0,0,Texture.Width,Texture.Height);
            _renderCapSettings?.Invoke();
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
        
        public static void Blit(FBO source, FBO dest, ClearBufferMask clearBufferMask, BlitFramebufferFilter blitFramebufferFilter)
        {
            GL.BindFramebuffer(FramebufferTarget.ReadFramebuffer, source.Handle);
            GL.BindFramebuffer(FramebufferTarget.DrawFramebuffer, dest.Handle);
            GL.BlitFramebuffer(
                0, 0, source.Width, source.Height,
                0, 0, dest.Width, dest.Height,
                clearBufferMask, blitFramebufferFilter);
        }

        public int GetTypeID() => (int) Type;
    }
}