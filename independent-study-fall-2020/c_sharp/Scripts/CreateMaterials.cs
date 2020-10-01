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

            shaded.SetupATexture("GroundClay002_COL_VAR1_3K.jpg", "Texture0", TextureUnit.Texture0, 0);
            shaded.SetupATexture("GroundClay002_NRM_3K.jpg", "Texture1", TextureUnit.Texture1, 1);
            #endregion
            
            #region solid_color
            var solidColor = new Material("solidColor_mat", new ShaderProgram("textureless"));
            solidColor.FeedBuffersAndCreateVAO(null, ModelImporter.GetAttribBuffersFromObjFile("ico_sphere", true, false, false));
            #endregion

            return new[]
            {
                shaded,
                solidColor
            };
        }
    }
}