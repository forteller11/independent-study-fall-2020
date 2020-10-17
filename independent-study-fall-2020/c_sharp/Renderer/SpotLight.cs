using OpenTK;

namespace CART_457.Renderer
{
    public class PointLight
    {
        public Vector3 Position;
        public Vector3 Color;

        public PointLight(Vector3 position, Vector3 color)
        {
            Position = position;
            Color = color;
        }
    }
}