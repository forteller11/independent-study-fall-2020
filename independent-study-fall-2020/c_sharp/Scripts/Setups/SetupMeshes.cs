
using System;
using CART_457.Helpers;
using CART_457.MaterialRelated;
using OpenTK;
using OpenTK.Mathematics;

namespace CART_457.Scripts.Setups
{
    public static class SetupMeshes
    {
        public static Mesh IcoSphereHighPoly = ModelImporter.GetAttribBuffersFromObjFile("ico_sphere");
        public static Mesh TableProto = ModelImporter.GetAttribBuffersFromObjFile("room_proto_table");
        public static Mesh Eyeball =  ModelImporter.GetAttribBuffersFromObjFile("eyeball", Quaternion.FromEulerAngles(-MathF.PI/2,0f,0f ));
        public static Mesh Webcam = ModelImporter.GetAttribBuffersFromObjFile("webcam");
        public static Mesh Plane = ModelImporter.GetAttribBuffersFromObjFile("Plane");
        public static Mesh WeirdHead = ModelImporter.GetAttribBuffersFromObjFile("head");
        public static Mesh Path = ModelImporter.GetAttribBuffersFromObjFile("path");
        
        public static Mesh RoomClean01 = ModelImporter.GetAttribBuffersFromObjFile("room_clean_01");
        public static Mesh RoomCleanCeilingLamp01 = ModelImporter.GetAttribBuffersFromObjFile("room_clean_01_ceiling_lamps");
        public static Mesh RoomClean01Colliders = ModelImporter.GetAttribBuffersFromObjFile("room_clean_01_colliders");
        public static Mesh RoomDirty01 = ModelImporter.GetAttribBuffersFromObjFile("room_dirty_01");
        public static Mesh BasementFloorColliders = ModelImporter.GetAttribBuffersFromObjFile("basement_stairs_test");
        
        public static Mesh ViewSpaceQuad;

        static SetupMeshes()
        {
  

            

            #region ViewQuad
            var quadPositions = new []
            {
                new Vector3(1,1,0), //upper right
                new Vector3(-1,1,0), 
                new Vector3(-1,-1,0), 
                
                new Vector3(1,1,0),
                new Vector3(-1,-1,0), 
                new Vector3(1,-1,0), //lower right
            };
            
            var quadUvs = new []
            {
                new Vector2(1,1), //upper right
                new Vector2(0,1), 
                new Vector2(0,0),
                
                new Vector2(1,1), 
                new Vector2(0,0), 
                new Vector2(1,0),  //lower right
            };
            ViewSpaceQuad =  new Mesh(AttributeBuffer.PositionAttribute(quadPositions), AttributeBuffer.UVAttribute(quadUvs), null );
            #endregion
        }
    }
}