using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;

namespace Indpendent_Study_Fall_2020
{
    public class Material //todo add uniforms, modify them,,,
    {
        public ShaderProgram Shader { get; private set; }
        private Dictionary<string, int> _uniformLocations = new Dictionary<string, int>();
        private List<Texture> _textures = new List<Texture>();
        public VertexArrayObject VAO;


        public Material(ShaderProgram shaderProgram)
        {
            Shader = shaderProgram;
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
            if (useProgram)
                Shader.Use();
            GL.UniformMatrix4(GL.GetUniformLocation(Shader.Handle, name), true, ref matrix4);
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