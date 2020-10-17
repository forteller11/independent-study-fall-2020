using System;
using System.Collections.Generic;
using System.Reflection;
using Indpendent_Study_Fall_2020.c_sharp.Attributes;
using Indpendent_Study_Fall_2020.c_sharp.Renderer;
using Indpendent_Study_Fall_2020.c_sharp.Scripts;
using Indpendent_Study_Fall_2020.EntitySystem;
using Indpendent_Study_Fall_2020.Helpers;
using Indpendent_Study_Fall_2020.MaterialRelated;
using Indpendent_Study_Fall_2020.Scripts.Materials;
using OpenTK.Graphics.OpenGL4;

namespace Indpendent_Study_Fall_2020.Scripts
{
    public static class MaterialSetup
    {

        [IncludeInDrawLoop] public static Material SolidSphere;
        [IncludeInDrawLoop] public static Material DirtSphere;
        [IncludeInDrawLoop] public static Material DirtPlane;
        [IncludeInDrawLoop] public static Material TileSphere;
        [IncludeInDrawLoop] public static Material ShadowMap;
        [IncludeInPostFX] public static Material PostProcessing;
        public static void SetupMaterials()
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


            // var visualizeDepth = new Material(MaterialType.VisualizeDepthTexture, CreateFBOs.FBOType.Default, new ShaderProgram("visualize_depth_map", "lighting"), null );
            // visualizeDepth.VAOFromMesh(CreateMeshes.IcoSphereHighPoly); 

        }

        public static Material[] CreatePostProcessing()
        {
            var depthVisualizer = Material.PostProcessing(ShaderProgram.PostProcessing("post_ffx_test"));
                depthVisualizer.SetupSampler(Material.MAIN_COLOR_SAMPLER, FboSetup.Main.ColorTexture1);
                depthVisualizer.SetupSampler(Material.SECONDARY_COLOR_SAMPLER, FboSetup.Main.ColorTexture2);
                depthVisualizer.SetupSampler(Material.MAIN_DEPTH_SAMPLER, FboSetup.Main.DepthTexture);
                
            return new Material[]
            {
                 depthVisualizer
            };
        }
        
        
        
    }
}