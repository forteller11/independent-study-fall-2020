using System;
using CART_457.Attributes;
using CART_457.c_sharp.Renderer;
using CART_457.MaterialRelated;
using Texture = FbxSharp.Texture;

namespace CART_457.Scripts
{
    public static class InitMaterials
    {

        [IncludeInDrawLoop] public static Material SolidSphereR1;
        [IncludeInDrawLoop] public static Material SolidSphereR2;
        [IncludeInDrawLoop] public static Material DirtSphere;
        [IncludeInDrawLoop] public static Material DirtPlane;
        [IncludeInDrawLoop] public static Material TileSphere;
        [IncludeInDrawLoop] public static Material TableProto;
        [IncludeInDrawLoop] public static Material EyeBall;
        [IncludeInDrawLoop] public static Material Camera;
        
        //dirt sphere room2
        
        [IncludeInDrawLoop] public static Material ShadowMapSphere;
        [IncludeInDrawLoop] public static Material ShadowMapPlane;
        [IncludeInDrawLoop] public static Material ShadowMapTable;
        [IncludeInDrawLoop] public static Material ShadowMapDiamond;
        
        [IncludeInPostFX] public static Material PostProcessing;
        static InitMaterials()
        {
        
        
            

            #region solid_color
            var shaderSolid = ShaderProgram.Standard("textureless");
            
            SolidSphereR1 = Material.EntitySolid(FboSetup.Room1, shaderSolid, InitMeshes.IcoSphereHighPoly, null);
            SolidSphereR2 = Material.EntitySolid(FboSetup.Room2, shaderSolid, InitMeshes.IcoSphereHighPoly, null);
            Camera = Material.EntitySolid(FboSetup.Room1, shaderSolid, InitMeshes.Diamond, null);
            #endregion

            #region shadow maps
            var shadowShader = ShaderProgram.Standard("shadow_map");
            
            ShadowMapSphere = Material.EntityCastShadow(
                FboSetup.Shadow1,
                shadowShader,
                InitMeshes.IcoSphereHighPoly,
                null);
            
            ShadowMapPlane = Material.EntityCastShadow(
                FboSetup.Shadow1,
                shadowShader,
                InitMeshes.Plane,
                null);
            
            ShadowMapTable = Material.EntityCastShadow(
                FboSetup.Shadow1,
                shadowShader,
                InitMeshes.TableProto,
                null);
            
            ShadowMapDiamond = Material.EntityCastShadow(
                FboSetup.Shadow1,
                shadowShader,
                InitMeshes.Diamond,
                null);
            #endregion
            
            
            
            #region normal materials
            var normalShader = ShaderProgram.Standard("normal_map");

            Action<Material> normaMaterialUniformSender = (mat) =>
            {
                UniformSender.SetFloat(mat, "NormalMapStrength", 2);
                UniformSender.SetFloat(mat, "SpecularRoughness", 16);
                FboSetup.Shadow1.UseTexturesAndGenerateMipMaps();
            };
            
            DirtSphere  = MaterialPreconfigs.Normal(
                FboSetup.Room1,
                normalShader,
                InitMeshes.IcoSphereHighPoly,
                FboSetup.Shadow1,
                TextureSetup.DirtDiffuse,
                TextureSetup.DirtNormalMap,
                TextureSetup.DirtSpecularMap,
            normaMaterialUniformSender
                );
            
            DirtPlane  = MaterialPreconfigs.Normal(
                FboSetup.Room1,
                normalShader,
                InitMeshes.Plane,
                FboSetup.Shadow1,
                TextureSetup.DirtDiffuse,
                TextureSetup.DirtNormalMap,
                TextureSetup.DirtSpecularMap,
                normaMaterialUniformSender
                );
            
            TileSphere = MaterialPreconfigs.Normal(
                FboSetup.Room1,
                normalShader,
                InitMeshes.IcoSphereHighPoly,
                FboSetup.Shadow1,
                TextureSetup.CarpetDiffuse,
                TextureSetup.CarpetNormalMap,
                TextureSetup.CarpetSpecularMap,
                normaMaterialUniformSender
                );
            
            TableProto = MaterialPreconfigs.Normal(
                FboSetup.Room1,
                normalShader,
                InitMeshes.TableProto,
                FboSetup.Shadow1,
                TextureSetup.TableDiffuse,
                TextureSetup.TableNormal,
                TextureSetup.TableSpecular,
                normaMaterialUniformSender
            );
            
            EyeBall = MaterialPreconfigs.Normal(
                FboSetup.Room1,
                normalShader,
                InitMeshes.Eyeball,
                FboSetup.Shadow1,
                TextureSetup.EyeDiffuse,
                TextureSetup.EyeNormal,
                TextureSetup.EyeSpecular,
                normaMaterialUniformSender
            );
            #endregion
            
            PostProcessing = Material.PostProcessing(ShaderProgram.PostProcessing("post_ffx_test"));
            PostProcessing.SetupSampler(UniformSender.MAIN_COLOR_FBO_SAMPLER, FboSetup.Room1.ColorTexture1);
            PostProcessing.SetupSampler(UniformSender.SECONDARY_COLOR_FBO_SAMPLER, FboSetup.Room1.ColorTexture2);
            // PostProcessing.SetupSampler(Material.MAIN_DEPTH_FBO_SAMPLER, FboSetup.Shadow.DepthTexture);
            PostProcessing.SetupSampler(UniformSender.MAIN_DEPTH_FBO_SAMPLER, FboSetup.Room1.DepthTexture);
            
        }



    }
}