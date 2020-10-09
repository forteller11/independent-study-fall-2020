using Indpendent_Study_Fall_2020.MaterialRelated;
using OpenTK.Graphics.OpenGL4;

namespace Indpendent_Study_Fall_2020.Scripts.Materials
{
    public class CreateFBOs
    {
        public static FBO[] Create()
        {
            var shadowBuffer = new FBO("Shadow", 680, 680, TextureUnit.Texture3);
            return new[]
            {
                shadowBuffer
            };
        }
    }
}