using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;

namespace Indpendent_Study_Fall_2020
{
    public class Material
    {
        private ShaderProgram _shader;
        private Dictionary<string, int> Attrib;
        private List<Uniform> _uniforms;
        private List<Texture> _textures = new List<Texture>();
        public VertexArrayObject VAO;


        public Material(ShaderProgram shaderProgram)
        {
            _shader = shaderProgram;
        }
        
        public void SetupVAO(params AttributeBuffer[] attributeBuffers)
        {
            VAO = new VertexArrayObject(_shader, attributeBuffers);
        }

        public void SetupATexture(string fileName, string samplerName, TextureUnit textureUnitEnum, int textureUnitIndex)
        {
            _shader.Use();
            var newTexture = new Texture(fileName, samplerName, textureUnitEnum);
            _textures.Add(newTexture);
            _shader.SetUniformInt(samplerName, textureUnitIndex);
            newTexture.UploadToShader();
        }


        
        public void SetupUniform()
        {
            //todo
        }
        public void Draw()
        {
            _shader.Use();
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