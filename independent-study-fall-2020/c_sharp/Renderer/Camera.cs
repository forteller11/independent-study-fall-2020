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

        public Camera(Vector3 position, Quaternion rotation, Matrix4 projection, float nearClip, float farClip)
        {
            Position = position;
            Rotation = rotation;
            Projection = projection;
            NearClip = nearClip;
            FarClip = farClip;
        }
    }
}