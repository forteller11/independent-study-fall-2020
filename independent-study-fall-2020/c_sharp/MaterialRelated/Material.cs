
using System;
using System.Collections.Generic;
using CART_457.c_sharp.Renderer;
using CART_457.EntitySystem;
using OpenTK.Graphics.OpenGL4;
using CART_457.Helpers;
using CART_457.Renderer;
using CART_457.Scripts;
using CART_457.Scripts.Setups;

namespace CART_457.MaterialRelated
{
    
    /// <summary>
    /// Responsible for setting up a shader, it's textures, it's vertex attributes and setting its uniforms
    /// </summary>
    public class Material
    {
        public static Dictionary<string, int> UniformBufferLocations { get; private set; }
        
        public FBO RenderTarget { get; private set; }
        public ShaderProgram Shader { get; private set; }
        
        public Dictionary<string, int> UniformLocations { get; private set; }
        public Dictionary<string, int> VertexAttribLocations { get; private set; }
        public VAOAndBuffers VAO { get; private set; }
        
        private List<Texture> _textures = new List<Texture>();

        public Action<Material> PerMaterialUniformSender;
        public Action<Entity, Material> PerEntityUniformSender;

        public bool IsShadowMapMaterial;
        private const bool DEBUG = false;

        static Material()
        {
            // UniformBufferLocations = new Dictionary<string, int>();
            //
            // int transformHandle = GL.GenBuffer();
            // GL.BindBuffer(BufferTarget.UniformBuffer, transformHandle);
            // GL.BufferData(BufferTarget.UniformBuffer, 336, IntPtr.Zero, BufferUsageHint.StreamDraw);
            // GL.BindBufferBase(BufferRangeTarget.UniformBuffer, 1, transformHandle); //bind point?
            // UniformBufferLocations.Add(TRANSFORM_BUFFER, transformHandle);
        }

        private Material()
        {
            // int transformIndex = GL.GetUniformBlockIndex(program.Handle, TRANSFORM_BUFFER);
            // GL.UniformBlockBinding(program.Handle, transformIndex, 1); //set binding point
        }
        
        
        public static Material GenericEntityBased(FBO fbo, ShaderProgram shaderProgram, Mesh mesh, Action<Material> perMaterialUniformSender, Action<Entity, Material> perEntityUniformSender)
        {
            var mat = new Material();
            mat.Shader = shaderProgram;
            mat.PerMaterialUniformSender += perMaterialUniformSender;
            mat.PerEntityUniformSender   += perEntityUniformSender;
            mat.RenderTarget = fbo;
            mat.GetUniformAndAttribLocations();
            mat.VAO = new VAOAndBuffers(mat, mesh);
            return mat;
        }
        
        public static Material EntityNormalUseShadow(FBO fbo, Mesh mesh, FBO shadowMapFBO, Action<Material> perMaterialUniformSender)
        {
            var mat = GenericEntityBased(fbo, SetupShaders.Normal, mesh, perMaterialUniformSender, (entity, material) =>
            {
                UniformSender.SendTransformMatrices(entity, material, shadowMapFBO.Camera, "Light");
                UniformSender.SendTransformMatrices(entity, material, material.RenderTarget.Camera);
                UniformSender.SendLights(material);
                UniformSender.SendGlobals(material);
            });
            
            return mat;
        }
        
        public static Material EntitySolid(FBO fbo, Mesh mesh, Action<Material> perMaterialUniformSender)
        {
            var mat = GenericEntityBased(fbo, SetupShaders.Solid, mesh, perMaterialUniformSender, (entity, material) =>
            {
                UniformSender.SendTransformMatrices(entity, material, material.RenderTarget.Camera);
                UniformSender.SendGlobals(material);
            });
            
            return mat;
        }
        
        public static Material EntityCastShadow(FBO fbo, Mesh mesh, Action<Material> perMaterialUniformSender)
        {
            var mat = GenericEntityBased(fbo, SetupShaders.Shadow, mesh, perMaterialUniformSender,
                (entity, material) =>
                {
                    UniformSender.SendTransformMatrices(entity, material, material.RenderTarget.Camera, "Light");
                });
            return mat;
        }

        public static Material PostProcessing(ShaderProgram shaderProgram)
        {
            var mat = new Material();
            mat.Shader = shaderProgram;
            mat.RenderTarget = SetupFBOs.PostProcessing;
            mat.GetUniformAndAttribLocations();
            mat.VAO = new VAOAndBuffers(mat, SetupMeshes.ViewSpaceQuad);
            {//get last fbo in fbosetup
                
            }

        mat.PerMaterialUniformSender = _ => SetupFBOs.Room1.UseTexturesAndGenerateMipMaps();
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
            if (texture == null)
                throw new  ArgumentException("Cannot setup sampler with a null texture!");
            Shader.Use();
            _textures.Add(texture);
            Shader.SetUniformInt(samplerName, texture.TextureUnit.ToInt());
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