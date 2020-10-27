using System;
using CART_457.Attributes;
using CART_457.c_sharp.Renderer;
using CART_457.MaterialRelated;
using Texture = FbxSharp.Texture;

namespace CART_457.Scripts
{
    public static class InitMaterials
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
        static InitMaterials()
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
                InitMeshes.IcoSphereHighPoly,
                TextureSetup.DirtDiffuse,
                TextureSetup.DirtNormalMap,
                TextureSetup.DirtSpecularMap,
            normaMaterialUniformSender
                );
            
            DirtPlane  = MaterialPreconfigs.Normal(
                FboSetup.Main,
                normalShader,
                InitMeshes.Plane,
                TextureSetup.DirtDiffuse,
                TextureSetup.DirtNormalMap,
                TextureSetup.DirtSpecularMap,
                normaMaterialUniformSender
                );
            
            TileSphere = MaterialPreconfigs.Normal(
                FboSetup.Main,
                normalShader,
                InitMeshes.IcoSphereHighPoly,
                TextureSetup.CarpetDiffuse,
                TextureSetup.CarpetNormalMap,
                TextureSetup.CarpetSpecularMap,
                normaMaterialUniformSender
                );
            
            TableProto = MaterialPreconfigs.Normal(
                FboSetup.Main,
                normalShader,
                InitMeshes.TableProto,
                TextureSetup.TableDiffuse,
                TextureSetup.TableNormal,
                TextureSetup.TableSpecular,
                normaMaterialUniformSender
            );
            
            EyeBall = MaterialPreconfigs.Normal(
                FboSetup.Main,
                normalShader,
                InitMeshes.Eyeball,
                TextureSetup.EyeDiffuse,
                TextureSetup.EyeNormal,
                TextureSetup.EyeSpecular,
                normaMaterialUniformSender
            );
            #endregion

            #region solid_color
            var shaderSolid = ShaderProgram.Standard("textureless");
            
            SolidSphere = Material.EntityBased(FboSetup.Main, shaderSolid, InitMeshes.IcoSphereHighPoly, null);
            Camera = Material.EntityBased(FboSetup.Main, shaderSolid, InitMeshes.Diamond, null);
            #endregion

            #region shadow maps
            var shadowShader = ShaderProgram.Standard("shadow_map");
            
            ShadowMapSphere = Material.EntityBased(
                FboSetup.Shadow,
                shadowShader,
                InitMeshes.IcoSphereHighPoly,
                null);
            
            ShadowMapPlane = Material.EntityBased(
                FboSetup.Shadow,
                shadowShader,
                InitMeshes.Plane,
                null);
            
            ShadowMapTable = Material.EntityBased(
                FboSetup.Shadow,
                shadowShader,
                InitMeshes.TableProto,
                null);
            
            ShadowMapDiamond = Material.EntityBased(
                FboSetup.Shadow,
                shadowShader,
                InitMeshes.Diamond,
                null);
            #endregion
            
            PostProcessing = Material.PostProcessing(ShaderProgram.PostProcessing("post_ffx_test"));
            PostProcessing.SetupSampler(Material.MAIN_COLOR_FBO_SAMPLER, FboSetup.Main.ColorTexture1);
            PostProcessing.SetupSampler(Material.SECONDARY_COLOR_FBO_SAMPLER, FboSetup.Main.ColorTexture2);
            // PostProcessing.SetupSampler(Material.MAIN_DEPTH_FBO_SAMPLER, FboSetup.Shadow.DepthTexture);
            PostProcessing.SetupSampler(Material.MAIN_DEPTH_FBO_SAMPLER, FboSetup.Main.DepthTexture);
            
        }



    }
}