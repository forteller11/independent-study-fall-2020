using System;
using OpenTK.Audio.OpenAL;
using OpenTK.Mathematics;

namespace CART_457.EntitySystem
{
    public class RandomInd
    {
        private Random _ran;

        public RandomInd(int seed) => _ran = new Random(seed);
        
        // public float NextFloat()
        // {
        //     Random.NextDouble()
        // }

        public Vector3 NextDirection()
        {
            return Vector3.Normalize(new Vector3((float) _ran.NextDouble(), (float) _ran.NextDouble(), (float) _ran.NextDouble()));
        }

        public double NextDouble() => _ran.NextDouble();
        public double NextDouble(double max) => _ran.NextDouble();

        public double NextDouble(double min, double max) => throw new NotImplementedException();
        
    }
}