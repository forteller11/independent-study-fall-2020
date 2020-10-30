using System;
using System.Collections.Generic;
using CART_457.EntitySystem;
using OpenTK;
using OpenTK.Input;

namespace CART_457.Renderer
{
    public static class Globals
    {
        public static Camera MainCamera = new Camera();
        public static Camera PlayerCamera;
        public static Camera WebCam;
        public static Camera ShadowCastingLight;
        
        public static Random Random;
        public static List<DirectionLight> DirectionLights;
        public static List<PointLight> PointLights;
        public static double AbsTime = 0;
        public static float AbsTimeF = 0;
        public static double DeltaTime = 0;
        public static float DeltaTimeF = 0;

        public static Vector2 MousePositionLastFrame;
        public static void Init()
        {
            float near = 0.1f;
            float far = 100f;
            
            Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(90), 1, near, far, out var playerCamPerspective);
            PlayerCamera = new Camera(Vector3.Zero, Quaternion.Identity, playerCamPerspective, near, far);
            
            Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45), 1, near, far, out var webCamPerspective);
            WebCam = new Camera(new Vector3(-.3f,1.8f,.6f), Quaternion.Identity, webCamPerspective, near, far);
            
            Matrix4.CreateOrthographic(25, 25, near, far, out var shadowLightPerspective);
            ShadowCastingLight = new Camera(new Vector3(0,10,0), Quaternion.FromAxisAngle(Vector3.UnitX, -MathF.PI/2), shadowLightPerspective, near, far);
            
            DirectionLights = new List<DirectionLight>();
            PointLights = new List<PointLight>();
            Random = new Random(0);
            MousePositionLastFrame = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
        }

        public static void Update(EntityUpdateEventArgs args)
        {
            DeltaTime = args.DeltaTime;
            DeltaTimeF = (float) DeltaTime;
            
            AbsTime += args.DeltaTime;
            AbsTimeF = (float) AbsTime;
            MousePositionLastFrame = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            // ShadowCastingLight.Rotation = Quaternion.FromEulerAngles(MathF.PI,0,0)

        }

    }
}