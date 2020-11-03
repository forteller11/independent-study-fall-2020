using System.Runtime.CompilerServices;
using CART_457.Renderer;
using OpenTK.Mathematics;

namespace CART_457.Helpers
{
    public static class PhysicsHelpersInd
    {

        // public static bool IsPointInFrustrum(Vector3 point, Camera camera)
        // {
        //     Vector3 cameraNormal = camera.Rotation * Vector3.UnitZ;
        //     Vector3 nearPlaneCenter = camera.Position + (cameraNormal * camera.NearClip);
        //     Vector3 farPlaneCenter =  camera.Position + (cameraNormal * camera.FarClip);
        //     
        //     //rotate everything to be axis aligned, 
        //     //then do 2D square test with sized lerped
        // }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]

        public static Vector3 PivotAbout(Vector3 point, Vector3 pivot, Quaternion rotation)
        {
            Vector3 pointWithPivotOrigin = point - pivot;
            Vector3 pointRotated = rotation * pointWithPivotOrigin;
            return pointRotated + pivot;
        }
    }
}