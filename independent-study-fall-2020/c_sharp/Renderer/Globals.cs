using System;
using System.Collections.Generic;
using CART_457.EntitySystem;
using OpenTK;
using OpenTK.Input;

namespace CART_457.Renderer
{
    public static class Globals
    {
        public static readonly Camera MainCamera = new Camera();
        public static readonly Camera PlayerCamera = new Camera();
        public static readonly Camera WebCam = new Camera();
        public static readonly Camera ShadowCastingLight = new Camera();
        
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