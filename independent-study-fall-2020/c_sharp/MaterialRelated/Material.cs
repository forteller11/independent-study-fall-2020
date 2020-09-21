using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;

namespace Indpendent_Study_Fall_2020
{
    public class Material //todo add uniforms, modify them,,,
    {
        public ShaderProgram Shader { get; private set; }
        public readonly Dictionary<string, int> UniformLocations;
        private List<Texture> _textures = new List<Texture>();
        public VertexArrayObject VAO;


        public Material(ShaderProgram shaderProgram)
        {
            Shader = shaderProgram;
            
            GL.GetProgram(Shader.Handle, GetProgramParameterName.ActiveUniforms, out int uniformCount);
            UniformLocations = new Dictionary<string, int>(uniformCount);
            for (int i = 0; i < uniformCount; i++)
            {
                var name = GL.GetActiveUniform(Shader.Handle, i, out _, out var type);
                var location = GL.GetUniformLocation(Shader.Handle, name);
                UniformLocations.Add(name,location);
            }
        }
        
        public void SetupVAO(params AttributeBuffer[] attributeBuffers)
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
                Debug.LogWarning($"Uniform {name} not found in shader program!");
        }
        public void SetVector3(string name, OpenTK.Vector3 vector3, bool useProgram=true) //set useProgram to false for batch operations for performance gains
        {
            if (useProgram) Shader.Use();
            if (UniformLocations.TryGetValue(name, out int location))
                GL.Uniform3(location, ref vector3);
            else
                Debug.LogWarning($"Uniform {name} not found in shader program!");
        }

        public void Draw()
        {
            Shader.Use();
            for (int i = 0; i < _textures.Count; i++)
                _textures[i].Use();
            
            //set uniforms here
            
            //.................
            
            GL.BindVertexArray(VAO.VAOHandle);
            GL.DrawArrays(PrimitiveType.Triangles, 0, VAO.VerticesCount);
        }
        
        //todo, automatically assign unifroms and textures
    }
}