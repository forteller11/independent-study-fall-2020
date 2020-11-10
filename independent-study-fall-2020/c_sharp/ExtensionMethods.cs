using System;
using OpenTK;
using OpenTK.Mathematics;

namespace CART_457
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
        
        public static Vector3 Absolute(this Vector3 v) => new Vector3(MathF.Abs(v.X),MathF.Abs(v.Y),MathF.Abs(v.Z));
        
        public static float ComponentMean(this Vector3 v) => (v.X + v.Y + v.Z)/3;
        public static float ComponentMean(this Vector2 v) => (v.X + v.Y)/2;

        public static string ToStringSmall(this Vector3 v, int digits = 2)
        {
            float x = (float) MathHelper.Round(v.X, digits, MidpointRounding.ToEven);
            float y = (float) MathHelper.Round(v.Y, digits, MidpointRounding.ToEven);
            float z = (float) MathHelper.Round(v.Z, digits, MidpointRounding.ToEven);

            string sX = (x.ToString()).PadLeft(digits + 2, ' ');
            string sY = (y.ToString()).PadLeft(digits + 2, ' ');
            string sZ = (z.ToString()).PadLeft(digits + 2, ' ');
            return $"({sX}; {sY}; {sZ})";
        }
        

    }
}