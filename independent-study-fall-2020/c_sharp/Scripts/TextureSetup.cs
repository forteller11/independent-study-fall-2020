﻿
using CART_457.MaterialRelated;
using OpenTK.Graphics.OpenGL4;

namespace CART_457.Scripts
{
    public static class TextureSetup
    {
        public static Texture DirtDiffuse =     Texture.FromFile("GroundClay002_COL_VAR1_3K.jpg", TextureUnit.Texture0);
        public static Texture DirtNormalMap =   Texture.FromFile("GroundClay002_NRM_3K.jpg", TextureUnit.Texture1);
        public static Texture DirtSpecularMap = Texture.FromFile("GroundClay002_GLOSS_3K.jpg", TextureUnit.Texture2);
        
        
        const string BATHROOM_TILES = "InteriorDesignRugStarryNight/";
        public static Texture CarpetDiffuse =     Texture.FromFile(BATHROOM_TILES + "COL_VAR2_3K.jpg", TextureUnit.Texture0);
        public static Texture CarpetNormalMap =   Texture.FromFile(BATHROOM_TILES + "NRM_3K.jpg", TextureUnit.Texture1);
        public static Texture CarpetSpecularMap = Texture.FromFile(BATHROOM_TILES + "GLOSS_3K.jpg", TextureUnit.Texture2);
        
         public static Texture NoiseStatic = Texture.FromFile("noise_gaussian_2000.bmp", TextureUnit.Texture7);
    }
}