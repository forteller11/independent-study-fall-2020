using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;

namespace Indpendent_Study_Fall_2020.MaterialRelated
{
    public class Material //todo add uniforms, modify them,,,
    {
        public readonly string Name;
        public ShaderProgram Shader { get; private set; }
        public readonly Dictionary<string, int> UniformLocations;
        private List<Texture> _textures = new List<Texture>();
        public VertexArrayObject VAO;


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

        public AttributeBuffer[] GetAttribBuffersFromObjFile(string fileName)
        {
            var obj = JeremyAnsel.Media.WavefrontObj.ObjFile.FromFile(SerializationManager.AssetPath + "\\" + fileName);

            int vertStride = 3;
            float[] vertsFlattened = new float[obj.Vertices.Count * vertStride];
            for (int i = 0; i < obj.Vertices.Count; i++)
            {
                int rootIndex = i * vertStride;
                vertsFlattened[rootIndex + 0] = obj.Vertices[i].Position.X;
                vertsFlattened[rootIndex + 1] = obj.Vertices[i].Position.Y;
                vertsFlattened[rootIndex + 2] = obj.Vertices[i].Position.Z;
            }

            int uvStride = 2;
            float[] uvsFlattened = new float[obj.TextureVertices.Count * uvStride];
            for (int i = 0; i < obj.TextureVertices.Count; i++)
            {
                int rootIndex = i * uvStride;
                uvsFlattened[rootIndex + 0] = obj.TextureVertices[i].X;
                uvsFlattened[rootIndex + 1] = obj.TextureVertices[i].Y;
            }
            
            var positionAttrib = new AttributeBuffer("in_position", vertStride, uvsFlattened);
            var uvAttrib = new AttributeBuffer("in_uv", uvStride, uvsFlattened);
            
            //todo normals
            return new [] {positionAttrib, uvAttrib};
        }
        public void SetupVAOFromAttribBuffers(params AttributeBuffer[] attributeBuffers)
        {
            VAO = new VertexArrayObject(Shader, attributeBuffers);
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
            GL.DrawArrays(PrimitiveType.Triangles, 0, VAO.VerticesCount);
        }

        public void PrepareBatchForDrawing()
        {
            Shader.Use();
            for (int i = 0; i < _textures.Count; i++)
                _textures[i].Use();
            GL.BindVertexArray(VAO.VAOHandle);
        }

        public void Draw()
        {
            GL.DrawArrays(PrimitiveType.Triangles, 0, VAO.VerticesCount);
        }
        
        
        
        //todo, automatically assign unifroms and textures
    }
}