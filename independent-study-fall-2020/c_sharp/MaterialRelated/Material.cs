
using System;
using System.Collections.Generic;
using Indpendent_Study_Fall_2020.c_sharp.Scripts;
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
        public MaterialFactory.MaterialType Type { get; private set; }
        public CreateFBOs.FBOType FBOType { get; private set; }
        public bool IsPostProcessing { get; private set; }
        // public int PostFXOrder { get; private set; } //where -1 is invalid, 0 is first, and pos-infinity is last
        public ShaderProgram Shader { get; private set; }
        
        public Dictionary<string, int> UniformLocations { get; private set; }
        public Dictionary<string, int> VertexAttribLocations { get; private set; }
        public VAOAndBuffers VAO { get; private set; }
        
        private List<Texture> _textures = new List<Texture>();
        
        public Action<Material> PerMaterialUniformSender;
        
        private const bool DEBUG = false;

        private Material() { }

    

        // public static Material EntityBased(CreateMaterials.MaterialType type, CreateFBOs.FBOType fboType, ShaderProgram shaderProgram, VAOAndBuffers vaoAndBuffers, Action<Material> perMaterialUniformSender)
        // {
        //     var mat = new Material();
        //     mat.Type = type;
        //     mat.Shader = shaderProgram;
        //     mat.PerMaterialUniformSender = perMaterialUniformSender;
        //     mat.FBOType = fboType;
        //     mat.VAO = vaoAndBuffers;
        //     mat.GetUniformAndAttribLocations();
        //     return mat;
        // }
        
        public static Material EntityBased(MaterialFactory.MaterialType type, CreateFBOs.FBOType fboType, ShaderProgram shaderProgram, Mesh mesh, Action<Material> perMaterialUniformSender)
        {
            var mat = new Material();
            mat.Type = type;
            mat.Shader = shaderProgram;
            mat.PerMaterialUniformSender = perMaterialUniformSender;
            mat.FBOType = fboType;
            mat.GetUniformAndAttribLocations();
            mat.VAO = new VAOAndBuffers(mat, mesh);
            return mat;
        }
         public static Material PostProcessing(ShaderProgram shaderProgram)
         {
             var mat = new Material();
             mat.Shader = shaderProgram;
             mat.FBOType = CreateFBOs.FBOType.Default;
             mat.Type = MaterialFactory.MaterialType.PostProcessing;
             // mat.PostFXOrder = order;
             mat.GetUniformAndAttribLocations();
             mat.VAO= new VAOAndBuffers(mat, CreateMeshes.ViewSpaceQuad);
             return mat;
         }

         /// <summary>
         /// must be called before setting up vao
         /// </summary>
        public void GetUniformAndAttribLocations()
        {
            GL.GetProgram(Shader.Handle, GetProgramParameterName.ActiveUniforms, out int uniformCount);
            UniformLocations = new Dictionary<string, int>(uniformCount);
            Debug.Log(Type);
            for (int i = 0; i < uniformCount; i++) 
            {
                var uniformName = GL.GetActiveUniform(Shader.Handle, i, out int size, out var uniformType);
                if (DEBUG)
                {
                    Debug.Log(uniformName);
                    Debug.Log(uniformType);
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
            PerMaterialUniformSender?.Invoke(this);
            GL.BindVertexArray(VAO.VAOHandle);
        }

        public int GetTypeID() => (int) Type;
    }
}