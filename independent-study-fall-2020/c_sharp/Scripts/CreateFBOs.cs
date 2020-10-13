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
        
        public static FBO[] Create()
        {
        
            var shadowBuffer = new FBO(FBOType.Shadow, 640, 640, FramebufferAttachment.ColorAttachment0, TextureUnit.Texture3);
            var defaultBuffer = new FBO();
            
            return new[]
            {
                shadowBuffer,
                defaultBuffer
            };
        }
    }
}