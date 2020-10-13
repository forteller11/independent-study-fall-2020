using System;
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
        
        public NormalMaterial(CreateMaterials.MaterialType type, CreateFBOs.FBOType fboType, ShaderProgram shaderProgram, AttributeBuffer[] vaoAndBuffers, string diffusePath, string normalPath, string specularPath, Action<Material> perMatSender) : base(type, fboType, shaderProgram, perMatSender)
        {
            FeedBuffersAndCreateVAO(null, vaoAndBuffers);
            SetupSampler(DiffuseColorSampler, Texture.FromFile(diffusePath, TextureUnit.Texture0));
            SetupSampler(NormalMapSampler, Texture.FromFile(normalPath, TextureUnit.Texture1));
            SetupSampler(SpecularMapSampler, Texture.FromFile(specularPath, TextureUnit.Texture2));
        }
    }
}