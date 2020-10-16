
using Indpendent_Study_Fall_2020.Helpers;
using Indpendent_Study_Fall_2020.MaterialRelated;
using OpenTK;

namespace Indpendent_Study_Fall_2020.c_sharp.Scripts
{
    /// <summary>
    /// Must be called BEFORE create materials
    /// </summary>
    public static class CreateMeshes
    {
        public static Mesh IcoSphereHighPoly;
        public static Mesh Plane;
        public static Mesh ViewSpaceQuad;

        public static void Create()
        {
            IcoSphereHighPoly =  ModelImporter.GetAttribBuffersFromObjFile("ico_sphere", true, true, true);
            Plane =  ModelImporter.GetAttribBuffersFromObjFile("Plane", true, true, true);

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