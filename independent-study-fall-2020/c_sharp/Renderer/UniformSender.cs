using System;
using Indpendent_Study_Fall_2020.EntitySystem;
using Indpendent_Study_Fall_2020.MaterialRelated;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace Indpendent_Study_Fall_2020.c_sharp.Renderer
{
    public static class UniformSender
    {
        
        /// <summary>
        /// Sends "ModelToWorld" and "WorldToView" uniform matrices to shader
        /// </summary>
        public static void SendTransformMatrices(GameObject gameObject)
        {
            var modelRotation = Matrix4.Transpose(Matrix4.CreateFromQuaternion(gameObject.Rotation));
            var worldTranslation = Matrix4.CreateTranslation(gameObject.Position - Globals.CameraPosition);
            var worldRotation = Matrix4.Transpose(Matrix4.CreateFromQuaternion(Globals.CameraRotation));
            var modelScale = Matrix4.CreateScale(gameObject.Scale); //transponse?
            
            //apparently matrix mult combines matrices as if matrix left matrix transformed THEN the right... opposite to how it works in math
            var modelToWorld = modelRotation * modelScale * worldTranslation * worldRotation;
            SetMatrix4(gameObject.Material, "ModelToWorld", modelToWorld, false);
            
            SetMatrix4(gameObject.Material, "ModelRotation", modelRotation, false);
            
            var worldToView = Globals.CameraPerspective;
            SetMatrix4(gameObject.Material, "WorldToView", worldToView, false);
        }

        /// <summary>
        /// Sends "in_pointLights" and "in_directionLights" vector4 arrays,
        /// where the first 3 components are the position/direction
        /// and the last component is the intensity
        /// </summary>
        //todo optimise uniforms which should only be changed once per frame, not per object... also don't flatten every time you call SendLights...
        //todo send via uniform buffer object? https://learnopengl.com/Advanced-OpenGL/Advanced-GLSL
        public static void SendLights(GameObject gameObject) 
        {
            int lightStride = 3;
            float[] pointPosFlat =   new float[Globals.PointLights.Count * lightStride];
            float[] pointColorFlat = new float[Globals.PointLights.Count * lightStride];
            
            float[] dirDirFlat =   new float[Globals.DirectionLights.Count * lightStride];
            float[] dirColorFlat = new float[Globals.DirectionLights.Count * lightStride];
            
            for (int i = 0; i < Globals.PointLights.Count; i++)
            {
                int baseIndex = i * lightStride;
                pointPosFlat[baseIndex + 0] = Globals.PointLights[i].Position.X;
                pointPosFlat[baseIndex + 1] = Globals.PointLights[i].Position.Y;
                pointPosFlat[baseIndex + 2] = Globals.PointLights[i].Position.Z;
                
                pointColorFlat[baseIndex + 0] = Globals.PointLights[i].Color.X;
                pointColorFlat[baseIndex + 1] = Globals.PointLights[i].Color.Y;
                pointColorFlat[baseIndex + 2] = Globals.PointLights[i].Color.Z;
            }
            
            for (int i = 0; i < Globals.DirectionLights.Count; i++)
            {
                int baseIndex = i * lightStride;
                dirDirFlat[baseIndex + 0] = Globals.DirectionLights[i].Direction.X;
                dirDirFlat[baseIndex + 1] = Globals.DirectionLights[i].Direction.Y;
                dirDirFlat[baseIndex + 2] = Globals.DirectionLights[i].Direction.Z;
                
                dirColorFlat[baseIndex + 0] = Globals.DirectionLights[i].Color.X;
                dirColorFlat[baseIndex + 1] = Globals.DirectionLights[i].Color.Y;
                dirColorFlat[baseIndex + 2] = Globals.DirectionLights[i].Color.Z;
            }
            
            SetInt(gameObject.Material,"DirectionLightsLength", Globals.DirectionLights.Count);
//            SetInt(gameObject.Material,"PointLightsLength", Globals.PointLights.Count);
            
//            SetVector3Array(gameObject.Material,"PointLightsPositions", pointPosFlat, false, false);
//            SetVector3Array(gameObject.Material,"PointLightsColors", pointColorFlat, false, false);
//            SetVector3Array(gameObject.Material,"DirectionLightsDirections", dirDirFlat, false, false);
            SetVector3Array(gameObject.Material,"DirectionLightsColors", dirColorFlat, false, false);
 
            
          
            

//            gameObject.Material.SetVector4("in_directionLight",new Vector4(Globals.DirectionLights[0].Direction, Globals.DirectionLights[0].Intensity ));
        }

        public static void SendTime(GameObject gameObject)
        {
            SetVector4(gameObject.Material, "Time", new Vector4(Globals.AbsTimeF, MathF.Cos(Globals.AbsTimeF), 0, 0));
//            gameObject.Material.set 
//            Globals.AbsoluteTime
        }
        
        
        public static void SetMatrix4(Material mat, string name, OpenTK.Matrix4 matrix4, bool useProgram=true) //set useProgram to false for batch operations for performance gains
        {
            if (useProgram) mat.Shader.Use();
            if (mat.UniformLocations.TryGetValue(name, out int location))
                GL.UniformMatrix4(location, true, ref matrix4);
            else
                Debug.LogWarning($"Uniform \"{name}\" not found in shader program! Are you using it in your output? (optimized out?)");
        }

//        public void SetVector4Array(string name, Vector4 [] vectors, bool useProgram = true, bool includeLength=true)
//        {
//            if (useProgram) Shader.Use();
//            
//            for (int i = 0; i < vectors.Length; i++)
//            {
//                if (UniformLocations.TryGetValue($"{name}[{i}]", out int location)) 
//                    GL.Uniform4(location, vectors.Length, vectors[i]);
//                else
//                    Debug.LogWarning($"Uniform \"{name}\" not found in shader program! Are you using it in your output? (optimized out?)");
//            }
//            SetInt(name + "Length", vectors.Length, useProgram);
//        }
        
        public static void SetVector3Array(Material mat, string name, float [] vectors, bool useProgram = true, bool includeLength=false)
        {
            if (useProgram) mat.Shader.Use();
            
            if (mat.UniformLocations.TryGetValue(name, out int location))
                    GL.Uniform3(location, vectors.Length, vectors);
            else
                Debug.LogWarning($"Uniform \"{name}\" not found in shader program! Are you using it in your output? (optimized out?)");
            
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
            else
                Debug.LogWarning($"Uniform \"{indexedName}\" not found in shader program! Are you using it in your output? (optimized out?)");
        }
        
        //todo send arrays again and FLATTEN
        
        public static void SetVector4Element(Material mat, string name, Vector4 vector, bool useProgram, int index)
        {
            if (useProgram) mat.Shader.Use();
            
            string indexedName = $"{name}[{index}]";
            if (mat.UniformLocations.TryGetValue(indexedName, out int location))
                GL.Uniform4(location, ref vector);
            else
                Debug.LogWarning($"Uniform \"{indexedName}\" not found in shader program! Are you using it in your output? (optimized out?)");
        }
        
        
        public static void SetVector3(Material mat, string name, Vector3 vector3, bool useProgram=true) //set useProgram to false for batch operations for performance gains
        {
            if (useProgram) mat.Shader.Use();
            if (mat.UniformLocations.TryGetValue(name, out int location))
                GL.Uniform3(location, ref vector3);
            else
                Debug.LogWarning($"Uniform \"{name}\" not found in shader program! Are you using it in your output? (optimized out?)");
        }
        
        public static void SetInt(Material mat, string name, int integer, bool useProgram=true) //set useProgram to false for batch operations for performance gains
        {
            if (useProgram) mat.Shader.Use();
            if (mat.UniformLocations.TryGetValue(name, out int location))
                GL.Uniform1(location, integer);
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