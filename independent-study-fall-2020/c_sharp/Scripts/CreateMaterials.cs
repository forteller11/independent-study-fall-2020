using System.Collections.Generic;
using Indpendent_Study_Fall_2020.Helpers;
using Indpendent_Study_Fall_2020.MaterialRelated;
using OpenTK.Graphics.OpenGL4;

namespace Indpendent_Study_Fall_2020.Scripts
{
    public static class CreateMaterials
    {
        public static Material[] Create()
        {
            #region shaded
            var shaded = new Material("shaded_mat", new ShaderProgram("shaded", "lighting"));

            var modelAttribs = ModelImporter.GetAttribBuffersFromObjFile("ico_sphere", true, true, true);
            shaded.FeedBuffersAndCreateVAO(null, modelAttribs);

            shaded.SetupATexture("GroundClay002_COL_VAR1_3K.jpg", "Color", TextureUnit.Texture0);
//            shaded.SetupATexture("GroundClay002_NRM_3K.jpg", "Texture1", TextureUnit.Texture1, 1);
            #endregion
            
            #region normal_map
            var normal = new Material("normal_mat", new ShaderProgram("normal_map", "lighting"));

            var modelAttribsNormal = ModelImporter.GetAttribBuffersFromObjFile("ico_sphere", true, true, true);
            normal.FeedBuffersAndCreateVAO(null, modelAttribsNormal);

            normal.SetupATexture("GroundClay002_COL_VAR1_3K.jpg", "Color", TextureUnit.Texture0);
            normal.SetupATexture("GroundClay002_NRM_3K.jpg", "Normal", TextureUnit.Texture1);
            normal.SetupATexture("GroundClay002_GLOSS_3K.jpg", "Gloss", TextureUnit.Texture2);
            #endregion
            
            #region normal_map
            var normal2 = new Material("normal_mat2", new ShaderProgram("normal_map", "lighting"));

            var modelAttribsNormal2 = ModelImporter.GetAttribBuffersFromObjFile("ico_sphere", true, true, true);
            normal2.FeedBuffersAndCreateVAO(null, modelAttribsNormal);

            const string bathroomTiles = "InteriorDesignRugStarryNight/";
            normal2.SetupATexture(bathroomTiles+"COL_VAR2_3K.jpg", "Color", TextureUnit.Texture0);
            normal2.SetupATexture(bathroomTiles+"NRM_3K.jpg", "Normal", TextureUnit.Texture1);
            normal2.SetupATexture(bathroomTiles+"GLOSS_3K.jpg", "Gloss", TextureUnit.Texture2);
            #endregion
            
            var dirt1 = new Material("normal_mat2", new ShaderProgram("normal_map", "lighting"));
            var dirt1Attrib = ModelImporter.GetAttribBuffersFromObjFile("ico_sphere", true, true, true);
            dirt1.FeedBuffersAndCreateVAO(null, dirt1Attrib);
            
            normal2.SetupATexture(bathroomTiles+"COL_VAR2_3K.jpg", "Color", TextureUnit.Texture0);
            normal2.SetupATexture(bathroomTiles+"NRM_3K.jpg", "Normal", TextureUnit.Texture1);
            normal2.SetupATexture(bathroomTiles+"GLOSS_3K.jpg", "Gloss", TextureUnit.Texture2);
            
            
            #region solid_color
            var solidColor = new Material("solidColor_mat", new ShaderProgram("textureless"));
            solidColor.FeedBuffersAndCreateVAO(null, ModelImporter.GetAttribBuffersFromObjFile("ico_sphere", true, false, false));
            #endregion

            return new[]
            {
                shaded,
                normal,
                solidColor,
                normal2
            };
        }
    }
}