using OpenTK.Graphics.OpenGL4;

namespace Indpendent_Study_Fall_2020.MaterialRelated.Textures
{
    public interface ITexture
    {
        TextureUnit TextureUnit { get; }
        void Use();
        void UploadToShader();
    }
}