using Indpendent_Study_Fall_2020.EntitySystem;
using Indpendent_Study_Fall_2020.MaterialRelated;
using OpenTK;

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
            gameObject.Material.SetMatrix4("ModelToWorld", modelToWorld, false);
            
            gameObject.Material.SetMatrix4("ModelRotation", modelRotation, false);
            
            var worldToView = Globals.CameraPerspective;
            gameObject.Material.SetMatrix4("WorldToView", worldToView, false);
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
            int lightStride = 4;
            float[] pointLightsFlattened = new float[Globals.PointLights.Count * lightStride];
            float[] directionLightsFlattened = new float[Globals.DirectionLights.Count * lightStride];

            for (int i = 0; i < Globals.PointLights.Count; i++)
            {
                int baseIndex = i * lightStride;
                pointLightsFlattened[baseIndex + 0] = Globals.PointLights[i].Position.X;
                pointLightsFlattened[baseIndex + 1] = Globals.PointLights[i].Position.Y;
                pointLightsFlattened[baseIndex + 2] = Globals.PointLights[i].Position.Z;
                pointLightsFlattened[baseIndex + 3] = Globals.PointLights[i].Intensity;
            }
            
            for (int i = 0; i < Globals.DirectionLights.Count; i++)
            {
                int baseIndex = i * lightStride;
                directionLightsFlattened[baseIndex + 0] = Globals.DirectionLights[i].Direction.X;
                directionLightsFlattened[baseIndex + 1] = Globals.DirectionLights[i].Direction.Y;
                directionLightsFlattened[baseIndex + 2] = Globals.DirectionLights[i].Direction.Z;
                directionLightsFlattened[baseIndex + 3] = Globals.DirectionLights[i].Intensity;
            }
            Vector4[] pointLightVecs = new Vector4[Globals.PointLights.Count];
            Vector4[] directionLightVecs = new Vector4[Globals.DirectionLights.Count];
            
            for (int i = 0; i < Globals.PointLights.Count; i++)
                pointLightVecs[i] = new Vector4(Globals.PointLights[i].Position, Globals.PointLights[i].Intensity);
            
            for (int i = 0; i < Globals.DirectionLights.Count; i++)
                directionLightVecs[i] = new Vector4(Globals.DirectionLights[i].Direction, Globals.DirectionLights[i].Intensity);
            
            gameObject.Material.SetVector4Array("PointLights", pointLightVecs, false);
            gameObject.Material.SetVector4Array("DirectionLights", directionLightVecs, false);
            
//            gameObject.Material.SetVector4("in_directionLight",new Vector4(Globals.DirectionLights[0].Direction, Globals.DirectionLights[0].Intensity ));
        }
    }
}