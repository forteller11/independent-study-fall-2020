using OpenTK;

namespace Indpendent_Study_Fall_2020.c_sharp.Renderer
{
    public class Camera
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public Matrix4 Projection;

        public Camera(Vector3 position, Quaternion rotation, Matrix4 projection)
        {
            Position = position;
            Rotation = rotation;
            Projection = projection;
        }
    }
}