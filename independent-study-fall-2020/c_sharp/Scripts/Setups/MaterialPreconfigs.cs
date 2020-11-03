using System;
using CART_457.c_sharp.Renderer;
using CART_457.EntitySystem;
using CART_457.MaterialRelated;

namespace CART_457.Scripts.Setups
{
    public class MaterialPreconfigs
    {

        public static Material NormalReceiveShadow(
            FBO fbo, Mesh mesh, FBO shadowMapFBO, Texture diffuse, Texture normal, Texture specular, Action<Material> perMatSender)
        {
            
            Material mat = Material.GenericEntityBased(fbo, SetupShaders.NormalReceiveShadow, mesh, perMatSender, (entity, material) =>
            {
                UniformSender.SendTransformMatrices(entity, material, shadowMapFBO.Camera, "Light");
                UniformSender.SendTransformMatrices(entity, material, material.RenderTarget.Camera);
                UniformSender.SendLights(material);
                UniformSender.SendGlobals(material);
            });

            mat.SetupSampler(UniformSender.DIFFUSE_SAMPLER, diffuse);
            mat.SetupSampler(UniformSender.NORMAL_MAP_SAMPLER, normal);
            mat.SetupSampler(UniformSender.SPECULAR_MAP_SAMPLER, specular);
            mat.SetupSampler(UniformSender.SHADOW_MAP_SAMPLER, shadowMapFBO.ColorTexture1);

            return mat;
        }
        
        public static Material NormalNoShadow(
            FBO fbo, Mesh mesh, Texture diffuse, Texture normal, Texture specular, Action<Material> perMatSender)
        {

             Material mat = Material.GenericEntityBased(fbo, SetupShaders.Normal, mesh, perMatSender, (entity, material) =>
            {
                UniformSender.SendTransformMatrices(entity, material, material.RenderTarget.Camera);
                UniformSender.SendLights(material);
                UniformSender.SendGlobals(material);
            });

            mat.SetupSampler(UniformSender.DIFFUSE_SAMPLER, diffuse);
            mat.SetupSampler(UniformSender.NORMAL_MAP_SAMPLER, normal);
            mat.SetupSampler(UniformSender.SPECULAR_MAP_SAMPLER, specular);

            return mat;
        }

   

    }
}