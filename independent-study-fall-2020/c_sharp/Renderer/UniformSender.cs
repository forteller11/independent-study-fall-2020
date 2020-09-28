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
            
            gameObject.Material.SetInt("DirectionLightsLength", Globals.DirectionLights.Count);
//            gameObject.Material.SetInt("PointLightsLength", Globals.PointLights.Count);
            
//            gameObject.Material.SetVector3Array("PointLightsPositions", pointPosFlat, false, false);
//            gameObject.Material.SetVector3Array("PointLightsColors", pointColorFlat, false, false);
//            gameObject.Material.SetVector3Array("DirectionLightsDirections", dirDirFlat, false, false);
            gameObject.Material.SetVector3Array("DirectionLightsColors", dirColorFlat, false, false);
 
            
          
            

//            gameObject.Material.SetVector4("in_directionLight",new Vector4(Globals.DirectionLights[0].Direction, Globals.DirectionLights[0].Intensity ));
        }

        public static void SendTime(GameObject gameObject)
        {
//            gameObject.Material.set
//            Globals.AbsoluteTime
        }
    }
}