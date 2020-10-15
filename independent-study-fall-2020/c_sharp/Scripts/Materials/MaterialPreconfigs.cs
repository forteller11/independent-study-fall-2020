using System;
using Indpendent_Study_Fall_2020.Helpers;
using Indpendent_Study_Fall_2020.MaterialRelated;
using OpenTK.Graphics.OpenGL4;

namespace Indpendent_Study_Fall_2020.Scripts.Materials
{
    public class MaterialPreconfigs
    {
        public const string DiffuseColorSampler = "Color";
        public const string NormalMapSampler = "Normal";
        public const string SpecularMapSampler = "Gloss";
        
        public static Material Normal(
            CreateMaterials.MaterialType type, 
            CreateFBOs.FBOType fboType, 
            ShaderProgram shaderProgram, 
            Mesh mesh, 
            string diffusePath, 
            string normalPath, 
            string specularPath, 
            Action<Material> perMatSender)
        {
            var mat = Material.EntityBased(type, fboType, shaderProgram, mesh, perMatSender);

            mat.SetupSampler(DiffuseColorSampler, Texture.FromFile(diffusePath, TextureUnit.Texture0));
            mat.SetupSampler(NormalMapSampler, Texture.FromFile(normalPath, TextureUnit.Texture1));
            mat.SetupSampler(SpecularMapSampler, Texture.FromFile(specularPath, TextureUnit.Texture2));
            
            return mat;
        }
    }
}