using Indpendent_Study_Fall_2020.MaterialRelated;
using OpenTK.Graphics.OpenGL4;

namespace Indpendent_Study_Fall_2020.Scripts.Materials
{
    public class CreateFBOs
    {
        public static FBO ShadowBuffer = new FBO("Shadow", 680, 680, FramebufferAttachment.Color, TextureUnit.Texture3);
        public static FBO[] Create()
        {

            return new[]
            {
                ShadowBuffer
            };
        }
    }
}