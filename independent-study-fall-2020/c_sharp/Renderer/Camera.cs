using System;
using CART_457.EntitySystem;
using CART_457.Helpers;

using OpenTK.Mathematics;

namespace CART_457.Renderer
{
    public class Camera
    {
        public Vector3 Position;
        public Quaternion Rotation;
        private Matrix4 _projection;

        public Matrix4 Projection
        {
            get => _projection;
            set => _projection = value;
        }
    
    private float _fov; ///in radians
        public float FOV => _fov; ///in radians
        public float NearClip;
        public float FarClip;

        public float NearClipWidth;
        public float FarClipWidth;
        
        

        public Camera(){}

        public static Camera CreatePerspective(Vector3 position, Quaternion rotation, float fov, float nearClip, float farClip)
        {
            Matrix4.CreatePerspectiveFieldOfView(fov, 1, nearClip, farClip, out var playerCamPerspective);
            float nearClipSize = GetPlaneWidthFromFOV(fov, nearClip);
            float farClipSize  = GetPlaneWidthFromFOV(fov, farClip);

            return new Camera(position, rotation, playerCamPerspective, nearClip, farClip, nearClipSize, farClipSize);
        }

        public void OverrideFrustrumDimensions(float nearClipWidth, float farClipWidth) //NOTE!! Frustrum will no longer match camera perspective!!
        {
            NearClipWidth = nearClipWidth;
            FarClipWidth = farClipWidth;
        }
        
        public static Camera CreateOrthographic(Vector3 position, Quaternion rotation, float size, float nearClip, float farClip)
        {
            Matrix4.CreateOrthographic(25, 25, size,size, out var projection);
            return new Camera(position, rotation, projection, nearClip, farClip, size, size);
        }
        
        private Camera(Vector3 position, Quaternion rotation, Matrix4 projection, float nearClip, float farClip, float nearClipSize, float farClipSize)
        {
            Position = position;
            Rotation = rotation;
            Projection = projection;
            
            NearClip = nearClip;
            FarClip = farClip;
            
            NearClipWidth = nearClipSize;
            FarClipWidth = farClipSize;
        }

        private static float GetPlaneWidthFromFOV(float fov, float distanceInFrustrum)
        {
            if (MathF.Abs(fov) >= MathF.PI)
                throw new ArgumentException($"FOV is {fov} but cannot be greater than PI or {MathF.PI}");
            
            Matrix2 rotateLeft = Matrix2.CreateRotation(fov/2);
            Matrix2 rotateRight = Matrix2.CreateRotation(-fov/2);

            Vector2 leftEdge  = Vector2.TransformRow(new Vector2(0,1), rotateLeft);
            Vector2 rightEdge = Vector2.TransformRow(new Vector2(0,1), rotateRight);

            Vector2 planeDistanceVector = new Vector2(0, distanceInFrustrum);

            Vector2 leftEdgeDistanceInFrustrumProjection  = Vector2.Dot(leftEdge, planeDistanceVector) * leftEdge;
            Vector2 rightEdgeDistanceInFrustrumProjection = Vector2.Dot(rightEdge, planeDistanceVector) * rightEdge;

            float distanceBetweenPointsOnFrustrum = rightEdgeDistanceInFrustrumProjection.X - leftEdgeDistanceInFrustrumProjection.X;

            return distanceBetweenPointsOnFrustrum;
        }

        public static void Lerp(Camera cam1, Camera cam2, float t, Camera camResult)
        {
            camResult.Position = Vector3.Lerp(cam1.Position, cam2.Position, t);
            camResult.Rotation = Quaternion.Slerp(cam1.Rotation, cam2.Rotation, t);
            camResult.Projection = MathInd.Lerp(cam1.Projection, cam2.Projection, t);
            
            camResult.NearClip = MathInd.Lerp(cam1.NearClip, cam2.NearClip, t);
            camResult.FarClip = MathInd.Lerp(cam1.FarClip, cam2.FarClip, t);
            
            camResult.NearClipWidth = MathInd.Lerp(cam1.NearClipWidth, cam2.NearClipWidth, t);
            camResult.FarClipWidth = MathInd.Lerp(cam1.FarClipWidth, cam2.FarClipWidth, t);
        }

        public void CopyFrom(Camera copy)
        {
            Position = copy.Position;
            Rotation = copy.Rotation;
            Projection = copy.Projection;
            
            NearClip = copy.NearClip;
            FarClip = copy.FarClip;
            
            NearClipWidth = copy.NearClipWidth;
            FarClipWidth = copy.FarClipWidth;
        }

        //nearplane width
        //nearplane height
        
        public void ToEntityOrientation(Entity entity)
        {
            Position = entity.WorldPosition;
            Rotation = entity.WorldRotation;
        }
    }
}