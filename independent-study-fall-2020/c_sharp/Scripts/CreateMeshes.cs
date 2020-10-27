
using CART_457.Helpers;
using CART_457.MaterialRelated;
using OpenTK;

namespace CART_457.Scripts
{
    public static class CreateMeshes
    {
        public static Mesh IcoSphereHighPoly;
        public static Mesh TableProto;
        public static Mesh Eyeball;
        public static Mesh Plane;
        public static Mesh ViewSpaceQuad;

        static CreateMeshes()
        {
            IcoSphereHighPoly =  ModelImporter.GetAttribBuffersFromObjFile("ico_sphere", true, true, true);
            TableProto = ModelImporter.GetAttribBuffersFromObjFile("room_proto_table", true, true, true);
            Plane =  ModelImporter.GetAttribBuffersFromObjFile("Plane", true, true, true);
            Eyeball =  ModelImporter.GetAttribBuffersFromObjFile("eyeball", true, true, true);

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