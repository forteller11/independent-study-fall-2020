﻿using System;
using System.Collections.Generic;
using CART_457.EntitySystem;
using OpenTK;
using OpenTK.Input;

namespace CART_457.Renderer
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
            
            Matrix4.CreateOrthographic(25, 25, .1f, 100f, out var shadowLightPerspective);
            ShadowCastingLight = new Camera(new Vector3(0,10,0), Quaternion.FromAxisAngle(Vector3.UnitX, -MathF.PI/2), shadowLightPerspective);
            
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
            // ShadowCastingLight.Rotation = Quaternion.FromEulerAngles(MathF.PI,0,0)

        }

    }
}