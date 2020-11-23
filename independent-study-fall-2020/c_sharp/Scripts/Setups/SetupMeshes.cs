
using System;
using CART_457.Helpers;
using CART_457.MaterialRelated;
using OpenTK;
using OpenTK.Mathematics;

namespace CART_457.Scripts.Setups
{
    public static class SetupMeshes
    {
        public static Mesh IcoSphereHighPoly;
        public static Mesh TableProto;
        public static Mesh Eyeball;
        public static Mesh Diamond;
        public static Mesh Webcam;
        public static Mesh Plane;
        public static Mesh ViewSpaceQuad;
        public static Mesh WeirdHead;
        public static Mesh Path;
        public static Mesh RoomClean01;
        public static Mesh RoomCleanCeilingLamp01;
        public static Mesh RoomClean01Colliders;
        public static Mesh RoomDirty01;

        static SetupMeshes()
        {
            IcoSphereHighPoly =  ModelImporter.GetAttribBuffersFromObjFile("ico_sphere");
            TableProto = ModelImporter.GetAttribBuffersFromObjFile("room_proto_table");
            Plane =  ModelImporter.GetAttribBuffersFromObjFile("Plane");
            Eyeball =  ModelImporter.GetAttribBuffersFromObjFile("eyeball", Quaternion.FromEulerAngles(-MathF.PI/2,0f,0f ));
            Diamond =  ModelImporter.GetAttribBuffersFromObjFile("camera_visualization");
            Webcam =  ModelImporter.GetAttribBuffersFromObjFile("webcam");
            WeirdHead =  ModelImporter.GetAttribBuffersFromObjFile("head");
            Path =  ModelImporter.GetAttribBuffersFromObjFile("path");
            
            RoomClean01 =  ModelImporter.GetAttribBuffersFromObjFile("room_clean_01");
            RoomCleanCeilingLamp01 =  ModelImporter.GetAttribBuffersFromObjFile("room_clean_01_ceiling_lamps");
            
            RoomDirty01 =  ModelImporter.GetAttribBuffersFromObjFile("room_dirty_01");
            
            RoomClean01Colliders =  ModelImporter.GetAttribBuffersFromObjFile("room_clean_01_colliders");
           
            // Diamond =  ModelImporter.GetAttribBuffersFromObjFile("room_proto_table", true, true, true);

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