
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
                var uniformName = GL.GetActiveUniform(Shader.Handle, i, out _, out var type);// TODO found it!!! it
                Debug.Log(type);
//                var uniformName = GL.GetActiveUniforms(Shader.Handle, i, );// TODO found it!!! it
                var location = GL.GetUniformLocation(Shader.Handle, uniformName);
                UniformLocations.Add(uniformName, location);
            }
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

//        public void SetVector4Array(string name, Vector4 [] vectors, bool useProgram = true, bool includeLength=true)
//        {
//            if (useProgram) Shader.Use();
//            
//            for (int i = 0; i < vectors.Length; i++)
//            {
//                if (UniformLocations.TryGetValue($"{name}[{i}]", out int location)) 
//                    GL.Uniform4(location, vectors.Length, vectors[i]);
//                else
//                    Debug.LogWarning($"Uniform \"{name}\" not found in shader program! Are you using it in your output? (optimized out?)");
//            }
//            SetInt(name + "Length", vectors.Length, useProgram);
//        }
        
        public void SetVector3Array(string name, float [] vectors, bool useProgram = true, bool includeLength=false)
        {
            if (useProgram) Shader.Use();
            
            if (UniformLocations.TryGetValue(name, out int location))
                    GL.Uniform3(location, vectors.Length, vectors);
            else
                Debug.LogWarning($"Uniform \"{name}\" not found in shader program! Are you using it in your output? (optimized out?)");
            
            if (includeLength)
                SetInt(name + "Length", vectors.Length, useProgram);
        }

        public void SetVector3Element(string name, Vector3 vector, bool useProgram, int index)
        {
            if (useProgram) Shader.Use();
            
            string indexedName = $"{name}[{index}]";
            var location = GL.GetUniformLocation(Shader.Handle, name);
            if (location != -1)
                GL.Uniform3(location, ref vector);
            else
                Debug.LogWarning($"Uniform \"{indexedName}\" not found in shader program! Are you using it in your output? (optimized out?)");
        }
        
        //todo send arrays again and FLATTEN
        
        public void SetVector4Element(string name, Vector4 vector, bool useProgram, int index)
        {
            if (useProgram) Shader.Use();
            
            string indexedName = $"{name}[{index}]";
            if (UniformLocations.TryGetValue(indexedName, out int location))
                GL.Uniform4(location, ref vector);
            else
                Debug.LogWarning($"Uniform \"{indexedName}\" not found in shader program! Are you using it in your output? (optimized out?)");
        }
        
        
        public void SetVector3(string name, Vector3 vector3, bool useProgram=true) //set useProgram to false for batch operations for performance gains
        {
            if (useProgram) Shader.Use();
            if (UniformLocations.TryGetValue(name, out int location))
                GL.Uniform3(location, ref vector3);
            else
                Debug.LogWarning($"Uniform \"{name}\" not found in shader program! Are you using it in your output? (optimized out?)");
        }
        
        public void SetInt(string name, int integer, bool useProgram=true) //set useProgram to false for batch operations for performance gains
        {
            if (useProgram) Shader.Use();
            if (UniformLocations.TryGetValue(name, out int location))
                GL.Uniform1(location, integer);
            else
                Debug.LogWarning($"Uniform \"{name}\" not found in shader program! Are you using it in your output? (optimized out?)");
        }
        
        public void SetVector4(string name, OpenTK.Vector4 vector4, bool useProgram=true) //set useProgram to false for batch operations for performance gains
        {
            if (useProgram) Shader.Use();
            if (UniformLocations.TryGetValue(name, out int location))
                GL.Uniform4(location, ref vector4);
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