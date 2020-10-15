using System;
using System.Collections.Generic;
using Indpendent_Study_Fall_2020.c_sharp.Renderer;
using Indpendent_Study_Fall_2020.c_sharp.Scripts;
using Indpendent_Study_Fall_2020.Helpers;
using Indpendent_Study_Fall_2020.MaterialRelated;
using Indpendent_Study_Fall_2020.Scripts.Materials;
using OpenTK.Graphics.OpenGL4;

namespace Indpendent_Study_Fall_2020.Scripts
{
    public static class CreateMaterials
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
            // VisualizeDepthTexture
        }
        public static Material[] Create()
        {
        
        
            #region normal materials
            var normalShader = new ShaderProgram("normal_map", "lighting");

            Action<Material> normaMaterialUniformSender = (mat) =>
            {
                UniformSender.SetFloat(mat, "NormalMapStrength", 2);
                UniformSender.SetFloat(mat, "SpecularRoughness", 16);
            };
            
            var dirt = new NormalMaterial(
                MaterialType.Dirt,
                CreateFBOs.FBOType.Default,
                normalShader,
                CreateMeshes.IcoSphereHighPoly,
                "GroundClay002_COL_VAR1_3K.jpg",
                "GroundClay002_NRM_3K.jpg",
                "GroundClay002_GLOSS_3K.jpg",
                normaMaterialUniformSender
                );
            
            var dirtPlane = new NormalMaterial(
                MaterialType.DirtPlane,
                CreateFBOs.FBOType.Default,
                normalShader,
                CreateMeshes.Plane,
                "GroundClay002_COL_VAR1_3K.jpg",
                "GroundClay002_NRM_3K.jpg",
                "GroundClay002_GLOSS_3K.jpg",
                normaMaterialUniformSender
                );
            
            const string bathroomTiles = "InteriorDesignRugStarryNight/";
            var tile = new NormalMaterial(
                MaterialType.Tile,
                CreateFBOs.FBOType.Default,
                normalShader,
                CreateMeshes.IcoSphereHighPoly,
                bathroomTiles+"COL_VAR2_3K.jpg",
                bathroomTiles+"NRM_3K.jpg",
                bathroomTiles+"GLOSS_3K.jpg",
                normaMaterialUniformSender
                );
            #endregion

            #region solid_color
            var solidColor = new Material(MaterialType.Solid, CreateFBOs.FBOType.Default, new ShaderProgram("textureless"), null);
            solidColor.VAOFromMesh(CreateMeshes.IcoSphereHighPoly);
            #endregion
            
            var shadowMap = new Material(MaterialType.ShadowMap, CreateFBOs.FBOType.Shadow, new ShaderProgram("shadow_map", "lighting"), null );
            shadowMap.VAOFromMesh(CreateMeshes.IcoSphereHighPoly); 
            
            
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
    }
}