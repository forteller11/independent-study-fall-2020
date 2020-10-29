using System;
using CART_457.Helpers;
using OpenTK;

namespace CART_457.Renderer
{
    public class Camera
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public Matrix4 Projection;
        public float NearClip;
        public float FarClip;

        public Camera(){}
        public Camera(Vector3 position, Quaternion rotation, Matrix4 projection, float nearClip, float farClip)
        {
            Position = position;
            Rotation = rotation;
            Projection = projection;
            NearClip = nearClip;
            FarClip = farClip;
        }

        public static Camera Lerp(Camera cam1, Camera cam2, float t)
        {
            var camera = new Camera();
            
            camera.Position = Vector3.Lerp(cam1.Position, cam2.Position, t);
            camera.Rotation = Quaternion.Slerp(cam1.Rotation, cam2.Rotation, t);
            camera.Projection = MathInd.Lerp(cam1.Projection, cam2.Projection, t);
            camera.NearClip = MathInd.Lerp(cam1.NearClip, cam2.NearClip, t);
            camera.FarClip = MathInd.Lerp(cam1.FarClip, cam2.FarClip, t);

            return camera;
        }
    }
}