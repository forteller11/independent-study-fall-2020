using System;
using System.Collections.Generic;
using Indpendent_Study_Fall_2020.c_sharp.EntitySystem.Renderer;
using Indpendent_Study_Fall_2020.c_sharp.Renderer;
using OpenTK;
using OpenTK.Input;

namespace Indpendent_Study_Fall_2020.EntitySystem
{
    public static class Globals
    {
        public static Camera MainCamera;
        public static Camera ShadowCastingLight;


        public static Random Random;
        public static List<DirectionLight> DirectionLights;
        public static List<PointLight> PointLights;
        public static double AbsTime = 0;
        public static float AbsTimeF = 0;

        public static Vector2 MousePositionLastFrame;
        public static void Init()
        {
            Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(90), 1, .1f, 100f, out var mainCamPerspective);
            MainCamera = new Camera(Vector3.Zero, Quaternion.Identity, mainCamPerspective);
            
            Matrix4.CreateOrthographic(20, 20, .1f, 100f, out var shadowLightPerspective);
            ShadowCastingLight = new Camera(new Vector3(0,0,10), Quaternion.FromEulerAngles(90,0,0), shadowLightPerspective);
            
            DirectionLights = new List<DirectionLight>();
            PointLights = new List<PointLight>();
            Random = new Random(0);
            MousePositionLastFrame = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
        }

        public static void Update(EntityUpdateEventArgs args)
        {
            AbsTime += args.DeltaTime;
            AbsTimeF = (float) AbsTime;
            MousePositionLastFrame = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);

        }

    }
}