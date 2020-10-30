using System;
using CART_457.c_sharp.Renderer;
using CART_457.MaterialRelated;
using CART_457.Helpers;
using OpenTK.Graphics.OpenGL4;

namespace CART_457.Scripts
{
    public class MaterialPreconfigs
    {

        public static Material Normal(
            FBO fbo, ShaderProgram shaderProgram, Mesh mesh, FBO shadowMapFBO, 
            Texture diffuse, Texture normal, Texture specular, 
            Action<Material> perMatSender)
        {
            var mat = Material.EntityNormalUseShadow(fbo, shaderProgram, mesh, shadowMapFBO, perMatSender);

            mat.SetupSampler(UniformSender.DIFFUSE_SAMPLER, diffuse);
            mat.SetupSampler(UniformSender.NORMAL_MAP_SAMPLER, normal);
            mat.SetupSampler(UniformSender.SPECULAR_MAP_SAMPLER, specular);
            mat.SetupSampler(UniformSender.SHADOW_MAP_SAMPLER, FboSetup.Shadow.ColorTexture1);
            mat.SetupSampler(UniformSender.NOISE_TEXTURE, TextureSetup.NoiseStatic);

            return mat;
        }
        
    }
}