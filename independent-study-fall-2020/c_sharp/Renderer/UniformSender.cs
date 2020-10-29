using System;
using System.Text;
using CART_457.EntitySystem;
using CART_457.MaterialRelated;
using CART_457.Renderer;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace CART_457.c_sharp.Renderer
{
    public static class UniformSender
    {
        private const string DIR_LIGHT = "DirectionLights";
        private const string POINT_LIGHT = "PointLights";
        
        /// <summary>
        /// Sends "ModelToWorld" and "WorldToView" uniform matrices to shader
        /// </summary>
        public static void SendTransformMatrices(Entity entity, Material material, Camera camera, string suffix="")
        {
            var modelToWorldRotation = Matrix4.CreateFromQuaternion(entity.WorldRotation);
            var worldToViewTranslation = Matrix4.CreateTranslation(-camera.Position);
            var modelToWorldTranslation = Matrix4.CreateTranslation(entity.WorldPosition);
            var worldToViewRotation = Matrix4.CreateFromQuaternion(Quaternion.Invert(camera.Rotation));
            var modelToWorldScale = Matrix4.CreateScale(entity.WorldScale); //transponse?
            
            //apparently matrix mult combines matrices as if matrix left matrix transformed THEN the right... opposite to how it works in math
            //todo consolidate matrices and refactor.... then go bk into shader
            var modelToWorld = modelToWorldRotation * modelToWorldScale  * modelToWorldTranslation ;
            var worldToView = worldToViewTranslation * worldToViewRotation * camera.Projection;
            var modelToViewNoProjection = modelToWorld * worldToViewTranslation * worldToViewRotation;
            var modelToView = modelToWorld * worldToView;
            
            SetMatrix4(material, Material.MODEL_TO_VIEW_UNIFORM + suffix, modelToView, false);
            SetMatrix4(material, Material.WORLD_TO_VIEW_UNIFORM + suffix, worldToView, false);
            SetMatrix4(material, Material.MODEL_ROTATION_UNIFORM + suffix, modelToWorldRotation, false);
            SetMatrix4(material, Material.MODEL_TO_WORLD_UNIFORM + suffix, modelToWorld, false);
            SetMatrix4(material, Material.MODEL_TO_WORLD_NO_PROJECTION_UNIFORM + suffix, modelToViewNoProjection, false);
            SetVector3(material, Material.CAM_POSITION_UNIFORM + suffix, camera.Position, false);
        }

        /// <summary>
        /// Sends "in_pointLights" and "in_directionLights" vector4 arrays,
        /// where the first 3 components are the position/direction
        /// and the last component is the intensity
        /// </summary>
        //todo optimise uniforms which should only be changed once per frame, not per object... also don't flatten every time you call SendLights...
        //todo send via uniform buffer object? https://learnopengl.com/Advanced-OpenGL/Advanced-GLSL
        public static void SendLights(Material material)
        {
            SetInt(material, $"{DIR_LIGHT}Length", Globals.DirectionLights.Count);
            for (int i = 0; i < Globals.DirectionLights.Count; i++)
            {
                SetVector3(material, $"{DIR_LIGHT}[{i}].Color",     Globals.DirectionLights[i].Color, false);
                SetVector3(material, $"{DIR_LIGHT}[{i}].Direction", Globals.DirectionLights[i].Direction, false);
            }
            
            SetInt(material,$"{POINT_LIGHT}Length", Globals.PointLights.Count);
            for (int i = 0; i < Globals.PointLights.Count; i++)
            {
                SetVector3(material, $"{POINT_LIGHT}[{i}].Color",    Globals.PointLights[i].Color, false);
                SetVector3(material, $"{POINT_LIGHT}[{i}].Position", Globals.PointLights[i].Position, false);
            }
        }

        public static void SendShadowMapInfo()
        {
            
        }

        //todo set element vec
        public static void SendTime(Material material)
        {
            // SetVector4(material, "Time", new Vector4(Globals.AbsTimeF, MathF.Cos(Globals.AbsTimeF), 0, 0));
            SetFloat(material, "Time", Globals.AbsTimeF);
//            gameObject.Material.set 
//            Globals.AbsoluteTime
        }
        
        
        public static void SetMatrix4(Material mat, string name, OpenTK.Matrix4 matrix4, bool useProgram=true) //set useProgram to false for batch operations for performance gains
        {
            if (useProgram) mat.Shader.Use();
            if (mat.UniformLocations.TryGetValue(name, out int location))
                GL.UniformMatrix4(location, true, ref matrix4);
//            else
//                Debug.LogWarning($"Uniform \"{name}\" not found in shader program! Are you using it in your output? (optimized out?)");
        }
        
        public static void SetMatrix3(Material mat, string name, OpenTK.Matrix3 matrix3, bool useProgram=true) //set useProgram to false for batch operations for performance gains
        {
            if (useProgram) mat.Shader.Use();
            if (mat.UniformLocations.TryGetValue(name, out int location))
                GL.UniformMatrix3(location, true, ref matrix3);
//            else
//                Debug.LogWarning($"Uniform \"{name}\" not found in shader program! Are you using it in your output? (optimized out?)");
        }


        
        public static void SetVector3Array(Material mat, string name, float [] vectors, bool useProgram = true, bool includeLength=false)
        {
            if (useProgram) mat.Shader.Use();
            
            if (mat.UniformLocations.TryGetValue(name, out int location))
                    GL.Uniform3(location, vectors.Length, vectors);
//            else
//                Debug.LogWarning($"Uniform \"{name}\" not found in shader program! Are you using it in your output? (optimized out?)");
            
            if (includeLength)
                SetInt(mat, name + "Length", vectors.Length, useProgram);
        }

        public static void SetVector3Element(Material mat, string name, Vector3 vector, bool useProgram, int index)
        {
            if (useProgram) mat.Shader.Use();
            
            string indexedName = $"{name}[{index}]";
            var location = GL.GetUniformLocation(mat.Shader.Handle, name);
            if (location != -1)
                GL.Uniform3(location, ref vector);
//            else
//                Debug.LogWarning($"Uniform \"{indexedName}\" not found in shader program! Are you using it in your output? (optimized out?)");
        }
        
        //todo send arrays again and FLATTEN
        
        public static void SetVector4Element(Material mat, string name, Vector4 vector, bool useProgram, int index)
        {
            if (useProgram) mat.Shader.Use();
            
            string indexedName = $"{name}[{index}]";
            if (mat.UniformLocations.TryGetValue(indexedName, out int location))
                GL.Uniform4(location, ref vector);
//            else
//                Debug.LogWarning($"Uniform \"{indexedName}\" not found in shader program! Are you using it in your output? (optimized out?)");
        }
        
        
        public static void SetVector3(Material mat, string name, Vector3 vector3, bool useProgram=true) //set useProgram to false for batch operations for performance gains
        {
            if (useProgram) mat.Shader.Use();
            if (mat.UniformLocations.TryGetValue(name, out int location))
                GL.Uniform3(location, ref vector3);
//            else
//                Debug.LogWarning($"Uniform \"{name}\" not found in shader program! Are you using it in your output? (optimized out?)");
        }
        
        public static void SetInt(Material mat, string name, int integer, bool useProgram=true) //set useProgram to false for batch operations for performance gains
        {
            if (useProgram) mat.Shader.Use();
            if (mat.UniformLocations.TryGetValue(name, out int location))
                GL.Uniform1(location, integer);
            // else
            //     Debug.LogWarning($"Uniform \"{name}\" not found in shader program! Are you using it in your output? (optimized out?)");
        }
        
        public static void SetFloat(Material mat, string name, float value, bool useProgram=true) //set useProgram to false for batch operations for performance gains
        {
            if (useProgram) mat.Shader.Use();
            if (mat.UniformLocations.TryGetValue(name, out int location))
                GL.Uniform1(location, value);
            // else
            //     Debug.LogWarning($"Uniform \"{name}\" not found in shader program! Are you using it in your output? (optimized out?)");
        }
        
        public static void SetVector4(Material mat, string name, OpenTK.Vector4 vector4, bool useProgram=true) //set useProgram to false for batch operations for performance gains
        {
            if (useProgram) mat.Shader.Use();
            if (mat.UniformLocations.TryGetValue(name, out int location))
                GL.Uniform4(location, ref vector4);
            // else
            //     Debug.LogWarning($"Uniform \"{name}\" not found in shader program! Are you using it in your output? (optimized out?)");
        }
    }
}