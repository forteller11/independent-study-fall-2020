using System;
using System.Collections.Generic;
using System.Drawing;
using CART_457.Helpers;
using CART_457.Renderer;
using CART_457.Scripts;
using OpenTK.Graphics.OpenGL4;

//watch thinmatrix videos
//how to draw to buffer in shaders?
//setup test shader
namespace CART_457.MaterialRelated
{
    public class FBO 
    {
        public int Handle { get; private set; }
        public string Name;
        public Size Size { get; private set; }

        public Action RenderCapSettings;
        public Camera Camera;
        public Texture ColorTexture1 { get; private set; }
        public Texture ColorTexture2 { get; private set; }
        public Texture DepthTexture { get; private set; }

        public ClearBufferMask ClearBufferBit;

        private FBO() { }
        public static FBO Custom( string name, Size size, Camera mainCamera, bool colorAttachment1, bool colorAttachment2, bool depthAttachment, ClearBufferMask clearBufferBit, Action renderCapSettings)
        {
            var fbo = new FBO();
            fbo.Handle = GL.GenFramebuffer();
            
            fbo.Name = name;
            fbo.Size = size;
            fbo.Camera = mainCamera;
            fbo.RenderCapSettings = renderCapSettings;
            fbo.ClearBufferBit = clearBufferBit;
            
            if (colorAttachment1)
                fbo.AddColorAttachment1();
            
            if (colorAttachment2)
                fbo.AddColorAttachment2();
            
            if (depthAttachment)
                fbo.AddDepthAttachment();
            
            return fbo;
        }

        public static FBO Default(string name, Action renderCapSettings) //texture is main viewport
        {
            var fbo = new FBO();
            fbo.Name = name;
            fbo.Handle = 0;
            fbo.RenderCapSettings = renderCapSettings;
            fbo.Size = DrawManager.TKWindowSize;
            return fbo;
        }
        

        private static void ValidateAttachments()
        {
            var fboStatus = GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer);
            if (fboStatus != FramebufferErrorCode.FramebufferComplete)
                throw new Exception($"Frame Buffer Exception! {fboStatus}");
        }

        public void AddColorAttachment1()
        {
            Bind();
            ColorTexture1 = Texture.EmptyRGBA(Size.Width, Size.Height, TextureUnit.Texture3);
            LinkTexture(ColorTexture1, FramebufferAttachment.ColorAttachment0);
            ValidateAttachments();
        }
        
        public void AddColorAttachment2()
        {
            Bind();
            ColorTexture2 = Texture.EmptyRGBA(Size.Width, Size.Height, TextureUnit.Texture4);
            LinkTexture(ColorTexture2, FramebufferAttachment.ColorAttachment1);
            ValidateAttachments();
        }
        
        public void AddDepthAttachment()
        {
            Bind();
            DepthTexture = Texture.EmptyDepth(Size.Width, Size.Height, TextureUnit.Texture5);
            LinkTexture(DepthTexture, FramebufferAttachment.DepthAttachment);
            ValidateAttachments();
        }
        

        public void Bind()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, Handle);
        }
        
        public void Clear()
        {
            GL.Clear(ClearBufferBit);
        }
        
        private void SetDrawBuffers()
        {
            if (Handle == 0)
                return;
            var drawBufferEnums = new List<DrawBuffersEnum>();
            if (ColorTexture1 != null) drawBufferEnums.Add(DrawBuffersEnum.ColorAttachment0);
            if (ColorTexture2 != null) drawBufferEnums.Add(DrawBuffersEnum.ColorAttachment1);
            GL.DrawBuffers(drawBufferEnums.Count, drawBufferEnums.ToArray());
        }

        public void SetDrawingStates()
        {
            Bind();
            Clear();

            SetDrawBuffers();
            
            GL.Viewport(0,0,Size.Width,Size.Height);
            RenderCapSettings?.Invoke();
        }

        

        public void UseTexturesAndGenerateMipMaps() //todo cache gen mip maps with dirty-pattern
        {
            ColorTexture1?.UseAndGenerateMipMaps();
            ColorTexture2?.UseAndGenerateMipMaps();
            DepthTexture?.UseAndGenerateMipMaps();
        }


        
        public void LinkTexture(Texture texture, FramebufferAttachment attachment)
        {
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, attachment, TextureTarget.Texture2D, texture.Handle, 0);
        }
        
        public static void Blit(FBO source, FBO dest, ClearBufferMask clearBufferMask, BlitFramebufferFilter blitFramebufferFilter)
        {
            GL.BindFramebuffer(FramebufferTarget.ReadFramebuffer, source.Handle);
            GL.BindFramebuffer(FramebufferTarget.DrawFramebuffer, dest.Handle);
            GL.BlitFramebuffer(
                0, 0, source.Size.Width, source.Size.Height,
                0, 0, dest.Size.Width, dest.Size.Height,
                clearBufferMask, blitFramebufferFilter);
        }
        
    }
}