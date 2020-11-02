using System;
using CART_457.Attributes;
using CART_457.c_sharp.Renderer;
using CART_457.MaterialRelated;
using Texture = FbxSharp.Texture;

namespace CART_457.Scripts.Setups
{
    public static class SetupMaterials
    {

        [IncludeInDrawLoop] public static Material SolidSphereR1;
        [IncludeInDrawLoop] public static Material SolidSphereR2;
        [IncludeInDrawLoop] public static Material DirtSphere;
        [IncludeInDrawLoop] public static Material DirtPlane;
        [IncludeInDrawLoop] public static Material TileSphere;
        [IncludeInDrawLoop] public static Material TableProto;
        [IncludeInDrawLoop] public static Material EyeBall;
        [IncludeInDrawLoop] public static Material Camera;
        [IncludeInDrawLoop] public static Material Screen;
        
        //dirt sphere room2
        
        [IncludeInDrawLoop] public static Material ShadowMapSphere;
        [IncludeInDrawLoop] public static Material ShadowMapPlane;
        [IncludeInDrawLoop] public static Material ShadowMapTable;
        [IncludeInDrawLoop] public static Material ShadowMapDiamond;
        
        [IncludeInPostFX] public static Material PostProcessing;
        static SetupMaterials()
        {
            
            #region solid_color
            SolidSphereR1 = Material.EntitySolid(SetupFBOs.Room1, SetupMeshes.IcoSphereHighPoly, null);
            SolidSphereR2 = Material.EntitySolid(SetupFBOs.Room2, SetupMeshes.IcoSphereHighPoly, null);
            Camera = Material.EntitySolid(SetupFBOs.Room1, SetupMeshes.Diamond, null);
            #endregion

            #region shadow maps
            ShadowMapSphere = Material.EntityCastShadow(
                SetupFBOs.Shadow1,
                SetupMeshes.IcoSphereHighPoly,
                null);
            
            ShadowMapPlane = Material.EntityCastShadow(
                SetupFBOs.Shadow1,
                SetupMeshes.Plane,
                null);
            
            ShadowMapTable = Material.EntityCastShadow(
                SetupFBOs.Shadow1,
                SetupMeshes.TableProto,
                null);
            
            ShadowMapDiamond = Material.EntityCastShadow(
                SetupFBOs.Shadow1,
                SetupMeshes.Diamond,
                null);
            #endregion
            
            
            
            #region normal materials

            Action<Material> normaMaterialUniformSender = (mat) =>
            {
                UniformSender.SetFloat(mat, "NormalMapStrength", 2);
                UniformSender.SetFloat(mat, "SpecularRoughness", 16);
                SetupFBOs.Shadow1.UseTexturesAndGenerateMipMaps();
            };
            
            DirtSphere  = MaterialPreconfigs.Normal(
                SetupFBOs.Room1,
                SetupMeshes.IcoSphereHighPoly,
                SetupFBOs.Shadow1,
                SetupTextures.DirtDiffuse,
                SetupTextures.DirtNormalMap,
                SetupTextures.DirtSpecularMap,
            normaMaterialUniformSender
                );
            
            DirtPlane  = MaterialPreconfigs.Normal(
                SetupFBOs.Room1,
                SetupMeshes.Plane,
                SetupFBOs.Shadow1,
                SetupTextures.DirtDiffuse,
                SetupTextures.DirtNormalMap,
                SetupTextures.DirtSpecularMap,
                normaMaterialUniformSender
                );
            
            TileSphere = MaterialPreconfigs.Normal(
                SetupFBOs.Room1,
                SetupMeshes.IcoSphereHighPoly,
                SetupFBOs.Shadow1,
                SetupTextures.CarpetDiffuse,
                SetupTextures.CarpetNormalMap,
                SetupTextures.CarpetSpecularMap,
                normaMaterialUniformSender
                );
            
            TableProto = MaterialPreconfigs.Normal(
                SetupFBOs.Room1,
                SetupMeshes.TableProto,
                SetupFBOs.Shadow1,
                SetupTextures.TableDiffuse,
                SetupTextures.TableNormal,
                SetupTextures.TableSpecular,
                normaMaterialUniformSender
            );
            
            EyeBall = MaterialPreconfigs.Normal(
                SetupFBOs.Room1,
                SetupMeshes.Eyeball,
                SetupFBOs.Shadow1,
                SetupTextures.EyeDiffuse,
                SetupTextures.EyeNormal,
                SetupTextures.EyeSpecular,
                normaMaterialUniformSender
            );
            #endregion
            
            #region screen material

            Screen = Material.GenericEntityBased(SetupFBOs.ScreenManager, SetupShaders.Screen, SetupMeshes.ViewSpaceQuad, null, 
                (entity, material) =>
                {
                    UniformSender.SendTransformMatrices(entity, material, material.RenderTarget.Camera);
                    UniformSender.SendGlobals(material);
                });
            #endregion
            
            #region postfx
            PostProcessing = Material.PostProcessing(SetupShaders.PostFX);
            PostProcessing.SetupSampler(UniformSender.MAIN_COLOR_FBO_SAMPLER, SetupFBOs.Room1.ColorTexture1);
            PostProcessing.SetupSampler(UniformSender.SECONDARY_COLOR_FBO_SAMPLER, SetupFBOs.Room1.ColorTexture2);
            // PostProcessing.SetupSampler(Material.MAIN_DEPTH_FBO_SAMPLER, FboSetup.Shadow.DepthTexture);
            PostProcessing.SetupSampler(UniformSender.MAIN_DEPTH_FBO_SAMPLER, SetupFBOs.Room1.DepthTexture);
            #endregion
            
        }



    }
}