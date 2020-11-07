using System.Runtime.CompilerServices;
using OpenTK.Mathematics;

namespace CART_457.Helpers
{
    public static class MathInd
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static float GetPercentageBetweenEdges(float edge1, float edge2, float value)
        {
            float sum = edge2 - edge1;
            float valueWithEdge1Origin = value - edge1;
            return valueWithEdge1Origin / sum;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static float Lerp(float n1, float n2, float t)
        {
            float diff = n2 - n1;
            return (diff * t) + n1;
        }
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Matrix4 Lerp(Matrix4 m1, Matrix4 m2, float t)
        {
            var result = new Matrix4();
            result.M11 = Lerp(m1.M11, m2.M11, t);
            result.M12 = Lerp(m1.M12, m2.M12, t);
            result.M13 = Lerp(m1.M13, m2.M13, t);
            result.M14 = Lerp(m1.M14, m2.M14, t);
            
            result.M21 = Lerp(m1.M21, m2.M21, t);
            result.M22 = Lerp(m1.M22, m2.M22, t);
            result.M23 = Lerp(m1.M23, m2.M23, t);
            result.M24 = Lerp(m1.M24, m2.M24, t);
            
            result.M31 = Lerp(m1.M31, m2.M31, t);
            result.M32 = Lerp(m1.M32, m2.M32, t);
            result.M33 = Lerp(m1.M33, m2.M33, t);
            result.M34 = Lerp(m1.M34, m2.M34, t);
            
            result.M41 = Lerp(m1.M41, m2.M41, t);
            result.M42 = Lerp(m1.M42, m2.M42, t);
            result.M43 = Lerp(m1.M43, m2.M43, t);
            result.M44 = Lerp(m1.M44, m2.M44, t);

            return result;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static float SmoothStep(float t) //3x^2 - 2x^3
        {
            return MathHelper.Clamp((3 * t * t) - (2 * t * t * t), 0,1);
        }
        
      
   
    }
}