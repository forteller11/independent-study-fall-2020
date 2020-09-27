using System;
using System.Collections.Generic;
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
        public static double AbsoluteTime = 0;


        


        public static void Init()
        {
            Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(90), 1, .1f, 100f, out CameraPerspective);
            DirectionLights = new List<DirectionLight>();
            Random = new Random(0);
        }

        public static void Update(GameObjectUpdateEventArgs args)
        {
            AbsoluteTime += args.DeltaTime;
        }

    }
}