using System.Runtime.CompilerServices;
using CART_457.Renderer;
using OpenTK.Mathematics;

namespace CART_457.Helpers
{
    public static class PhysicsHelpersInd
    {

        public static bool IsPointInFrustrum(Vector3 point, Camera camera)
        {
            Vector3 cameraNormal = camera.Rotation * Vector3.UnitZ;
            Vector3 nearPlaneCenter = camera.Position + (cameraNormal * camera.NearClip);
            Vector3 farPlaneCenter =  camera.Position + (cameraNormal * camera.FarClip);

            Quaternion toIdentity = camera.Rotation.Inverted();

            Vector3 pointAlignedSpace = PivotAbout(point, camera.Position, toIdentity);
            Vector3 nearCenterAlignedSpace = camera.Position + new Vector3(0,0,camera.NearClip);
            Vector3 farCenterAlignedSpace =  camera.Position + new Vector3(0,0,camera.FarClip);
            
            
            // float MathF.
            //lerp where u are in space
            Debug.Log($"------");
            Debug.Log($"Point :{pointAlignedSpace}");
            Debug.Log($"Near :{nearCenterAlignedSpace}");
            Debug.Log($"Far :{farCenterAlignedSpace}");
            Debug.Log($"------");
            //rotate everything to be axis aligned, 
            //then do 2D square test with sized lerped
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]

        public static Vector3 PivotAbout(Vector3 point, Vector3 pivot, Quaternion rotation)
        {
            Vector3 pointWithPivotOrigin = point - pivot;
            Vector3 pointRotated = rotation * pointWithPivotOrigin;
            return pointRotated + pivot;
        }
    }
}