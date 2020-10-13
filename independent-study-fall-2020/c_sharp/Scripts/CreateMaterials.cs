﻿using System;
using System.Collections.Generic;
using Indpendent_Study_Fall_2020.c_sharp.Renderer;
using Indpendent_Study_Fall_2020.Helpers;
using Indpendent_Study_Fall_2020.MaterialRelated;
using Indpendent_Study_Fall_2020.Scripts.Materials;
using OpenTK.Graphics.OpenGL4;

namespace Indpendent_Study_Fall_2020.Scripts
{
    public static class CreateMaterials
    {
        public enum MaterialType
        {
            None = default,
            Solid,
            Dirt,
            Tile
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
                normalShader,
                ModelImporter.GetAttribBuffersFromObjFile("ico_sphere"),
                "GroundClay002_COL_VAR1_3K.jpg",
                "GroundClay002_NRM_3K.jpg",
                "GroundClay002_GLOSS_3K.jpg",
                normaMaterialUniformSender
                );
            
            const string bathroomTiles = "InteriorDesignRugStarryNight/";
            var tile = new NormalMaterial(
                MaterialType.Tile,
                normalShader,
                ModelImporter.GetAttribBuffersFromObjFile("ico_sphere"),
                bathroomTiles+"COL_VAR2_3K.jpg",
                bathroomTiles+"NRM_3K.jpg",
                bathroomTiles+"GLOSS_3K.jpg",
                normaMaterialUniformSender
                );
            #endregion
            

            #region solid_color
            var solidColor = new Material(MaterialType.Solid, new ShaderProgram("textureless"), null);
            solidColor.FeedBuffersAndCreateVAO(null, ModelImporter.GetAttribBuffersFromObjFile("ico_sphere", true, false, false));
            #endregion

            return new[]
            {
                solidColor,
                dirt,
                tile
            };
        }
    }
}