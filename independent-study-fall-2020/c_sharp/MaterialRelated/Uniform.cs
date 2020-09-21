namespace Indpendent_Study_Fall_2020
{
    public interface Uniform
    {
        string ShaderName { get; set; }
        void UploadToShader();
        void Use();
    }
}