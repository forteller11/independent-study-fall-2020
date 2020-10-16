using System;
using System.Collections.Generic;
using Indpendent_Study_Fall_2020.c_sharp.Renderer;
using Indpendent_Study_Fall_2020.c_sharp.Scripts;
using Indpendent_Study_Fall_2020.EntitySystem;
using Indpendent_Study_Fall_2020.Helpers;
using Indpendent_Study_Fall_2020.MaterialRelated;
using Indpendent_Study_Fall_2020.Scripts.Materials;
using OpenTK.Graphics.OpenGL4;

namespace Indpendent_Study_Fall_2020.Scripts
{
    public static class MaterialFactory
    {
        [Flags]
        public enum MaterialType
        {
            None = default,
            Solid,
            Dirt,
            DirtPlane,
            Tile,
            ShadowMap,
            PostProcessing
            // VisualizeDepthTexture
        }
        public static Material[] CreateEntityBased()
        {
        
        
            #region normal materials
            var normalShader = ShaderProgram.Standard("normal_map", "lighting");

            Action<Material> normaMaterialUniformSender = (mat) =>
            {
                UniformSender.SetFloat(mat, "NormalMapStrength", 2);
                UniformSender.SetFloat(mat, "SpecularRoughness", 16);
            };
            
            var dirt  = MaterialPreconfigs.Normal(
                MaterialType.Dirt,
                CreateFBOs.FBOID.Default,
                normalShader,
                CreateMeshes.IcoSphereHighPoly,
                "GroundClay002_COL_VAR1_3K.jpg",
                "GroundClay002_NRM_3K.jpg",
                "GroundClay002_GLOSS_3K.jpg",
                normaMaterialUniformSender
                );
            
            var dirtPlane  = MaterialPreconfigs.Normal(
                MaterialType.DirtPlane,
                CreateFBOs.FBOID.Default,
                normalShader,
                CreateMeshes.Plane,
                "GroundClay002_COL_VAR1_3K.jpg",
                "GroundClay002_NRM_3K.jpg",
                "GroundClay002_GLOSS_3K.jpg",
                normaMaterialUniformSender
                );
            
            const string bathroomTiles = "InteriorDesignRugStarryNight/";
            var tile = MaterialPreconfigs.Normal(
                MaterialType.Tile,
                CreateFBOs.FBOID.Default,
                normalShader,
                CreateMeshes.IcoSphereHighPoly,
                bathroomTiles+"COL_VAR2_3K.jpg",
                bathroomTiles+"NRM_3K.jpg",
                bathroomTiles+"GLOSS_3K.jpg",
                normaMaterialUniformSender
                );
            #endregion

            #region solid_color
            var solidColor = Material.EntityBased(MaterialType.Solid, CreateFBOs.FBOID.Default, ShaderProgram.Standard("textureless"), CreateMeshes.IcoSphereHighPoly, null);
            #endregion

            var shadowMap = Material.EntityBased(
                MaterialType.ShadowMap,
                CreateFBOs.FBOID.Shadow,
                ShaderProgram.Standard("shadow_map"),
                CreateMeshes.IcoSphereHighPoly,
                null);


            // var visualizeDepth = new Material(MaterialType.VisualizeDepthTexture, CreateFBOs.FBOType.Default, new ShaderProgram("visualize_depth_map", "lighting"), null );
            // visualizeDepth.VAOFromMesh(CreateMeshes.IcoSphereHighPoly); 

            return new[]
            {
                shadowMap,
                solidColor,
                dirt,
                dirtPlane,
                tile,
                // visualizeDepth
            };
        }

        public static Material[] CreatePostProcessing()
        {
            var depthVisualizer = Material.PostProcessing(ShaderProgram.PostProcessing("post_ffx_test"));
                depthVisualizer.SetupSampler("MainColor", DrawManager.PostProcessingFbo.ColorTexture);
                depthVisualizer.SetupSampler("MainDepth", DrawManager.PostProcessingFbo.DepthTexture);
                
            return new Material[]
            {
                 depthVisualizer
            };
        }
        
        
        
    }
}