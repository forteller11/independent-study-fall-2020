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
    
             FrustrumNormal.FromPositionRotationScale(true, new Vector3(0,0,0),Quaternion.Identity, new Vector3(1f), SetupMaterials.BedroomDirtyOutCamera, SetupMaterials.BedroomDirtyShadow);
             FrustrumNormal.FromPositionRotationScale(false, new Vector3(0,0,0),Quaternion.Identity, new Vector3(1f), SetupMaterials.BedroomCeilingLampsInCamera);
             
             FrustrumNormal.FromPositionRotationScale(false, new Vector3(0,0,0),Quaternion.Identity, new Vector3(1f), SetupMaterials.BedroomCleanInCamera);
             
             FrustrumNormal.FromPositionRotationScale(false, new Vector3(0,0,0),Quaternion.Identity, new Vector3(1f), SetupMaterials.BedroomClean);
             FrustrumNormal.FromPositionRotationScale(false, new Vector3(0,0,0),Quaternion.Identity, new Vector3(1f), SetupMaterials.BedroomCleanCeilingLamps);

             new ShadowLightController();
            new Empty(SetupMaterials.Basement, SetupMaterials.BasementShadow);
            // s.LocalRotation = Quaternion.FromEulerAngles(1,1,1);
            var floorColliders = new ColliderGroup();
            floorColliders.AddCollider(new MeshCollider(null, false, SetupMeshes.RoomClean01Colliders));

            var basementCollider = new MeshCollider(null, false, SetupMeshes.BasementFloorColliders);
             var basementColliderTrigger = new FloorColliderTrigger(Globals.WebCam, basementCollider, floorColliders, false,
                 new Vector3(-7.1744f, -1.49f, 3.5f), new Vector3(-7.1744f, -1.49f, 0.6f),
                 new Vector3(-7.1744f, 4f, 3.5f), new Vector3(-7.1744f, 4f, 0.6f)
                 );

             #region camera related

            var webcamRotater = new RotateOnHit(new Vector3(1.27f,3f,7.5f), Quaternion.FromEulerAngles(.2f,1f,0));
            webcamRotater.AddCollider(new SphereCollider(webcamRotater, true, 1));
            webcamRotater.LocalScale *= .35f;
            
            var webcamVisualizer = new Empty(SetupMaterials.EyeBall, SetupMaterials.ShadowMapSphere);
            webcamVisualizer.Parent = webcamRotater;
            
            var webcamController = new CameraController(Globals.WebCam, Vector3.Zero, Quaternion.Identity);
            webcamController.Parent = webcamRotater;
            
            var playerCameraController = new PlayerController(floorColliders, Globals.PlayerCamera);
            CreateEyeVisualizer(new Vector3(0.33f, 0, 0), playerCameraController);
            CreateEyeVisualizer(new Vector3(-0.33f, 0, 0), playerCameraController);
            
            var doorOpen = new DoorOpen(playerCameraController, floorColliders);
            doorOpen.LocalPosition = new Vector3(-17.662f, -9.1f, 52.416f);
            
            var doorOpenHandle = new DoorHandle(doorOpen);
            doorOpenHandle.AddCollider(new MeshCollider(doorOpenHandle, true, SetupMeshes.DoorOpenHandle));
            doorOpenHandle.Parent = doorOpen;
            doorOpenHandle.LocalPosition = new Vector3(-3.1671f, -0.29577f, -0.26485f);
            
            var uberDriver = new UberDriver(Globals.UberDriver, new Vector3(-23.13793f,-6.2f , 93.552f), Quaternion.Identity, new Vector3(40f,0,0), .06f);
            var uberBag = new UberBag(floorColliders, playerCameraController, doorOpen, uberDriver);
            uberBag.LocalPosition = BlenderToVector3(-19.878f, -56.345f, -12.7f);

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

            var monitor = Empty.FromPositionRotationScale(new Vector3(1.3f, 2.177f, 7.4f), Quaternion.FromEulerAngles(0, MathF.PI, 0), new Vector3(0.855f, .563f, 1f), SetupMaterials.ScreenR1);

            new ScreenManager(Globals.PlayerCamera);
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

            Globals.PlayerCamera.CopyFrom(Camera.CreatePerspective(Vector3.Zero, Quaternion.Identity,  MathHelper.DegreesToRadians(90), near, far));
            Globals.UberDriver.CopyFrom(Camera.CreatePerspective(Vector3.Zero, Quaternion.Identity,  MathHelper.DegreesToRadians(90), near, far));
            
            Globals.ShadowCastingLight.CopyFrom(Camera.CreatePerspective(Vector3.Zero, Quaternion.Identity,  MathHelper.DegreesToRadians(120), near, far));
            
            Globals.WebCam.CopyFrom(Camera.CreatePerspective(new Vector3(0), Quaternion.Identity, MathHelper.DegreesToRadians(120), near, far));
            Globals.WebCam.OverrideFrustrumDimensions(2, 100);
      

            Globals.MainCamera.CopyFrom(Globals.PlayerCamera);
 
        }
        public static Vector3 BlenderToVector3(float x, float y, float z) => new Vector3(x, z, -y);
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