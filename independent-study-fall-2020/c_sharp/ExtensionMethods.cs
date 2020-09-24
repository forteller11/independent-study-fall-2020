using System;
using OpenTK;

namespace Indpendent_Study_Fall_2020
{
    public static class ExtensionMethods
    {
        
        public static bool EqualsAprox(this float n1, float n2) => Math.Abs(n1 - n2) < 0.01f;
        
        public static bool EqualsAprox(this Vector3 v1, Vector3 v2)
        {
            return v1.X.EqualsAprox(v2.X) && 
                   v1.Y.EqualsAprox(v2.Y) &&
                   v1.Z.EqualsAprox(v2.Z);
        }
        
        public static bool EqualsAprox(this Vector2 v1, Vector2 v2)
        {
            return v1.X.EqualsAprox(v2.X) &&
                   v1.Y.EqualsAprox(v2.Y);
        }
    }
}