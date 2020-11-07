using System.Collections.Generic;
using System.Data;
using CART_457.MaterialRelated;
using JeremyAnsel.Media.WavefrontObj;
using OpenTK.Mathematics;

namespace CART_457.Helpers
{
    public class ModelImporter
    {
        private const int POINTS_IN_TRIANGLE = 3;

        public static Mesh GetAttribBuffersFromObjFile(string fileName) => GetAttribBuffersFromObjFile(fileName, Quaternion.Identity, true, true, true);
        public static Mesh GetAttribBuffersFromObjFile(string fileName, Quaternion rotationToBakeIn) => GetAttribBuffersFromObjFile(fileName, rotationToBakeIn, true, true, true);
        public static Mesh GetAttribBuffersFromObjFile(string fileName, Quaternion? rotationToBakeIn, bool in_position, bool in_uv, bool in_normal) //TODO collaspe indices' specefic attribs into one
        {
            var obj = JeremyAnsel.Media.WavefrontObj.ObjFile.FromFile(SerializationManager.MeshPath + "\\" + fileName + ".obj");
            
            #region bake in rotation

            if (rotationToBakeIn.HasValue)
            {
                var rotation = rotationToBakeIn.Value;
                for (int i = 0; i < obj.Vertices.Count; i++)
                {
                    var color = obj.Vertices[i].Color ?? new ObjVector4(0, 0, 0,1);
                    var positionTK = obj.Vertices[i].Position.ToTKVector3();
                    positionTK = rotation * positionTK;
                    obj.Vertices[i] = new ObjVertex(positionTK.X, positionTK.Y,positionTK.Z, color.X, color.Y, color.Z);
                }

                for (int i = 0; i < obj.VertexNormals.Count; i++)
                {
                    var normalTK = obj.VertexNormals[i].ToTKVector3();
                    normalTK = rotation * normalTK;
                    obj.VertexNormals[i] = normalTK.ToOBJVector3();
                }
            }
            
            #endregion
            
            int positionsStride = 3;
            int uvsStride = 3;
            int normalsStride = 3;
            
            float[] positionsFlattened = new float[obj.Faces.Count * positionsStride * POINTS_IN_TRIANGLE];
            float[] uvsFlattened = new float[obj.Faces.Count * uvsStride * POINTS_IN_TRIANGLE];
            float[] normalsFlattened = new float[obj.Faces.Count * normalsStride * POINTS_IN_TRIANGLE];
            
            int rootIndex = 0;
            for (int i = 0; i < obj.Faces.Count; i++)
            {
                if (obj.Faces[i].Vertices.Count != POINTS_IN_TRIANGLE)
                    throw new DataException($"A face doesn't has \"{obj.Faces[i].Vertices.Count}\" and not {POINTS_IN_TRIANGLE} vertices, was the mesh not triangulated?");
                
                for (int j = 0; j < POINTS_IN_TRIANGLE; j++)
                {
                    int vertexIndex = obj.Faces[i].Vertices[j].Vertex  -1;
                    int texIndex    = obj.Faces[i].Vertices[j].Texture -1;
                    int normIndex   = obj.Faces[i].Vertices[j].Normal  -1;
                    
                    positionsFlattened[rootIndex + 0] = obj.Vertices[vertexIndex].Position.X;
                    positionsFlattened[rootIndex + 1] = obj.Vertices[vertexIndex].Position.Y;
                    positionsFlattened[rootIndex + 2] = obj.Vertices[vertexIndex].Position.Z;

                    uvsFlattened[rootIndex + 0] = obj.TextureVertices[texIndex].X;
                    uvsFlattened[rootIndex + 1] = obj.TextureVertices[texIndex].Y;
                    
                    normalsFlattened[rootIndex + 0] = obj.VertexNormals[normIndex].X;
                    normalsFlattened[rootIndex + 1] = obj.VertexNormals[normIndex].Y;
                    normalsFlattened[rootIndex + 2] = obj.VertexNormals[normIndex].Z;

                    rootIndex += 3;
                }
            }
            
            
            
            var positionsBuffer = in_position ? new AttributeBuffer("in_position", positionsStride, positionsFlattened) : null;
            var uvsBuffer = in_uv ? new AttributeBuffer("in_uv", uvsStride, uvsFlattened) : null;
            var normalsbuffer = in_normal ?new AttributeBuffer("in_normal", normalsStride, normalsFlattened) : null;

            
            return new Mesh(positionsBuffer, uvsBuffer, normalsbuffer);
        }
        

        
    }
}