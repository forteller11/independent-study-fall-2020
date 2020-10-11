using Indpendent_Study_Fall_2020.Helpers;
using Indpendent_Study_Fall_2020.MaterialRelated;
using OpenTK.Graphics.OpenGL4;

namespace Indpendent_Study_Fall_2020.Scripts.Materials
{
    public class NormalMaterial : Material
    {
        public const string DiffuseColorSampler = "Color";
        public const string NormalMapSampler = "Normal";
        public const string SpecularMapSampler = "Gloss";
        // public const string FrameBufferSampler = "Buffer";
        
        public NormalMaterial(string name, ShaderProgram shaderProgram, AttributeBuffer[] vaoAndBuffers, string diffuseTexture, string normalMap, string specularMap) : base(name, shaderProgram)
        {
            FeedBuffersAndCreateVAO(null, vaoAndBuffers);
            SetupAndAttachTexture(diffuseTexture, DiffuseColorSampler, TextureUnit.Texture0);
            SetupAndAttachTexture(normalMap, NormalMapSampler, TextureUnit.Texture1);
            SetupAndAttachTexture(specularMap, SpecularMapSampler, TextureUnit.Texture2);
        }
    }
}