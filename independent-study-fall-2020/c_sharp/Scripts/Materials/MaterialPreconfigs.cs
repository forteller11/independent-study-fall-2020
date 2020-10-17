using System;
using CART_457.MaterialRelated;
using CART_457.Helpers;
using OpenTK.Graphics.OpenGL4;

namespace CART_457.Scripts
{
    public class MaterialPreconfigs
    {
        public const string DiffuseColorSampler = Material.DIFFUSE_SAMPLER;
        public const string NormalMapSampler = Material.NORMAL_MAP_SAMPLER;
        public const string SpecularMapSampler = Material.SPECULAR_MAP_SAMPLER;
        public const string ShadowMapSampler = Material.SHADOW_MAP_SAMPLER;
        
        public static Material Normal(
            FboSetup.FBOID fboid, 
            ShaderProgram shaderProgram, 
            Mesh mesh, 
            string diffusePath, 
            string normalPath, 
            string specularPath, 
            Action<Material> perMatSender)
        {
            var mat = Material.EntityBased(fboid, shaderProgram, mesh, perMatSender);

            mat.SetupSampler(DiffuseColorSampler, Texture.FromFile(diffusePath, TextureUnit.Texture0));
            mat.SetupSampler(NormalMapSampler, Texture.FromFile(normalPath, TextureUnit.Texture1));
            mat.SetupSampler(SpecularMapSampler, Texture.FromFile(specularPath, TextureUnit.Texture2));
            mat.SetupSampler(ShadowMapSampler, FboSetup.Shadow.ColorTexture1);
            
            return mat;
        }
    }
}