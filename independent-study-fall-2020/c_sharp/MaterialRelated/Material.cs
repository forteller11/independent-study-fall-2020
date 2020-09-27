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

        public AttributeBuffer[] GetAttribBuffersFromFBXFile(string fileName)
        {
            var fbxImporter = new Importer(fileName);
            var scene = fbxImporter.Import(SerializationManager.AssetPath + "\\" + fileName);
            return null;
        }
        public AttributeBuffer[] GetAttribBuffersFromObjFile(string fileName) //TODO collaspe indices' specefic attribs into one
        {
            var obj = JeremyAnsel.Media.WavefrontObj.ObjFile.FromFile(SerializationManager.AssetPath + "\\" + fileName);

            int vertStride = 3;
            float[] vertsFlattened = new float[obj.Faces.Count * vertStride * 3];
            int rootIndex = 0;
            for (int i = 0; i < obj.Faces.Count; i++)
            {
                if (obj.Faces[i].Vertices.Count != 3)
                    throw new DataException($"A face doesn't has \"{obj.Faces[i].Vertices.Count}\" and not 3 vertices, was the mesh not triangulated?");
                

                for (int j = 0; j < 3; j++)
                {
                    
                    
                    int vertexIndex = obj.Faces[i].Vertices[j].Vertex-1;
                    int texIndex = obj.Faces[i].Vertices[j].Texture-1;
                    int normIndex = obj.Faces[i].Vertices[j].Normal-1;
                    
                    vertsFlattened[rootIndex + 0] = obj.Vertices[vertexIndex].Position.X;
                    vertsFlattened[rootIndex + 1] = obj.Vertices[vertexIndex].Position.Y;
                    vertsFlattened[rootIndex + 2] = obj.Vertices[vertexIndex].Position.Z;
                    
                    rootIndex += 3;
                }
            }
            
//            Debug.GraphEnumerable(vertsFlattened);
            for (int i = 0; i < vertsFlattened.Length; i+=3)
                Debug.Log($"{vertsFlattened[i]}, {vertsFlattened[i+1]}, {vertsFlattened[i+2]}");
                
            
 
            
            var positionAttrib = new AttributeBuffer("in_position", vertStride, vertsFlattened);
//            var normalAttrib = new AttributeBuffer("in_normal", normalStride, normalsFlattened);
//            var uvAttrib = new AttributeBuffer("in_uv", uvStride, uvsFlattened);
            //todo indices
            
            return new [] {
                positionAttrib,
//                normalAttrib, 
//                uvAttrib
                
            };
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