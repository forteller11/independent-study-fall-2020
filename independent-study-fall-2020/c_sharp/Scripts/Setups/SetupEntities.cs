using System;
using System.Collections.Generic;
using CART_457.Blueprints;
using CART_457.EntitySystem;
using CART_457.Helpers;
using CART_457.PhysicsRelated;
using CART_457.Renderer;
using CART_457.Scripts.Blueprints;
using CART_457.Scripts.EntityPrefabs;
using CART_457Scripts.Blueprints;
using Indpendent_Study_Fall_2020.c_sharp.Scripts.Blueprints;
using OpenTK.Mathematics;

namespace CART_457.Scripts.Setups
{
    //TODO make entity constructor automatically add entities to game world... Instant, Delayed creation CreateAndAdd vs Create
    public class SetupEntities
    {
        public static void CreateGameObjects() 
        {

            #region room 1


            var room = FrustrumNormal.FromPositionRotationScale(true, new Vector3(0,0,0),Quaternion.Identity, new Vector3(1f), SetupMaterials.RoomCleanR1InCamera, SetupMaterials.ShadowMapPlane);
            room.AddColliders(SetupMeshes.TableClean01Colliders);
            // var room
            // var dirtPlane01 = Empty.FromPosition(new Vector3(0, 0, 0), SetupMaterials.DirtPlane, SetupMaterials.ShadowMapPlane);
            // dirtPlane01.LocalScale = new Vector3(4);
            var dirtPlane02 = Empty.FromPosition(new Vector3(0, -12, 0), SetupMaterials.DirtPlaneR1Frustrum, SetupMaterials.ShadowMapPlane);
            dirtPlane02.LocalScale = new Vector3(7);

            #region camera related
            var cameraVisualizer = new CameraVisualizer(new Vector3(1.27f,2.96f,7.5f), .2f);

            var camController = new CameraControllerSingleton(room.ColliderGroup, Globals.PlayerCameraRoom1);

            
            var raycastTest = new WebcamRotater(cameraVisualizer.WebCamVisualizer);
            raycastTest.Parent = camController;

            new CameraInterperlator();
            
            #endregion
            #region visualizers and debuggers
            new FBOVisualizationInput();

            new RaycastDebugger();
                
            #endregion
            for (int i = 0; i < Globals.PointLights.Count; i++)
                new PointLightVisualizer(i,SetupMaterials.SolidSphereR1);
            #endregion
            
            #region room2
            var head = new Wobble(.03f, 100f, new Vector3(0, 1.9f, 4), SetupMaterials.WeirdHeadR2);
            head.LocalRotation = Quaternion.FromEulerAngles(0, MathF.PI/2,0);
            #endregion
            
            #region screen combiner

            #endregion
            
  
        }

        public static void CreateGlobals()
        {
            Globals.DirectionLights.Add(new DirectionLight(new Vector3(0,1,0), new Vector3(.8f)));wwd
//            Globals.DirectionLights.Add(new DirectionLight(new Vector3(1,0,0), new Vector3(1,0,0)));
//            Globals.DirectionLights.Add(new DirectionLight(new Vector3(0,0,1), new Vector3(0,1,1)));
            
            // Globals.PointLights.Add(new PointLight(new Vector3(-2,-3f,0), new Vector3(.3f,.3f,1)));
            // Globals.PointLights.Add(new PointLight(new Vector3(2,-3f,0), new Vector3(1,.3f,.3f)));
            Globals.PointLights.Add(new PointLight(new Vector3(9.3f,2.96f,5.8f), new Vector3(1,1f,.8f)));
        }
    }
}