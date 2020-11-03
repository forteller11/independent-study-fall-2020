using System;
using System.Collections.Generic;
using System.Drawing;
using CART_457.Helpers;
using CART_457.Renderer;
using CART_457.Scripts;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

//watch thinmatrix videos
//how to draw to buffer in shaders?
//setup test shader
namespace CART_457.MaterialRelated
{
    public class FBO 
    {
        public int Handle { get; private set; }
        public string Name;
        public Vector2i Size { get; private set; }

        public Action RenderCapSettings;
        public Camera Camera;
        public Texture ColorTexture1 { get; private set; }
        public Texture ColorTexture2 { get; private set; }
        public Texture DepthTexture { get; private set; }

        public ClearBufferMask ClearBufferBit;

        private FBO() { }
        //serial in that the next fbos will automatically available have access to this fbos render outputs
        public static FBO Serial( string name, Vector2i size, Camera mainCamera, bool colorAttachment1, bool colorAttachment2, bool depthAttachment, ClearBufferMask clearBufferBit, Action renderCapSettings)
        {
            var fbo = new FBO();
            fbo.Handle = GL.GenFramebuffer();
            
            fbo.Name = name;
            fbo.Size = size;
            fbo.Camera = mainCamera;
            fbo.RenderCapSettings = renderCapSettings;
            fbo.ClearBufferBit = clearBufferBit;
            
            if (colorAttachment1)
                fbo.AddColorAttachment1(TextureUnit.Texture3);
            
            if (colorAttachment2)
                fbo.AddColorAttachment2(TextureUnit.Texture4);
            
            if (depthAttachment)
                fbo.AddDepthAttachment(TextureUnit.Texture5);
            
            return fbo;
        }
        
        //custom in that the output  textureunits can be manually assigned
        public static FBO Custom( string name, Vector2i size, Camera mainCamera, 
            TextureUnit? colorAttachment01TextureUnit, TextureUnit? colorAttachment02TextureUnit, TextureUnit? depthAttachment01TextureUnit, 
            ClearBufferMask clearBufferBit, Action renderCapSettings)
        {
            var fbo = new FBO();
            fbo.Handle = GL.GenFramebuffer();
            
            fbo.Name = name;
            fbo.Size = size;
            fbo.Camera = mainCamera;
            fbo.RenderCapSettings = renderCapSettings;
            fbo.ClearBufferBit = clearBufferBit;
            
            if (colorAttachment01TextureUnit != null)
                fbo.AddColorAttachment1(colorAttachment01TextureUnit.Value);
            
            if (colorAttachment02TextureUnit != null)
                fbo.AddColorAttachment2(colorAttachment02TextureUnit.Value);
            
            if (depthAttachment01TextureUnit != null)
                fbo.AddDepthAttachment(depthAttachment01TextureUnit.Value);
            
            return fbo;
        }

        //more of an interface, represents main viewport controlled by openGL
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

        public void AddColorAttachment1(TextureUnit textureUnit)
        {
            Bind();
            ColorTexture1 = Texture.GPURGBA(Size.X, Size.Y, textureUnit);
            LinkTexture(ColorTexture1, FramebufferAttachment.ColorAttachment0);
            ValidateAttachments();
        }
        
        public void AddColorAttachment2(TextureUnit textureUnit)
        {
            Bind();
            ColorTexture2 = Texture.GPURGBA(Size.X, Size.Y, textureUnit);
            LinkTexture(ColorTexture2, FramebufferAttachment.ColorAttachment1);
            ValidateAttachments();
        }
        
        public void AddDepthAttachment(TextureUnit textureUnit)
        {
            Bind();
            DepthTexture = Texture.GPUDepth(Size.X, Size.Y, textureUnit);
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
            
            GL.Viewport(0,0,Size.X,Size.Y);
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
        
        public static void Blit(FBO source, FBO dest, ReadBufferMode readBufferAttachment, ClearBufferMask clearBufferMask, BlitFramebufferFilter blitFramebufferFilter)
        {
            
            GL.BindFramebuffer(FramebufferTarget.ReadFramebuffer, source.Handle);
            GL.BindFramebuffer(FramebufferTarget.DrawFramebuffer, dest.Handle);
            GL.ReadBuffer(readBufferAttachment);
            GL.BlitFramebuffer(
                0, 0, source.Size.X, source.Size.Y,
                0, 0, dest.Size.X, dest.Size.Y,
                clearBufferMask, blitFramebufferFilter);
        }
        
    }
}