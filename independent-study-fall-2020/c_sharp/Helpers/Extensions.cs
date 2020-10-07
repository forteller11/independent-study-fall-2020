using System.Data;
using OpenTK.Graphics.OpenGL4;

namespace Indpendent_Study_Fall_2020.Helpers
{
    public static class Extensions
    {
        public static int ToIndex(this TextureUnit texUnit)
        {
            switch (texUnit)
            {
                case TextureUnit.Texture0: return 0;
                case TextureUnit.Texture1: return 1;
                case TextureUnit.Texture2: return 2;
                case TextureUnit.Texture3: return 3;
                case TextureUnit.Texture4: return 4;
                case TextureUnit.Texture5: return 5;
                case TextureUnit.Texture6: return 6;
                case TextureUnit.Texture7: return 7;
                case TextureUnit.Texture8: return 8;
                case TextureUnit.Texture9: return 9;
                case TextureUnit.Texture10: return 10;
                case TextureUnit.Texture11: return 11;
                case TextureUnit.Texture12: return 12;
                case TextureUnit.Texture13: return 13;
                case TextureUnit.Texture14: return 14;
                case TextureUnit.Texture15: return 15;
                case TextureUnit.Texture16: return 16;
                case TextureUnit.Texture17: return 17;
                case TextureUnit.Texture18: return 18;
                case TextureUnit.Texture19: return 19;
                case TextureUnit.Texture20: return 20;
                case TextureUnit.Texture21: return 21;
                case TextureUnit.Texture22: return 22;
                case TextureUnit.Texture23: return 23;
                case TextureUnit.Texture24: return 24;
                case TextureUnit.Texture25: return 25;
                case TextureUnit.Texture26: return 26;
                case TextureUnit.Texture27: return 27;
                case TextureUnit.Texture28: return 28;
                case TextureUnit.Texture29: return 29;
                case TextureUnit.Texture30: return 30;
                case TextureUnit.Texture31: return 31;
                default: throw new DataException($"Texture Unit is {texUnit} which is outside the acceptable range openGL allows!");
            }
        }
    }
}