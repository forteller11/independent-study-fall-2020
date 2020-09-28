using Indpendent_Study_Fall_2020.EntitySystem;
using Indpendent_Study_Fall_2020.MaterialRelated;
using OpenTK;
using OpenTK.Graphics.ES10;

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

            gameObject.Material.SetInt("DirectionLightsLength", Globals.DirectionLights.Count);
            for (int i = 0; i < Globals.DirectionLights.Count; i++)
            {
                gameObject.Material.SetVector3Element("DirectionLightsDirections", Globals.DirectionLights[i].Direction, false, i);
                gameObject.Material.SetVector3Element("DirectionLightsColors", Globals.DirectionLights[i].Color, false, i);
            }
            
            gameObject.Material.SetInt("PointLightsLength", Globals.PointLights.Count);
            for (int i = 0; i < Globals.PointLights.Count; i++)
            {
                gameObject.Material.SetVector3Element("PointLightsPositions", Globals.PointLights[i].Position, false, i);
                gameObject.Material.SetVector3Element("PointLightsColors", Globals.PointLights[i].Color, false, i);
            }

            

//            gameObject.Material.SetVector4("in_directionLight",new Vector4(Globals.DirectionLights[0].Direction, Globals.DirectionLights[0].Intensity ));
        }

        public static void SendTime(GameObject gameObject)
        {
//            gameObject.Material.set
//            Globals.AbsoluteTime
        }
    }
}