
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
        private const string EYE_BALL = "eyeball/";
        public static Texture EyeDiffuse = Texture.FromFile(EYE_BALL + "eyeblend_DefaultMaterial_BaseColor.jpg", TextureUnit.Texture0);
        public static Texture EyeNormal = Texture.FromFile(EYE_BALL + "eyeblend_DefaultMaterial_Normal.jpg", TextureUnit.Texture1);
        public static Texture EyeSpecular = Texture.FromFile(EYE_BALL + "eyeblend_DefaultMaterial_Roughness.jpg", TextureUnit.Texture2);

        private const string ROOM_CLEAN_01 = "table_clean_01/";
        public static Texture BedroomCleanDiffuse = Texture.FromFile(ROOM_CLEAN_01 + "diffuse.jpg", TextureUnit.Texture0);
        public static Texture BedroomCleanNormal = Texture.FromFile(ROOM_CLEAN_01 + "normal.jpg", TextureUnit.Texture1);
        public static Texture BedroomCleanSpecular = Texture.FromFile(ROOM_CLEAN_01 + "specular.jpg", TextureUnit.Texture2);
        
        private const string ROOM_CLEAN_01_CEILING_LAMPS = "table_clean_01_ceiling_lamps/";
        public static Texture BedroomCeilingLampsDiffuse = Texture.FromFile(ROOM_CLEAN_01_CEILING_LAMPS + "diffuse.jpg", TextureUnit.Texture0);
        public static Texture BedroomCeilingLampsNormal = Texture.FromFile(ROOM_CLEAN_01_CEILING_LAMPS + "normal.jpg", TextureUnit.Texture1);
        public static Texture BedroomCeilingLampsSpecular = Texture.FromFile(ROOM_CLEAN_01_CEILING_LAMPS + "specular.jpg", TextureUnit.Texture2);
        
        private const string ROOM_DIRTY_01_ = "table_dirty_01/";
        public static Texture BedroomDirtyDiffuse = Texture.FromFile(ROOM_DIRTY_01_ + "diffuse.jpg", TextureUnit.Texture0);
        public static Texture BedroomDirtyNormal = Texture.FromFile(ROOM_DIRTY_01_ + "normal.jpg", TextureUnit.Texture1);
        public static Texture BedroomDirtySpecular = Texture.FromFile(ROOM_DIRTY_01_ + "specular.jpg", TextureUnit.Texture2);
        
        private const string BASEMENT = "basement/";
        public static Texture BasementDiffuse = Texture.FromFile(BASEMENT + "diffuse.jpg", TextureUnit.Texture0);
        public static Texture BasementNormal = Texture.FromFile(BASEMENT + "normal.jpg", TextureUnit.Texture1);
        public static Texture BasementSpecular = Texture.FromFile(BASEMENT + "specular.jpg", TextureUnit.Texture2);
        
        private const string DOOR_OPEN = "door_open/";
        public static Texture DoorOpenDiffuse = Texture.FromFile(DOOR_OPEN + "diffuse.jpg", TextureUnit.Texture0);
        public static Texture DoorOpenNormal = Texture.FromFile(DOOR_OPEN + "normal.jpg", TextureUnit.Texture1);
        public static Texture DoorOpenSpecular = Texture.FromFile(DOOR_OPEN + "specular.jpg", TextureUnit.Texture2);
        
        private const string DOOR_OPEN_HANDLE = "door_open_handle/";
        public static Texture DoorOpenHandleDiffuse = Texture.FromFile(DOOR_OPEN_HANDLE + "diffuse.jpg", TextureUnit.Texture0);
        public static Texture DoorOpenHandleNormal = Texture.FromFile(DOOR_OPEN_HANDLE + "normal.jpg", TextureUnit.Texture1);
        public static Texture DoorOpenHandleSpecular = Texture.FromFile(DOOR_OPEN_HANDLE + "specular.jpg", TextureUnit.Texture2);
        
        private const string UBER_BAG = "uber_bag/";
        public static Texture UberBagDiffuse = Texture.FromFile  (UBER_BAG + "diffuse.jpg", TextureUnit.Texture0);
        public static Texture UberBagNormal = Texture.FromFile  (UBER_BAG + "normal.jpg", TextureUnit.Texture1);
        public static Texture UberBagSpecular = Texture.FromFile(UBER_BAG + "specular.jpg", TextureUnit.Texture2);
        
         public static Texture NoiseStatic = Texture.FromFile("noise_gaussian_2000.bmp", TextureUnit.Texture7);
    }
}