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
        public static readonly Camera PlayerCamera = new Camera();
        public static readonly Camera WebCam = new Camera();
        public static readonly Camera ShadowCastingLight = new Camera();
        public static readonly Camera UberDriver = new Camera();
        
        public static RandomInd Random = new RandomInd(0);
        public static FastNoiseLite Noise = new FastNoiseLite();
        public static double AbsTime = 0;
        public static float AbsTimeF = 0;
        public static double DeltaTime = 0;
        public static float DeltaTimeF = 0;
        
        public static List<DirectionLight> DirectionLights;
        public static List<PointLight> PointLights;
        public static Vector3 ShadowDirection;


        public static void Init(GameWindow gameWindow)
        {
            DirectionLights = new List<DirectionLight>();
            PointLights = new List<PointLight>();

            Noise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2);
        }

        public static void Update(EntityUpdateEventArgs args)
        {
            DeltaTime = args.DeltaTime;
            DeltaTimeF = (float) DeltaTime;
            
            AbsTime += args.DeltaTime;
            AbsTimeF = (float) AbsTime;
            
            // ShadowCastingLight.Rotation = Quaternion.FromEulerAngles(MathF.PI,0,0)

        }

    }
}