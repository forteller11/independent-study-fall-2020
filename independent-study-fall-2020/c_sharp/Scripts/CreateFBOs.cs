using Indpendent_Study_Fall_2020.MaterialRelated;
using OpenTK.Graphics.OpenGL4;

namespace Indpendent_Study_Fall_2020.Scripts.Materials
{
    public class CreateFBOs
    {
        public enum FBOType
        {
            Default = default,
            Shadow
        };
        
        public static FBO ShadowBuffer; 
        public static FBO[] Create()
        {
        
            ShadowBuffer = new FBO(FBOType.Shadow, 2560,2560, FramebufferAttachment.ColorAttachment0, PixelInternalFormat.Rgba, TextureUnit.Texture3);
            var defaultBuffer = new FBO();
            
            return new[]
            {
                ShadowBuffer,
                defaultBuffer
            };
        }
    }
}