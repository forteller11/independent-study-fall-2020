
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
    public class Material : ITypeID
    {
        public readonly CreateMaterials.MaterialType Type;
        public readonly CreateFBOs.FBOType FBOType;
        public ShaderProgram Shader { get; private set; }
        public readonly Dictionary<string, int> UniformLocations;
        public readonly Dictionary<string, int> VertexAttribLocations;
        private List<Texture> _textures = new List<Texture>();
        
        public Action<Material> PerMaterialAttributeSender;
        private const bool DEBUG = false; 

        public Material(CreateMaterials.MaterialType type, CreateFBOs.FBOType fboType, ShaderProgram shaderProgram, Action<Material> perMaterialAttributeSender)
        {
            Type = type;
            Shader = shaderProgram;
            PerMaterialAttributeSender = perMaterialAttributeSender;
            FBOType = fboType;
            
            GL.GetProgram(Shader.Handle, GetProgramParameterName.ActiveUniforms, out int uniformCount);
            UniformLocations = new Dictionary<string, int>(uniformCount);
            Debug.Log(Type);
            for (int i = 0; i < uniformCount; i++) 
            {
                var uniformName = GL.GetActiveUniform(Shader.Handle, i, out int size, out var uniformType);
                if (DEBUG)
                {
                    Debug.Log(uniformName);
                    Debug.Log(type);
                    Debug.Log("size: " + size);
                    Debug.Log('\n');
                }

                int location = GL.GetUniformLocation(Shader.Handle, uniformName);
                UniformLocations.Add(uniformName, location);
            }

            GL.GetProgram(Shader.Handle, GetProgramParameterName.ActiveAttributes, out int attribCount);
            VertexAttribLocations = new Dictionary<string, int>(attribCount);
            for (int i = 0; i < attribCount; i++)
            {
                var attribName = GL.GetActiveAttrib(Shader.Handle, i, out int size, out var uniformType);
                int location = GL.GetAttribLocation(Shader.Handle, attribName);
                VertexAttribLocations.Add(attribName, location);
            }
            
        }

        public int GetAttribLocation(string name)
        {
            if (VertexAttribLocations.TryGetValue(name, out int location))
                return location;
            else
                throw new Exception($"Attribute {name} not found at material {Type}");
        }
        
        public void FeedBuffersAndCreateVAO(uint[] indices, params AttributeBuffer[] attributeBuffers)
        {
            VAO = new VAOAndBuffers(this, indices, attributeBuffers);
        }

        
        // public void SetupATexture(string fileName, string samplerName, TextureUnit textureUnitEnum)
        // {
        //     Shader.Use();
        //     var newTexture = Texture.FromFile(fileName, textureUnitEnum);
        //     _textures.Add(newTexture);
        //     Shader.SetUniformInt(samplerName, textureUnitEnum.ToIndex());
        // }

        public void SetupSampler(string samplerName, Texture texture)
        {
            Shader.Use();
            _textures.Add(texture);
            Shader.SetUniformInt(samplerName, texture.TextureUnit.ToIndex());
        }

        public void UseAllAttachedTextures()
        {
            for (int i = 0; i < _textures.Count; i++)
                _textures[i].Use();
        }

        public void SetDrawingStates()
        {
            Shader.Use();
            UseAllAttachedTextures();
            PerMaterialAttributeSender?.Invoke(this);
            GL.BindVertexArray(VAO.VAOHandle);
        }

        public int GetTypeID() => (int) Type;
    }
}