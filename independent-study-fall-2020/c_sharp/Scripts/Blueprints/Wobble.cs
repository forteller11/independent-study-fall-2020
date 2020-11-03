using CART_457;
using CART_457.EntitySystem;
using CART_457.MaterialRelated;
using CART_457.Renderer;
using OpenTK.Mathematics;

namespace Indpendent_Study_Fall_2020.c_sharp.Scripts.Blueprints
{
    public class Wobble : Entity
    {
        public float Radius;
        public float Speed;
        public Vector3 Anchor;
        public float Seed;
        
        public Wobble(float wobbleRadius, float wobbleSpeed, Vector3 anchor, params Material[] mats)
        {
            AssignMaterials(mats);
            Radius = wobbleRadius;
            Speed = wobbleSpeed;
            Anchor = anchor;
            Seed = (float) (Globals.Random.NextDouble()*9999);
            Debug.Log(Seed);
        }

        public override void OnUpdate(EntityUpdateEventArgs eventArgs)
        {
            float noiseX = Globals.Noise.GetNoise(Globals.AbsTimeF * Speed, Seed);
            float noiseY = Globals.Noise.GetNoise(Globals.AbsTimeF * Speed, Seed+9999);
            float noiseZ = Globals.Noise.GetNoise(Globals.AbsTimeF * Speed, Seed+9999*2);
            Vector3 noise = new Vector3(noiseX, noiseY, noiseZ);
      
            noise *= Radius;
            LocalPosition = noise + Anchor;
        }
    }
}