
using CART_457.MaterialRelated;
using OpenTK.Graphics.OpenGL4;

/* Texture Unit standard
 
 0-2 for normal textures inputting into shader (diffuse, normals, specular-map etc.)
 
 3-5 used as input for framebuffers to shaders (color1 and 2, depth1)
 
 6- used by custom fbos for non linear passes
 
 
 */
namespace CART_457.Scripts.Setups
{
    public static class SetupTextures
    {
        public static Texture DirtDiffuse =     Texture.FromFile("GroundClay002_COL_VAR1_3K.jpg", TextureUnit.Texture0);
        public static Texture DirtNormalMap =   Texture.FromFile("GroundClay002_NRM_3K.jpg", TextureUnit.Texture1);
        public static Texture DirtSpecularMap = Texture.FromFile("GroundClay002_GLOSS_3K.jpg", TextureUnit.Texture2);
        
        
        const string BATHROOM_TILES = "InteriorDesignRugStarryNight/";
        public static Texture CarpetDiffuse =     Texture.FromFile(BATHROOM_TILES + "COL_VAR2_3K.jpg", TextureUnit.Texture0);
        public static Texture CarpetNormalMap =   Texture.FromFile(BATHROOM_TILES + "NRM_3K.jpg", TextureUnit.Texture1);
        public static Texture CarpetSpecularMap = Texture.FromFile(BATHROOM_TILES + "GLOSS_3K.jpg", TextureUnit.Texture2);

        private const string TABLE_PROTO = "table_proto/";
        public static Texture TableDiffuse = Texture.FromFile(TABLE_PROTO + "room_proto_table_04_Material.001_BaseColor.jpg", TextureUnit.Texture0);
        public static Texture TableNormal = Texture.FromFile(TABLE_PROTO + "room_proto_table_04_Material.001_Normal.jpg", TextureUnit.Texture1);
        public static Texture TableSpecular = Texture.FromFile(TABLE_PROTO + "room_proto_table_04_Material.001_Roughness.jpg", TextureUnit.Texture2);
        
        private const string EYE_BALL = "eyeball/";
        public static Texture EyeDiffuse = Texture.FromFile(EYE_BALL + "eyeblend_DefaultMaterial_BaseColor.jpg", TextureUnit.Texture0);
        public static Texture EyeNormal = Texture.FromFile(EYE_BALL + "eyeblend_DefaultMaterial_Normal.jpg", TextureUnit.Texture1);
        public static Texture EyeSpecular = Texture.FromFile(EYE_BALL + "eyeblend_DefaultMaterial_Roughness.jpg", TextureUnit.Texture2);
        
        private const string WEIRD_HEAD = "head/";
        public static Texture WeirdHeadDiffuse = Texture.FromFile(WEIRD_HEAD + "head_None_BaseColor.jpg", TextureUnit.Texture0);
        public static Texture WeirdHeadNormal = Texture.FromFile(WEIRD_HEAD + "head_None_Normal.jpg", TextureUnit.Texture1);
        public static Texture WeirdHeadSpecular = Texture.FromFile(WEIRD_HEAD + "head_None_Roughness.jpg", TextureUnit.Texture2);
        
        private const string WEBCAM = "webcam/";
        public static Texture WebcamDiffuse = Texture.FromFile(WEBCAM + "diffuse.jpg", TextureUnit.Texture0);
        public static Texture WebcamNormal = Texture.FromFile(WEBCAM + "normal.jpg", TextureUnit.Texture1);
        public static Texture WebcamSpecular = Texture.FromFile(WEBCAM + "specular.jpg", TextureUnit.Texture2);
        
        private const string ROOM_CLEAN_01 = "table_clean_01/";
        public static Texture RoomClean01Diffuse = Texture.FromFile(ROOM_CLEAN_01 + "diffuse.jpg", TextureUnit.Texture0);
        public static Texture RoomClean01Normal = Texture.FromFile(ROOM_CLEAN_01 + "normal.jpg", TextureUnit.Texture1);
        public static Texture RoomClean01Specular = Texture.FromFile(ROOM_CLEAN_01 + "specular.jpg", TextureUnit.Texture2);
        
        private const string ROOM_CLEAN_01_CEILING_LAMPS = "table_clean_01_ceiling_lamps/";
        public static Texture RoomClean01CeilingLampsDiffuse = Texture.FromFile(ROOM_CLEAN_01_CEILING_LAMPS + "diffuse.jpg", TextureUnit.Texture0);
        public static Texture RoomClean01CeilingLampsNormal = Texture.FromFile(ROOM_CLEAN_01_CEILING_LAMPS + "normal.jpg", TextureUnit.Texture1);
        public static Texture RoomClean01CeilingLampsSpecular = Texture.FromFile(ROOM_CLEAN_01_CEILING_LAMPS + "specular.jpg", TextureUnit.Texture2);
        
        private const string ROOM_DIRTY_01_ = "table_dirty_01/";
        public static Texture RoomDirty01Diffuse = Texture.FromFile(ROOM_DIRTY_01_ + "diffuse.jpg", TextureUnit.Texture0);
        public static Texture RoomDirty01Normal = Texture.FromFile(ROOM_DIRTY_01_ + "normal.jpg", TextureUnit.Texture1);
        public static Texture RoomDirty01Specular = Texture.FromFile(ROOM_DIRTY_01_ + "specular.jpg", TextureUnit.Texture2);
        
        private const string BASEMENT = "basement/";
        public static Texture BasementDiffuse = Texture.FromFile(BASEMENT + "diffuse.jpg", TextureUnit.Texture0);
        public static Texture BasementNormal = Texture.FromFile(BASEMENT + "normal.jpg", TextureUnit.Texture1);
        public static Texture BasementSpecular = Texture.FromFile(BASEMENT + "specular.jpg", TextureUnit.Texture2);
        
        private const string DOOR_OPEN = "door_open/";
        public static Texture DoorOpenDiffuse = Texture.FromFile(DOOR_OPEN + "diffuse.jpg", TextureUnit.Texture0);
        public static Texture DoorOpenNormal = Texture.FromFile(DOOR_OPEN + "normal.jpg", TextureUnit.Texture1);
        public static Texture DoorOpenSpecular = Texture.FromFile(DOOR_OPEN + "specular.jpg", TextureUnit.Texture2);
        
         public static Texture NoiseStatic = Texture.FromFile("noise_gaussian_2000.bmp", TextureUnit.Texture7);
    }
}