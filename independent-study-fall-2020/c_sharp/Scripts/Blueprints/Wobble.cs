using System.Security.Cryptography.X509Certificates;
using CART_457;
using CART_457.EntitySystem;
using CART_457.MaterialRelated;
using CART_457.Renderer;
using OpenTK.Mathematics;

namespace CART_457.Scripts.Blueprints
{
    public class Wobble : Entity
    {
        public Vector3 PositionWobbleRadius;
        public Vector3 PositionWobbleSpeed;
        public Vector3 PositionAnchor;

        public Vector3 RotationWobbleMax;
        public Vector3 RotationWobbleSpeed;
        public Quaternion RotationAnchor;
        
        public float PositionSeed;
        public float RotationSeed;
        
        public Wobble(Vector3 positionWobbleRadius, Vector3 positionWobbleSpeed, Vector3 positionAnchor, Vector3 rotationWobbleMax, Vector3 rotationWobbleSpeed, Quaternion rotationAnchor, params Material[] mats) : base(mats)
        {
            PositionWobbleRadius = positionWobbleRadius;
            PositionWobbleSpeed  = positionWobbleSpeed;
            PositionAnchor       = positionAnchor;
            
            RotationWobbleMax   = rotationWobbleMax;
            RotationWobbleSpeed = rotationWobbleSpeed;
            RotationAnchor      = rotationAnchor;
            
            
            PositionSeed = (float) Globals.Random.NextDouble(99099);
            RotationSeed = (float) Globals.Random.NextDouble(99999);
        }
        
        public static Wobble Position(Vector3 positionWobbleRadius, Vector3 positionWobbleSpeed, Vector3 positionAnchor, params Material[] mats) =>
            new Wobble(positionWobbleRadius, positionWobbleSpeed, positionAnchor, Vector3.Zero, Vector3.Zero, Quaternion.Identity, mats);
        
        public static Wobble Rotation(Vector3 rotationWobbleRadius, Vector3 rotationWobbleSpeed, Quaternion rotationAnchor, params Material[] mats) =>
            new Wobble(Vector3.Zero, Vector3.Zero, Vector3.Zero,rotationWobbleRadius, rotationWobbleSpeed, rotationAnchor, mats);

        public override void OnUpdate(EntityUpdateEventArgs eventArgs)
        {
            {
                float noiseX = Globals.Noise.GetNoise(Globals.AbsTimeF * PositionWobbleSpeed.X, PositionSeed);
                float noiseY = Globals.Noise.GetNoise(Globals.AbsTimeF * PositionWobbleSpeed.Y, PositionSeed + 9999);
                float noiseZ = Globals.Noise.GetNoise(Globals.AbsTimeF * PositionWobbleSpeed.Z, PositionSeed + 9999 * 2);
                Vector3 noise = new Vector3(noiseX, noiseY, noiseZ);
                noise *= PositionWobbleRadius;
                LocalPosition = noise + PositionAnchor;
            }

            {
                float noiseX = Globals.Noise.GetNoise(Globals.AbsTimeF * RotationWobbleSpeed.X, RotationSeed);
                float noiseY = Globals.Noise.GetNoise(Globals.AbsTimeF * RotationWobbleSpeed.Y, RotationSeed + 9999);
                float noiseZ = Globals.Noise.GetNoise(Globals.AbsTimeF * RotationWobbleSpeed.Z, RotationSeed + 9999 * 2);
                Vector3 noise = new Vector3(noiseX, noiseY, noiseZ);
                noise *= RotationWobbleMax;

                LocalRotation = RotationAnchor * Quaternion.FromEulerAngles(noise);
            }

        }
    }
}