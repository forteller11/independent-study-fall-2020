using System;
using System.Collections.Generic;
using CART_457.EntitySystem;
using CART_457.Helpers;
using OpenTK;
using OpenTK.Input;
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;

namespace CART_457.Renderer
{
    public static class Globals
    {
        public static readonly Camera MainCamera = new Camera();

        public static readonly Camera PlayerCameraRoom1 = new Camera();
        public static readonly Camera WebCamRoom1 = new Camera();
        public static readonly Camera ShadowCastingLightRoom1 = new Camera();
        
        public static readonly Camera PlayerCameraRoom2 = new Camera();
        public static readonly Camera WebCamRoom2 = new Camera();
        public static readonly Camera ShadowCastingLightRoom2 = new Camera();
        
        public static RandomInd Random = new RandomInd(0);
        public static FastNoiseLite Noise = new FastNoiseLite();
        public static List<DirectionLight> DirectionLights;
        public static List<PointLight> PointLights;
        public static double AbsTime = 0;
        public static float AbsTimeF = 0;
        public static double DeltaTime = 0;
        public static float DeltaTimeF = 0;

        public static Vector2 MousePositionLastFrame;
        public static void Init(GameWindow gameWindow)
        {
            DirectionLights = new List<DirectionLight>();
            PointLights = new List<PointLight>();
            MousePositionLastFrame = gameWindow.MouseState.Position;
            
            Noise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2);
        }

        public static void Update(EntityUpdateEventArgs args)
        {
            DeltaTime = args.DeltaTime;
            DeltaTimeF = (float) DeltaTime;
            
            AbsTime += args.DeltaTime;
            AbsTimeF = (float) AbsTime;
            MousePositionLastFrame = args.MouseState.Position;
            // ShadowCastingLight.Rotation = Quaternion.FromEulerAngles(MathF.PI,0,0)

        }

    }
}