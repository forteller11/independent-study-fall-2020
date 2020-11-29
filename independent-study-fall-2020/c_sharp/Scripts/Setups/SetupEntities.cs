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
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace CART_457.Scripts.Setups
{
    //TODO make entity constructor automatically add entities to game world... Instant, Delayed creation CreateAndAdd vs Create
    public class SetupEntities
    {
        public static void CreateGameObjects() 
        {

            #region room 1

             var dirty= FrustrumNormal.FromPositionRotationScale(true, new Vector3(0,0,0),Quaternion.Identity, new Vector3(1f), SetupMaterials.RoomDirtyR1OutCamera, SetupMaterials.ShadowMapPlane);
             var room = FrustrumNormal.FromPositionRotationScale(false, new Vector3(0,0,0),Quaternion.Identity, new Vector3(1f), SetupMaterials.RoomCleanR1InCamera, SetupMaterials.ShadowMapPlane);
             FrustrumNormal.FromPositionRotationScale(false, new Vector3(0,0,0),Quaternion.Identity, new Vector3(1f), SetupMaterials.RoomCleanWebcam, SetupMaterials.ShadowMapPlane);
             FrustrumNormal.FromPositionRotationScale(false, new Vector3(0,0,0),Quaternion.Identity, new Vector3(1f), SetupMaterials.RoomCleanCeilingAndLampsWebcam, SetupMaterials.ShadowMapPlane);
            FrustrumNormal.FromPositionRotationScale(false, new Vector3(0,0,0),Quaternion.Identity, new Vector3(1f), SetupMaterials.RoomCleanCeilingLampsR1InCamera, SetupMaterials.ShadowMapPlane);
            new Empty(SetupMaterials.Basement);
            // s.LocalRotation = Quaternion.FromEulerAngles(1,1,1);
            var floorColliders = new ColliderGroup();
            floorColliders.AddCollider(new MeshCollider(null, false, SetupMeshes.RoomClean01Colliders));

            var basementCollider = new MeshCollider(null, false, SetupMeshes.BasementFloorColliders);
             var basementColliderTrigger = new FloorColliderTrigger(Globals.WebCamRoom1, basementCollider, floorColliders, false,
                 new Vector3(-7.1744f, -1.49f, 3.5f), new Vector3(-7.1744f, -1.49f, 0.6f),
                 new Vector3(-7.1744f, 4f, 3.5f), new Vector3(-7.1744f, 4f, 0.6f)
                 );
  
            var dirtPlane02 = Empty.FromPosition(new Vector3(0, -12, 0), SetupMaterials.DirtPlaneR1Frustrum, SetupMaterials.ShadowMapPlane);
            dirtPlane02.LocalScale = new Vector3(7);
            
            #region camera related

            var webcamRotater = new RotateOnHit(new Vector3(1.27f,3f,7.5f), Quaternion.FromEulerAngles(0,0,0));
            webcamRotater.AddCollider(new SphereCollider(webcamRotater, true, 1));
            webcamRotater.LocalScale *= .35f;
            
            var webcamVisualizer = new Empty(SetupMaterials.EyeBall, SetupMaterials.ShadowMapSphere);
            webcamVisualizer.Parent = webcamRotater;
            
            var webcamController = new CameraController(Globals.WebCamRoom1, Vector3.Zero, Quaternion.Identity);
            webcamController.Parent = webcamRotater;
            
            var playerCameraController = new PlayerController(floorColliders, Globals.PlayerCameraRoom1);
            CreateEyeVisualizer(new Vector3(0.33f, 0, 0), playerCameraController);
            CreateEyeVisualizer(new Vector3(-0.33f, 0, 0), playerCameraController);
            
            var doorOpen = new DoorOpen(playerCameraController);
            doorOpen.LocalPosition = new Vector3(-17.662f, -9.1f, 52.416f);
            
            var doorOpenHandle = new DoorHandle(doorOpen);
            doorOpenHandle.AddCollider(new MeshCollider(doorOpenHandle, true, SetupMeshes.DoorOpenHandle));
            doorOpenHandle.Parent = doorOpen;
            doorOpenHandle.LocalPosition = new Vector3(-3.1671f, -0.29577f, -0.26485f);
            
            var uberDriverCameraController = new CameraController(Globals.UberDriver, new Vector3(-23.13793f,-6.2f , 93.552f), Quaternion.Identity);
      
            
            //var webcamRotater = new WebcamRotater(webcamController);
            //webcamRotater.Parent = playerCameraController;

            new CameraInterperlator();
            #endregion
            
            #region visualizers and debuggers
            new FBOVisualizationInput();

            #endregion
            for (int i = 0; i < Globals.PointLights.Count; i++)
                new PointLightVisualizer(i,SetupMaterials.SolidSphereR1);
            #endregion
            
            #region room2
            var head = Wobble.Position(new Vector3(.03f), new Vector3(100f), new Vector3(0, 1.9f, 4), SetupMaterials.WeirdHeadR2);
            head.LocalRotation = Quaternion.FromEulerAngles(0, MathF.PI/2,0);
            #endregion

            var monitor = Empty.FromPositionRotationScale(new Vector3(1.3f, 2.177f, 7.4f), Quaternion.FromEulerAngles(0, MathF.PI, 0), new Vector3(0.855f, .563f, 1f), SetupMaterials.ScreenR1);

        }

        
        public static void SetupGlobals()
        {

            Globals.PointLights.Add(new PointLight(new Vector3(0.427837f,6.2f,-1.98f), new Vector3(.4f))); //ceiling light

            Globals.PointLights.Add(new PointLight(new Vector3(9.3f,2.96f,5.8f), new Vector3(1,1f,.8f)*.2f)); //window lighbt
            Globals.PointLights.Add(new PointLight(new Vector3(6.65284f,3.30652f,1.68395f), new Vector3(1,.8f,.6f)*.4f)); //lamp
            
            //Globals.PointLights.Add(new PointLight(new Vector3(30,30,30), new Vector3(0))); //basement
            Globals.PointLights.Add(new PointLight(new Vector3(-18.98f,-6.76f,51.17f), new Vector3(.4f))); //basement
            
            
            float near = 0.1f;
            float far = 100f;

            Globals.PlayerCameraRoom1.CopyFrom(Camera.CreatePerspective(Vector3.Zero, Quaternion.Identity,  MathHelper.DegreesToRadians(90), near, far));
            Globals.UberDriver.CopyFrom(Camera.CreatePerspective(Vector3.Zero, Quaternion.Identity,  MathHelper.DegreesToRadians(90), near, far));

            Globals.ShadowCastingLightRoom1.CopyFrom(Camera.CreateOrthographic(new Vector3(0,10,0), Quaternion.FromAxisAngle(Vector3.UnitX, -MathF.PI/2), 25, near, far));

            Globals.WebCamRoom1.CopyFrom(Camera.CreatePerspective(new Vector3(0), Quaternion.Identity, MathHelper.DegreesToRadians(120), near, far));
            Globals.WebCamRoom1.OverrideFrustrumDimensions(2, 100);
            
            Globals.PlayerCameraRoom2.CopyFrom(Globals.PlayerCameraRoom1);
            Globals.ShadowCastingLightRoom2.CopyFrom( Globals.ShadowCastingLightRoom1);

            Globals.MainCamera.CopyFrom(Globals.PlayerCameraRoom1);
 
        }
        
        static Wobble CreateEyeVisualizer(Vector3 position, Entity parent)
        {
            var eyeRotation = Quaternion.FromEulerAngles(0, 0,MathF.PI);
            var eye = new Wobble(new Vector3(.005f), new Vector3(70f), position, new Vector3(1f), new Vector3(1f), eyeRotation,  SetupMaterials.EyeBall);
            eye.LocalScale = Vector3.One * .13f;
            eye.LocalRotation = eyeRotation;
            eye.Parent = parent;
            return eye;
        }
    }
}