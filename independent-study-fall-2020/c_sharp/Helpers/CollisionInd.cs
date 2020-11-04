using System;
using System.Runtime.CompilerServices;
using CART_457.Renderer;
using OpenTK.Mathematics;

namespace CART_457.Helpers
{
    public static class PhysicsHelpersInd
    {

        public static float WidthOfFrustrumAtPoint(Vector3 point, Camera camera)
        {
            Vector3 cameraNormal = camera.Rotation * Vector3.UnitZ;
            Vector3 nearPlaneCenter = camera.Position + (cameraNormal * camera.NearClip);
            Vector3 farPlaneCenter = camera.Position + (cameraNormal * camera.FarClip);

            Quaternion toIdentity = camera.Rotation.Inverted();

            Vector3 pointAlignedSpace = PivotAbout(point, camera.Position, toIdentity);
            Vector3 nearCenterAlignedSpace = new Vector3(0, 0, camera.NearClip) - camera.Position;
            Vector3 farCenterAlignedSpace = new Vector3(0, 0, camera.FarClip) - camera.Position;

            float t = MathInd.GetPercentageBetweenEdges(nearCenterAlignedSpace.Z, farCenterAlignedSpace.Z,
                -pointAlignedSpace.Z);
            Debug.Log($"T: {t}");
            float width = MathInd.Lerp(camera.NearClipWidth, camera.FarClipWidth, t);
            return width;
        }

        public static bool IsPointWithinFrustrum(Vector3 point, Camera camera, bool withinClipPlanes=false)
        {
            Quaternion toIdentity = camera.Rotation.Inverted();

            Vector3 pointAlignedSpace = PivotAbout(point, camera.Position, toIdentity);
            Vector3 nearCenterAlignedSpace = new Vector3(0, 0, camera.NearClip) - camera.Position;
            Vector3 farCenterAlignedSpace  = new Vector3(0, 0, camera.FarClip)  - camera.Position;

            float t = MathInd.GetPercentageBetweenEdges(nearCenterAlignedSpace.Z, farCenterAlignedSpace.Z, -pointAlignedSpace.Z);
            
            if (withinClipPlanes)
                if (t > 1 || t < 0)
                    return false;

            float widthOfFrustrumAtPoint = MathInd.Lerp(camera.NearClipWidth, camera.FarClipWidth, t);

            Vector3 pointFrustrumOrigin = pointAlignedSpace - camera.Position;

            if (MathF.Abs(pointFrustrumOrigin.X) > widthOfFrustrumAtPoint / 2)
                return false;
            if (MathF.Abs(pointFrustrumOrigin.Y) > widthOfFrustrumAtPoint / 2)
                return false;

            return true;
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