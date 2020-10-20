
using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using CART_457.Helpers;
using CART_457.Scripts;

namespace CART_457.MaterialRelated
{
    
    /// <summary>
    /// Responsible for setting up a shader, it's textures, it's vertex attributes and setting its uniforms
    /// </summary>
    public class Material
    {
        public FBO RenderTarget { get; private set; }
        public ShaderProgram Shader { get; private set; }
        
        public Dictionary<string, int> UniformLocations { get; private set; }
        public Dictionary<string, int> VertexAttribLocations { get; private set; }
        public VAOAndBuffers VAO { get; private set; }
        
        private List<Texture> _textures = new List<Texture>();
        
        public Action<Material> PerMaterialUniformSender;

        public const string MODEL_TO_VIEW_UNIFORM = "ModelToView";
        public const string WORLD_TO_VIEW_UNIFORM = "WorldToView";
        public const string MODEL_TO_WORLD_UNIFORM = "ModelToWorld";
        public const string MODEL_ROTATION_UNIFORM = "ModelRotation";
        public const string MODEL_TO_WORLD_NO_PROJECTION_UNIFORM = "ModelToWorldNoProjection";
        public const string CAM_POSITION_UNIFORM = "CamPosition";
        
        public const string DIFFUSE_SAMPLER = "Diffuse";
        public const string SPECULAR_MAP_SAMPLER = "Gloss";
        public const string NORMAL_MAP_SAMPLER = "Normal";
        
        public const string MAIN_COLOR_SAMPLER = "MainColor";
        public const string SECONDARY_COLOR_SAMPLER = "SecondaryColor";
        public const string MAIN_DEPTH_SAMPLER = "MainDepth";

        public const string SHADOW_MAP_SAMPLER = "ShadowMap";
        
        
        private const bool DEBUG = false;

        private Material() { }
        
        
        public static Material EntityBased(FBO fbo, ShaderProgram shaderProgram, Mesh mesh, Action<Material> perMaterialUniformSender)
        {
            var mat = new Material();
            mat.Shader = shaderProgram;
            mat.PerMaterialUniformSender = perMaterialUniformSender;
            mat.RenderTarget = fbo;
            mat.GetUniformAndAttribLocations();
            mat.VAO = new VAOAndBuffers(mat, mesh);
            return mat;
        }
         public static Material PostProcessing(ShaderProgram shaderProgram)
         {
             var mat = new Material();
             mat.Shader = shaderProgram;
             mat.RenderTarget = FboSetup.PostProcessing;
             mat.GetUniformAndAttribLocations();
             mat.VAO = new VAOAndBuffers(mat, CreateMeshes.ViewSpaceQuad);
             mat.PerMaterialUniformSender = _ => FboSetup.Main.UseTexturesAndGenerateMipMaps();
             return mat;
         }

         /// <summary>
         /// must be called before setting up vao
         /// </summary>
        public void GetUniformAndAttribLocations()
        {
            GL.GetProgram(Shader.Handle, GetProgramParameterName.ActiveUniforms, out int uniformCount);
            UniformLocations = new Dictionary<string, int>(uniformCount);
            Debug.Log(GetType().Name);
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
                throw new Exception($"Attribute {name} not found at material {this.GetType().Name}");
        }

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
        
    }
}