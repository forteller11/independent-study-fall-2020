﻿using System;
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
        public readonly int Handle = 0;
        public readonly CreateFBOs.FBOType Type;
        private Action _renderCapSettings;
        public Texture Texture { get; private set; }

        public FBO(CreateFBOs.FBOType type, FramebufferAttachment attachment, Texture texture, Action renderCapSettings)
        {
            Handle = GL.GenFramebuffer();
            Type = type;
            _renderCapSettings = renderCapSettings;
            
            Use();
            AssignTexture(texture, attachment);
            
            var fboStatus = GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer);
            if (fboStatus != FramebufferErrorCode.FramebufferComplete)
                throw new Exception($"Frame Buffer Exception! {fboStatus}");


        }

        /// <summary>
        /// Creates default fbo
        /// </summary>
        public FBO(Action renderCapSettings)
        {
            Type = CreateFBOs.FBOType.Default;
            Handle = 0;
            Texture = null;
            _renderCapSettings = renderCapSettings;
        }

        public void Use()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, Handle);
        }

        public void SetDrawingStates()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, Handle);
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

        public int GetTypeID() => (int) Type;
    }
}