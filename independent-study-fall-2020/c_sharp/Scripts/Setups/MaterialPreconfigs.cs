using System;
using CART_457.c_sharp.Renderer;
using CART_457.MaterialRelated;

namespace CART_457.Scripts.Setups
{
    public class MaterialPreconfigs
    {

        public static Material Normal(
            FBO fbo, Mesh mesh, FBO shadowMapFBO, 
            Texture diffuse, Texture normal, Texture specular, 
            Action<Material> perMatSender)
        {
            var mat = Material.EntityNormalUseShadow(fbo, mesh, shadowMapFBO, perMatSender);

            mat.SetupSampler(UniformSender.DIFFUSE_SAMPLER, diffuse);
            mat.SetupSampler(UniformSender.NORMAL_MAP_SAMPLER, normal);
            mat.SetupSampler(UniformSender.SPECULAR_MAP_SAMPLER, specular);
            mat.SetupSampler(UniformSender.SHADOW_MAP_SAMPLER, shadowMapFBO.ColorTexture1);

            return mat;
        }
        
    }
}