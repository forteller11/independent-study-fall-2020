using OpenTK;
using OpenTK.Mathematics;

namespace CART_457.Renderer
{
    public class PointLight
    {
        public Vector3 Position;
        public Vector3 Color;
        public float Radius;

        public PointLight(Vector3 position, Vector3 color, float radius)
        {
            Position = position;
            Color = color;
            Radius = radius;
        }
    }
}