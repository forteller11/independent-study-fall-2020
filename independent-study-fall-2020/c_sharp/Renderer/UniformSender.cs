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
            var modelRotation = Matrix4.CreateFromQuaternion(gameObject.Rotation);
            var worldTranslation = Matrix4.CreateTranslation(gameObject.Position - Globals.CameraPosition);
            var worldRotation = Matrix4.CreateFromQuaternion(Globals.CameraRotation);

            //apparently matrix mult combines matrices as if matrix left matrix transformed THEN the right... opposite to how it works in math
            var modelToWorld = Matrix4.Transpose(modelRotation) * Matrix4.CreateScale(gameObject.Scale) * worldTranslation * Matrix4.Transpose(worldRotation);
            gameObject.Material.SetMatrix4("ModelToWorld", modelToWorld, false);
            
            var worldToView = Globals.CameraPerspective;
            gameObject.Material.SetMatrix4("WorldToView", worldToView, false);
        }

        /// <summary>
        /// Sends "in_pointLights" and "in_directionLights" vector4 arrays,
        /// where the first 3 components are the position/direction
        /// and the last component is the intensity
        /// </summary>
        public static void SendLights(GameObject gameObject) //todo optimise uniforms which should only be changed once per frame, not per object... also don't flatten every time you call SendLights...
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
            
            gameObject.Material.SetVector4Array("in_pointLights", pointLightsFlattened, false);
            gameObject.Material.SetVector4Array("in_directionLights", directionLightsFlattened, false);
        }
    }
}