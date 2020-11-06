
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

        static SetupMeshes()
        {
            IcoSphereHighPoly =  ModelImporter.GetAttribBuffersFromObjFile("ico_sphere", true, true, true);
            TableProto = ModelImporter.GetAttribBuffersFromObjFile("room_proto_table", true, true, true);
            Plane =  ModelImporter.GetAttribBuffersFromObjFile("Plane", true, true, true);
            Eyeball =  ModelImporter.GetAttribBuffersFromObjFile("eyeball", true, true, true);
            Diamond =  ModelImporter.GetAttribBuffersFromObjFile("camera_visualization", true, true, true);
            Webcam =  ModelImporter.GetAttribBuffersFromObjFile("webcam", true, true, true);
            WeirdHead =  ModelImporter.GetAttribBuffersFromObjFile("head", true, true, true);
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