using System;
using CART_457.Attributes;
using CART_457.c_sharp.Renderer;
using CART_457.MaterialRelated;
using Texture = FbxSharp.Texture;

namespace CART_457.Scripts
{
    public static class MaterialSetup
    {

        [IncludeInDrawLoop] public static Material SolidSphere;
        [IncludeInDrawLoop] public static Material DirtSphere;
        [IncludeInDrawLoop] public static Material DirtPlane;
        [IncludeInDrawLoop] public static Material TileSphere;
        [IncludeInDrawLoop] public static Material TableProto;
        [IncludeInDrawLoop] public static Material EyeBall;
        [IncludeInDrawLoop] public static Material Camera;
        
        [IncludeInDrawLoop] public static Material ShadowMapSphere;
        [IncludeInDrawLoop] public static Material ShadowMapPlane;
        [IncludeInDrawLoop] public static Material ShadowMapTable;
        [IncludeInDrawLoop] public static Material ShadowMapDiamond;
        
        [IncludeInPostFX] public static Material PostProcessing;
        static MaterialSetup()
        {
        
        
            #region normal materials
            var normalShader = ShaderProgram.Standard("normal_map");

            Action<Material> normaMaterialUniformSender = (mat) =>
            {
                UniformSender.SetFloat(mat, "NormalMapStrength", 2);
                UniformSender.SetFloat(mat, "SpecularRoughness", 16);
                FboSetup.Shadow.UseTexturesAndGenerateMipMaps();
            };
            
            DirtSphere  = MaterialPreconfigs.Normal(
                FboSetup.Main,
                normalShader,
                CreateMeshes.IcoSphereHighPoly,
                TextureSetup.DirtDiffuse,
                TextureSetup.DirtNormalMap,
                TextureSetup.DirtSpecularMap,
            normaMaterialUniformSender
                );
            
            DirtPlane  = MaterialPreconfigs.Normal(
                FboSetup.Main,
                normalShader,
                CreateMeshes.Plane,
                TextureSetup.DirtDiffuse,
                TextureSetup.DirtNormalMap,
                TextureSetup.DirtSpecularMap,
                normaMaterialUniformSender
                );
            
            TileSphere = MaterialPreconfigs.Normal(
                FboSetup.Main,
                normalShader,
                CreateMeshes.IcoSphereHighPoly,
                TextureSetup.CarpetDiffuse,
                TextureSetup.CarpetNormalMap,
                TextureSetup.CarpetSpecularMap,
                normaMaterialUniformSender
                );
            
            TableProto = MaterialPreconfigs.Normal(
                FboSetup.Main,
                normalShader,
                CreateMeshes.TableProto,
                TextureSetup.TableDiffuse,
                TextureSetup.TableNormal,
                TextureSetup.TableSpecular,
                normaMaterialUniformSender
            );
            
            EyeBall = MaterialPreconfigs.Normal(
                FboSetup.Main,
                normalShader,
                CreateMeshes.Eyeball,
                TextureSetup.EyeDiffuse,
                TextureSetup.EyeNormal,
                TextureSetup.EyeSpecular,
                normaMaterialUniformSender
            );
            
            #endregion

            #region solid_color
            SolidSphere = Material.EntityBased(FboSetup.Main, ShaderProgram.Standard("textureless"), CreateMeshes.IcoSphereHighPoly, null);
            Camera = Material.EntityBased(FboSetup.Main, ShaderProgram.Standard("textureless"), CreateMeshes.Diamond, null);
            #endregion

            ShadowMapSphere = Material.EntityBased(
                FboSetup.Shadow,
                ShaderProgram.Standard("shadow_map"),
                CreateMeshes.IcoSphereHighPoly,
                null);
            
            ShadowMapPlane = Material.EntityBased(
                FboSetup.Shadow,
                ShaderProgram.Standard("shadow_map"),
                CreateMeshes.Plane,
                null);
            
            ShadowMapTable = Material.EntityBased(
                FboSetup.Shadow,
                ShaderProgram.Standard("shadow_map"),
                CreateMeshes.TableProto,
                null);
            
            ShadowMapDiamond = Material.EntityBased(
                FboSetup.Shadow,
                ShaderProgram.Standard("shadow_map"),
                CreateMeshes.Diamond,
                null);
            
            PostProcessing = Material.PostProcessing(ShaderProgram.PostProcessing("post_ffx_test"));
            PostProcessing.SetupSampler(Material.MAIN_COLOR_FBO_SAMPLER, FboSetup.Main.ColorTexture1);
            PostProcessing.SetupSampler(Material.SECONDARY_COLOR_FBO_SAMPLER, FboSetup.Main.ColorTexture2);
            // PostProcessing.SetupSampler(Material.MAIN_DEPTH_FBO_SAMPLER, FboSetup.Shadow.DepthTexture);
            PostProcessing.SetupSampler(Material.MAIN_DEPTH_FBO_SAMPLER, FboSetup.Main.DepthTexture);
            
        }



    }
}