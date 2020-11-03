﻿using System;
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
            //todo calculate size
            return new Camera(position, rotation, playerCamPerspective, nearClip, farClip, 0, 0);
        }
        
        public static Camera CreateOrthographic(Vector3 position, Quaternion rotation, float size, float nearClip, float farClip)
        {
            Matrix4.CreateOrthographic(25, 25, size,size, out var projection);
            return new Camera(position, rotation, projection, nearClip, farClip, size, size);
        }

        public static float GetPlaneWidthFromFOV(float fov, float distanceInFrustrum)
        {
            Matrix2 rotateLeft = Matrix2.CreateRotation(fov/2);
            Matrix2 rotateRight = Matrix2.CreateRotation(-fov/2);

            Vector2 leftEdge  = Vector2.TransformRow(new Vector2(0,1), rotateLeft);
            Vector2 rightEdge = Vector2.TransformRow(new Vector2(0,1), rotateRight);
            
            Debug.Log($"Left Edge:  {leftEdge}");
            Debug.Log($"Right Edge: {rightEdge}");
            
            Vector2 planeDistanceVector = new Vector2(0, distanceInFrustrum);

            Vector2 leftEdgeDistanceInFrustrumProjection  = Vector2.Dot(leftEdge, planeDistanceVector) * leftEdge;
            Vector2 rightEdgeDistanceInFrustrumProjection = Vector2.Dot(rightEdge, planeDistanceVector) * rightEdge;

            float distanceBetweenPointsOnFrustrum = rightEdgeDistanceInFrustrumProjection.X - leftEdgeDistanceInFrustrumProjection.X;
            Debug.Log($"Distance: {distanceInFrustrum}");
            Debug.Log($"Width: {distanceBetweenPointsOnFrustrum}");
            return distanceBetweenPointsOnFrustrum;
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

   

        public static void Lerp(Camera cam1, Camera cam2, float t, Camera camResult)
        {
            camResult.Position = Vector3.Lerp(cam1.Position, cam2.Position, t);
            camResult.Rotation = Quaternion.Slerp(cam1.Rotation, cam2.Rotation, t);
            camResult.Projection = MathInd.Lerp(cam1.Projection, cam2.Projection, t);
            camResult.NearClip = MathInd.Lerp(cam1.NearClip, cam2.NearClip, t);
            camResult.FarClip = MathInd.Lerp(cam1.FarClip, cam2.FarClip, t);
        }

        public void CopyFrom(Camera copy)
        {
            Position = copy.Position;
            Rotation = copy.Rotation;
            Projection = copy.Projection;
            NearClip = copy.NearClip;
            FarClip = copy.FarClip;
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