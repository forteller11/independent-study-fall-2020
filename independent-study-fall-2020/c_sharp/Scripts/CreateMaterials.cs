using System.Collections.Generic;
using Indpendent_Study_Fall_2020.Helpers;
using Indpendent_Study_Fall_2020.MaterialRelated;
using Indpendent_Study_Fall_2020.Scripts.Materials;
using OpenTK.Graphics.OpenGL4;

namespace Indpendent_Study_Fall_2020.Scripts
{
    public static class CreateMaterials
    {
        public enum MaterialName
        {
            None = default,
            Solid,
            Shaded,
            Normal,
            Dirt,
            Tile,
            Buffer
        }
        public static Material[] Create()
        {
            var normalShader = new ShaderProgram("normal_map", "lighting");
            
            var dirt = new NormalMaterial(
                MaterialName.Dirt,
                normalShader,
                ModelImporter.GetAttribBuffersFromObjFile("ico_sphere"),
                "GroundClay002_COL_VAR1_3K.jpg",
                "GroundClay002_NRM_3K.jpg",
                "GroundClay002_GLOSS_3K.jpg"
                );
            
            
            
            const string bathroomTiles = "InteriorDesignRugStarryNight/";
            var tile = new NormalMaterial(
                MaterialName.Tile,
                normalShader,
                ModelImporter.GetAttribBuffersFromObjFile("ico_sphere"),
                bathroomTiles+"COL_VAR2_3K.jpg",
                bathroomTiles+"NRM_3K.jpg",
                bathroomTiles+"GLOSS_3K.jpg"
                );
            

            #region buffer
            var buffer = new Material(MaterialName.Normal, new ShaderProgram("buffer", "lighting"), CreateFBOs.FBOName.Shadow);

            var modelAttribsNormal = ModelImporter.GetAttribBuffersFromObjFile("ico_sphere", true, true, true);
            buffer.FeedBuffersAndCreateVAO(null, modelAttribsNormal);
            #endregion
            
            
            #region solid_color
            var solidColor = new Material(MaterialName.Solid, new ShaderProgram("textureless"));
            solidColor.FeedBuffersAndCreateVAO(null, ModelImporter.GetAttribBuffersFromObjFile("ico_sphere", true, false, false));
            #endregion

            return new[]
            {
                solidColor,
                dirt,
                tile,
                buffer
            };
        }
    }
}