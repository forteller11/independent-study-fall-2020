using System;
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


        


        public static void Init()
        {
            Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(90), 1, .1f, 100f, out CameraPerspective);
//            Matrix4.CreateOrthographic(4, 4, 0.5f, 1000f, out CameraPerspective);
            Random = new Random(0);
        }

    }
}