using Indpendent_Study_Fall_2020.MaterialRelated;
using OpenTK.Graphics.OpenGL4;

namespace Indpendent_Study_Fall_2020.Scripts.Materials
{
    public class CreateFBOs
    {
        public enum FBOName
        {
            Default = default,
            Shadow
        }
        public static FBO ShadowBuffer = new FBO(FBOName.Shadow, 680, 680, FramebufferAttachment.Color, TextureUnit.Texture3);
        
        public static FBO DefaultBuffer = new FBO();
        public static FBO[] Create()
        {

            return new[]
            {
                ShadowBuffer,
                DefaultBuffer
            };
        }
    }
}