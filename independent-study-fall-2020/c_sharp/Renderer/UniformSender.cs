using System;
using Indpendent_Study_Fall_2020.EntitySystem;
using Indpendent_Study_Fall_2020.MaterialRelated;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace Indpendent_Study_Fall_2020.c_sharp.Renderer
{
    public static class UniformSender
    {
        private const string DIR_LIGHT = "DirectionLights";
        private const string POINT_LIGHT = "PointLights";
        
        /// <summary>
        /// Sends "ModelToWorld" and "WorldToView" uniform matrices to shader
        /// </summary>
        public static void SendTransformMatrices(Entity entity)
        {
            var modelToWorldRotation = Matrix4.Transpose(Matrix4.CreateFromQuaternion(entity.Rotation));
            var worldToViewTranslation = Matrix4.CreateTranslation(-Globals.CameraPosition);
            var modelToWorldTranslation = Matrix4.CreateTranslation(entity.Position);
            var worldToViewRotation = Matrix4.Transpose(Matrix4.CreateFromQuaternion(Globals.CameraRotation));
            var modelToWorldScale = Matrix4.CreateScale(entity.Scale); //transponse?
            
            //apparently matrix mult combines matrices as if matrix left matrix transformed THEN the right... opposite to how it works in math
            //todo consolidate matrices and refactor.... then go bk into shader
            var modelToWorld = modelToWorldRotation * modelToWorldScale  * modelToWorldTranslation ;
            var worldToView = worldToViewTranslation * worldToViewRotation * Globals.CameraPerspective;
            var modelToView = modelToWorld * worldToView;
            
            SetMatrix4(entity.Material, "ModelToView", modelToView, false);
            SetMatrix4(entity.Material, "WorldToView", worldToView, false);
            SetMatrix4(entity.Material, "ModelRotation", modelToWorldRotation, false);
            SetMatrix4(entity.Material, "ModelToWorld", modelToWorld, false);
            
            SetVector3(entity.Material, "CamPosition", Globals.CameraPosition, false); //todo batch with like materials
        }

        /// <summary>
        /// Sends "in_pointLights" and "in_directionLights" vector4 arrays,
        /// where the first 3 components are the position/direction
        /// and the last component is the intensity
        /// </summary>
        //todo optimise uniforms which should only be changed once per frame, not per object... also don't flatten every time you call SendLights...
        //todo send via uniform buffer object? https://learnopengl.com/Advanced-OpenGL/Advanced-GLSL
        public static void SendLights(Entity entity)
        {
            SetInt(entity.Material, $"{DIR_LIGHT}Length", Globals.DirectionLights.Count);
            for (int i = 0; i < Globals.DirectionLights.Count; i++)
            {
                SetVector3(entity.Material, $"{DIR_LIGHT}[{i}].Color",     Globals.DirectionLights[i].Color, false);
                SetVector3(entity.Material, $"{DIR_LIGHT}[{i}].Direction", Globals.DirectionLights[i].Direction, false);
            }
            
            SetInt(entity.Material,$"{POINT_LIGHT}Length", Globals.PointLights.Count);
            for (int i = 0; i < Globals.PointLights.Count; i++)
            {
                SetVector3(entity.Material, $"{POINT_LIGHT}[{i}].Color",    Globals.PointLights[i].Color, false);
                SetVector3(entity.Material, $"{POINT_LIGHT}[{i}].Position", Globals.PointLights[i].Position, false);
            }
        }

        //todo set element vec
        public static void SendTime(Entity entity)
        {
            SetVector4(entity.Material, "Time", new Vector4(Globals.AbsTimeF, MathF.Cos(Globals.AbsTimeF), 0, 0));
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
            else
                Debug.LogWarning($"Uniform \"{name}\" not found in shader program! Are you using it in your output? (optimized out?)");
        }
        
        public static void SetFloat(Material mat, string name, float value, bool useProgram=true) //set useProgram to false for batch operations for performance gains
        {
            if (useProgram) mat.Shader.Use();
            if (mat.UniformLocations.TryGetValue(name, out int location))
                GL.Uniform1(location, value);
            else
                Debug.LogWarning($"Uniform \"{name}\" not found in shader program! Are you using it in your output? (optimized out?)");
        }
        
        public static void SetVector4(Material mat, string name, OpenTK.Vector4 vector4, bool useProgram=true) //set useProgram to false for batch operations for performance gains
        {
            if (useProgram) mat.Shader.Use();
            if (mat.UniformLocations.TryGetValue(name, out int location))
                GL.Uniform4(location, ref vector4);
            else
                Debug.LogWarning($"Uniform \"{name}\" not found in shader program! Are you using it in your output? (optimized out?)");
        }
    }
}