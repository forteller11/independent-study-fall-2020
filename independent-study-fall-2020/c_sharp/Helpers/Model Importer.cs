using System.Collections.Generic;
using System.Data;
using Indpendent_Study_Fall_2020.MaterialRelated;

namespace Indpendent_Study_Fall_2020.Helpers
{
    public class ModelImporter
    {
        private const int POINTS_IN_TRIANGLE = 3;
        public static AttributeBuffer[] GetAttribBuffersFromObjFile(string fileName, bool in_position=true, bool in_uv=true, bool in_normal=true) //TODO collaspe indices' specefic attribs into one
        {
            var obj = JeremyAnsel.Media.WavefrontObj.ObjFile.FromFile(SerializationManager.MeshPath + "\\" + fileName + ".obj");
            
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
                    int vertexIndex = obj.Faces[i].Vertices[j].Vertex -1;
                    int texIndex    = obj.Faces[i].Vertices[j].Texture-1;
                    int normIndex   = obj.Faces[i].Vertices[j].Normal -1;
                    
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
            
//            for (int i = 0; i < positionsFlattened.Length; i+=3)
//                Debug.Log($"{positionsFlattened[i]}, {positionsFlattened[i+1]}, {positionsFlattened[i+2]}");
//            Debug.Log("UVS====================");
//            for (int i = 0; i < uvsFlattened.Length; i+=3)
//                Debug.Log($"{uvsFlattened[i]}, {uvsFlattened[i+1]}");


            List<AttributeBuffer> result = new List<AttributeBuffer>(3);
            
            if (in_position) 
                result.Add(new AttributeBuffer("in_position", positionsStride, positionsFlattened));
            if (in_uv) 
                result.Add(new AttributeBuffer("in_uv", uvsStride, uvsFlattened));
            if (in_normal) 
                result.Add(new AttributeBuffer("in_normal", normalsStride, normalsFlattened));

            return result.ToArray();
        }
    }
}