using System;
using OpenTK;

namespace Indpendent_Study_Fall_2020.Helpers
{
    public class Color
    {
        public float R;
        public float G;
        public float B;
        public float A;
        
        public Color(float r, float g, float b, float a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public static Color RandomColor(Random random)
        {
            var r = random.Next(10000) / 10000f;
            var g = random.Next(10000) / 10000f;
            var b = random.Next(10000) / 10000f;
            var a = random.Next(10000) / 10000f;
            return new Color(r,g,b,a);
        }
        
        public Color WithAlpha(float newAlpha) => new Color(R,G,B,newAlpha);

        public static implicit operator Vector4(Color c) => new Vector4(c.R,c.G,c.B,c.A);
        public static implicit operator Color(Vector4 v) => new Color(v.X,v.Y,v.Z,v.W);
        
    }
}