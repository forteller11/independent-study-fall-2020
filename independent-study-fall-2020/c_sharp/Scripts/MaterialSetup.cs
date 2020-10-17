using System;
using CART_457.Attributes;
using CART_457.c_sharp.Renderer;
using CART_457.MaterialRelated;

namespace CART_457.Scripts
{
    public static class MaterialSetup
    {

        [IncludeInDrawLoop] public static Material SolidSphere;
        [IncludeInDrawLoop] public static Material DirtSphere;
        [IncludeInDrawLoop] public static Material DirtPlane;
        [IncludeInDrawLoop] public static Material TileSphere;
        [IncludeInDrawLoop] public static Material ShadowMap;
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
                FboSetup.FBOID.Main,
                normalShader,
                CreateMeshes.IcoSphereHighPoly,
                "GroundClay002_COL_VAR1_3K.jpg",
                "GroundClay002_NRM_3K.jpg",
                "GroundClay002_GLOSS_3K.jpg",
                normaMaterialUniformSender
                );
            
            DirtPlane  = MaterialPreconfigs.Normal(
                FboSetup.FBOID.Main,
                normalShader,
                CreateMeshes.Plane,
                "GroundClay002_COL_VAR1_3K.jpg",
                "GroundClay002_NRM_3K.jpg",
                "GroundClay002_GLOSS_3K.jpg",
                normaMaterialUniformSender
                );
            
            const string bathroomTiles = "InteriorDesignRugStarryNight/";
            TileSphere = MaterialPreconfigs.Normal(
                FboSetup.FBOID.Main,
                normalShader,
                CreateMeshes.IcoSphereHighPoly,
                bathroomTiles+"COL_VAR2_3K.jpg",
                bathroomTiles+"NRM_3K.jpg",
                bathroomTiles+"GLOSS_3K.jpg",
                normaMaterialUniformSender
                );
            #endregion

            #region solid_color
            SolidSphere = Material.EntityBased(FboSetup.FBOID.Main, ShaderProgram.Standard("textureless"), CreateMeshes.IcoSphereHighPoly, null);
            #endregion

            ShadowMap = Material.EntityBased(
                FboSetup.FBOID.Shadow,
                ShaderProgram.Standard("shadow_map"),
                CreateMeshes.IcoSphereHighPoly,
                null);
            
            
            PostProcessing = Material.PostProcessing(ShaderProgram.PostProcessing("post_ffx_test"));
            PostProcessing.SetupSampler(Material.MAIN_COLOR_SAMPLER, FboSetup.Main.ColorTexture1);
            PostProcessing.SetupSampler(Material.SECONDARY_COLOR_SAMPLER, FboSetup.Main.ColorTexture2);
            PostProcessing.SetupSampler(Material.MAIN_DEPTH_SAMPLER, FboSetup.Main.DepthTexture);
            
        }



    }
}