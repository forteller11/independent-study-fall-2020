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
            string diffusePath, 
            string normalPath, 
            string specularPath, 
            Action<Material> perMatSender)
        {
            var mat = Material.EntityBased(fbo, shaderProgram, mesh, perMatSender);

            mat.SetupSampler(Material.DIFFUSE_SAMPLER, Texture.FromFile(diffusePath, TextureUnit.Texture0));
            mat.SetupSampler(Material.NORMAL_MAP_SAMPLER, Texture.FromFile(normalPath, TextureUnit.Texture1));
            mat.SetupSampler(Material.SPECULAR_MAP_SAMPLER, Texture.FromFile(specularPath, TextureUnit.Texture2));
            mat.SetupSampler(Material.SHADOW_MAP_SAMPLER, FboSetup.Shadow.ColorTexture1);

            return mat;
        }
    }
}