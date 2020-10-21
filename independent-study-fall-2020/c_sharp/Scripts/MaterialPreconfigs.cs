using System;
using CART_457.MaterialRelated;
using CART_457.Helpers;
using OpenTK.Graphics.OpenGL4;

namespace CART_457.Scripts
{
    public class MaterialPreconfigs
    {

        public static Material Normal(
            FBO fbo, 
            ShaderProgram shaderProgram, 
            Mesh mesh, 
            Texture diffuse, 
            Texture normal, 
            Texture specular, 
            Action<Material> perMatSender)
        {
            var mat = Material.EntityBased(fbo, shaderProgram, mesh, perMatSender);

            mat.SetupSampler(Material.DIFFUSE_SAMPLER, diffuse);
            mat.SetupSampler(Material.NORMAL_MAP_SAMPLER, normal);
            mat.SetupSampler(Material.SPECULAR_MAP_SAMPLER, specular);
            mat.SetupSampler(Material.SHADOW_MAP_SAMPLER, FboSetup.Shadow.ColorTexture1);
            mat.SetupSampler(Material.NOISE_TEXTURE, TextureSetup.NoiseStatic);

            return mat;
        }
        
    }
}