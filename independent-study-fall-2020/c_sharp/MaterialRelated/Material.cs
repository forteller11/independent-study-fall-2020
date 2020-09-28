﻿
using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL4;


namespace Indpendent_Study_Fall_2020.MaterialRelated
{
    public class Material //todo add uniforms, modify them,,,
    {
        public readonly string Name;
        public ShaderProgram Shader { get; private set; }
        public readonly Dictionary<string, int> UniformLocations;
        public readonly Dictionary<string, int> VertexAttribLocations;
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
                var uniformName = GL.GetActiveUniform(Shader.Handle, i, out int size, out var type);
                Debug.Log(uniformName);
                Debug.Log(type);
                Debug.Log("size: " + size);
                Debug.Log('\n');
                int location = GL.GetUniformLocation(Shader.Handle, uniformName);
                UniformLocations.Add(uniformName, location);
            }

            GL.GetProgram(Shader.Handle, GetProgramParameterName.ActiveAttributes, out int attribCount);
            VertexAttribLocations = new Dictionary<string, int>(attribCount);
            for (int i = 0; i < attribCount; i++)
            {
                var attribName = GL.GetActiveAttrib(Shader.Handle, i, out int size, out var type);
                int location = GL.GetAttribLocation(Shader.Handle, attribName);
                VertexAttribLocations.Add(attribName, location);
            }
            
        }

        public int GetAttribLocation(string name)
        {
            if (VertexAttribLocations.TryGetValue(name, out int location))
                return location;
            else
                throw new Exception($"Attribute {name} not found at material {Name}");
        }
        
        public void FeedBuffersAndCreateVAO(uint[] indices, params AttributeBuffer[] attributeBuffers)
        {
            VAO = new VAOAndBuffers(this, indices, attributeBuffers);
        }

       

        public void SetupATexture(string fileName, string samplerName, TextureUnit textureUnitEnum, int textureUnitIndex)
        {
            Shader.Use();
            var newTexture = new Texture(fileName, samplerName, textureUnitEnum);
            _textures.Add(newTexture);
            Shader.SetUniformInt(samplerName, textureUnitIndex);
            newTexture.UploadToShader();
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
//            if (VAO.UseIndices == false)
//            {
////                Debug.Log("draw arrays");
//                GL.DrawArrays(PrimitiveType.Triangles, 0, VAO.VerticesCount);
//            }
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