using System;
using Indpendent_Study_Fall_2020.Helpers;
using Indpendent_Study_Fall_2020.MaterialRelated;
using OpenTK.Graphics.OpenGL4;

namespace Indpendent_Study_Fall_2020.Scripts.Materials
{
    public class MaterialPreconfigs
    {
        public const string DiffuseColorSampler = Material.DIFFUSE_SAMPLER;
        public const string NormalMapSampler = Material.NORMAL_MAP_SAMPLER;
        public const string SpecularMapSampler = Material.SPECULAR_MAP_SAMPLER;
        public const string ShadowMapSampler = Material.SHADOW_MAP_SAMPLER;
        
        public static Material Normal(
            MaterialSetup.MaterialType type, 
            FboSetup.FBOID fboid, 
            ShaderProgram shaderProgram, 
            Mesh mesh, 
            string diffusePath, 
            string normalPath, 
            string specularPath, 
            Action<Material> perMatSender)
        {
            var mat = Material.EntityBased(type, fboid, shaderProgram, mesh, perMatSender);

            mat.SetupSampler(DiffuseColorSampler, Texture.FromFile(diffusePath, TextureUnit.Texture0));
            mat.SetupSampler(NormalMapSampler, Texture.FromFile(normalPath, TextureUnit.Texture1));
            mat.SetupSampler(SpecularMapSampler, Texture.FromFile(specularPath, TextureUnit.Texture2));
            mat.SetupSampler(ShadowMapSampler, FboSetup.Shadow.ColorTexture1);
            
            return mat;
        }
    }
}