using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using FbxSharp;
using OpenTK.Graphics.OpenGL4;

namespace Indpendent_Study_Fall_2020.MaterialRelated
{
    public class Material //todo add uniforms, modify them,,,
    {
        public readonly string Name;
        public ShaderProgram Shader { get; private set; }
        public readonly Dictionary<string, int> UniformLocations;
        private List<Texture> _textures = new List<Texture>();
        public VAOAndBuffers VAO;
        private const int POINTS_IN_TRIANGLE = 3;


        public Material(string name, ShaderProgram shaderProgram)
        {
            Name = name;
            Shader = shaderProgram;
            
            GL.GetProgram(Shader.Handle, GetProgramParameterName.ActiveUniforms, out int uniformCount);
            UniformLocations = new Dictionary<string, int>(uniformCount);
            for (int i = 0; i < uniformCount; i++)
            {
                var uniformName = GL.GetActiveUniform(Shader.Handle, i, out _, out _);
                var location = GL.GetUniformLocation(Shader.Handle, uniformName);
                UniformLocations.Add(uniformName, location);
            }
        }
        
        public AttributeBuffer[] GetAttribBuffersFromObjFile(string fileName, bool in_position, bool in_uv, bool in_normal) //TODO collaspe indices' specefic attribs into one
        {
            var obj = JeremyAnsel.Media.WavefrontObj.ObjFile.FromFile(SerializationManager.AssetPath + "\\" + fileName);
            
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
                    int vertexIndex = obj.Faces[i].Vertices[j].Vertex-1;
                    int texIndex = obj.Faces[i].Vertices[j].Texture-1;
                    int normIndex = obj.Faces[i].Vertices[j].Normal-1;
                    
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
            
            for (int i = 0; i < positionsFlattened.Length; i+=3)
                Debug.Log($"{positionsFlattened[i]}, {positionsFlattened[i+1]}, {positionsFlattened[i+2]}");


            List<AttributeBuffer> result = new List<AttributeBuffer>(3);
            
            if (in_position) 
                result.Add(new AttributeBuffer("in_position", positionsStride, positionsFlattened));
            if (in_uv) 
                result.Add(new AttributeBuffer("in_uv", uvsStride, uvsFlattened));
            if (in_normal) 
                result.Add(new AttributeBuffer("in_normal", normalsStride, normalsFlattened));

            return result.ToArray();
        }
        public void FeedBufferAndIndicesData(uint[] indices, params AttributeBuffer[] attributeBuffers)
        {
            VAO = new VAOAndBuffers(Shader, indices, attributeBuffers);
     
        }

       

        public void SetupATexture(string fileName, string samplerName, TextureUnit textureUnitEnum, int textureUnitIndex)
        {
            Shader.Use();
            var newTexture = new Texture(fileName, samplerName, textureUnitEnum);
            _textures.Add(newTexture);
            Shader.SetUniformInt(samplerName, textureUnitIndex);
            newTexture.UploadToShader();
        }
        
        
        public void SetMatrix4(string name, OpenTK.Matrix4 matrix4, bool useProgram=true) //set useProgram to false for batch operations for performance gains
        {
            if (useProgram) Shader.Use();
            if (UniformLocations.TryGetValue(name, out int location))
                GL.UniformMatrix4(location, true, ref matrix4);
            else
                Debug.LogWarning($"Uniform \"{name}\" not found in shader program! Are you using it in your output? (optimized out?)");
        }
        public void SetVector3(string name, OpenTK.Vector3 vector3, bool useProgram=true) //set useProgram to false for batch operations for performance gains
        {
            if (useProgram) Shader.Use();
            if (UniformLocations.TryGetValue(name, out int location))
                GL.Uniform3(location, ref vector3);
            else
                Debug.LogWarning($"Uniform \"{name}\" not found in shader program! Are you using it in your output? (optimized out?)");
        }

        public void PrepareAndDraw()
        {
            PrepareBatchForDrawing();
            Draw();
        }

        public void PrepareBatchForDrawing()
        {
            Shader.Use();
            for (int i = 0; i < _textures.Count; i++)
                _textures[i].Use();
            GL.BindVertexArray(VAO.VAOHandle);
//            GL.BindBuffer(BufferTarget.ArrayBuffer, VAO.IndicesHandle);
        }

        public void Draw()
        {
            if (VAO.UseIndices == false)
            {
//                Debug.Log("draw arrays");
                GL.DrawArrays(PrimitiveType.Triangles, 0, VAO.VerticesCount);
            }
            else
            {
//                Debug.Log("draw elements");
                GL.DrawElements(PrimitiveType.Triangles, VAO.IndicesBuffer.Length, DrawElementsType.UnsignedInt, 0);
//                GL.DrawElementsInstanced(PrimitiveType.Triangles,
//                    0,
//                    DrawElementsType.UnsignedInt,
//                    Indices,
//                    ref VAO.VerticesCount);
            }

            //todo
//            GL.DrawElementsInstanced(PrimitiveType.Triangles, 3, DrawElementsType.UnsignedInt, ref Indices, int 0);
        }
    }
}