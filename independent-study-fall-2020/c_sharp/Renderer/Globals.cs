﻿using System;
using System.Collections.Generic;
using Indpendent_Study_Fall_2020.c_sharp.EntitySystem.Renderer;
using OpenTK;

namespace Indpendent_Study_Fall_2020.EntitySystem
{
    public static class Globals
    {
        public static DrawManager DrawManager = new DrawManager();
        
        public static Vector3 CameraPosition;
        public static Quaternion CameraRotation;
        public static Matrix4 CameraPerspective;
        public static Random Random;
        public static List<DirectionLight> DirectionLights;
        public static List<PointLight> PointLights;
        public static double AbsTime = 0;
        public static float AbsTimeF = 0;

        public static void Init()
        {
            Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(90), 1, .1f, 100f, out CameraPerspective);
            DirectionLights = new List<DirectionLight>();
            PointLights = new List<PointLight>();
            Random = new Random(0);
        }

        public static void Update(GameObjectUpdateEventArgs args)
        {
            AbsTime += args.DeltaTime;
            AbsTimeF = (float) AbsTime;
        }

    }
}