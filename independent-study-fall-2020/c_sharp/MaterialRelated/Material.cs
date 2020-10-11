
using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using Indpendent_Study_Fall_2020.Helpers;
using Indpendent_Study_Fall_2020.Scripts;
using Indpendent_Study_Fall_2020.Scripts.Materials;

namespace Indpendent_Study_Fall_2020.MaterialRelated
{
    
    /// <summary>
    /// Responsible for setting up a shader, it's textures, it's vertex attributes and setting its uniforms
    /// </summary>
    public class Material : IUniqueName
    {
        public readonly CreateMaterials.MaterialName Name;
        public readonly CreateFBOs.FBOName FBOName;
        public ShaderProgram Shader { get; private set; }
        public readonly Dictionary<string, int> UniformLocations;
        public readonly Dictionary<string, int> VertexAttribLocations;
        private List<Texture> _textures = new List<Texture>();
        public VAOAndBuffers VAO;


        public Material(CreateMaterials.MaterialName name, ShaderProgram shaderProgram, CreateFBOs.FBOName fboName = CreateFBOs.FBOName.Default)
        {
            Name = name;
            Shader = shaderProgram;
            FBOName = fboName;
            
            GL.GetProgram(Shader.Handle, GetProgramParameterName.ActiveUniforms, out int uniformCount);
            UniformLocations = new Dictionary<string, int>(uniformCount);
            Debug.Log(Name);
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

        
        public void SetupAndAttachTexture(string fileName, string samplerName, TextureUnit textureUnit)
        {
            var newTexture = Texture.FromFile(fileName, textureUnit);
            AttachTexture(newTexture, samplerName, textureUnit);
        }

        public void AttachTexture(Texture texture, string samplerName, TextureUnit textureUnit)
        {
            if (texture.TextureUnit != textureUnit)
                throw new ArgumentException($"TextureUnit setup in {Name} does not match TextureUnit set in Texture... must be identical!");
            
            Shader.Use();
            _textures.Add(texture);
            Shader.SetUniformInt(samplerName, textureUnit.ToIndex());
            texture.UploadToShader();
        }
        
        public void AttachFrameBuffer(FBO fbo, string samplerName, TextureUnit textureUnit)
        {
            if (fbo.Texture.TextureUnit != textureUnit)
                throw new ArgumentException($"TextureUnit setup in {Name} does not match TextureUnit set in FrameBuffer's texture... must be identical!");
            
            Shader.Use();
            _textures.Add(fbo.Texture);
            Shader.SetUniformInt(samplerName, textureUnit.ToIndex());
            fbo.Texture.UploadToShader();
        }

        public void UseAllAttachedTextures()
        {
            for (int i = 0; i < _textures.Count; i++)
                _textures[i].Use();
        }
        

        public void PrepareBatchForDrawing()
        {
            Shader.Use();
            UseAllAttachedTextures();
            GL.BindVertexArray(VAO.VAOHandle);
        }

        public string GetUniqueName() => Name.ToString();


    }
}