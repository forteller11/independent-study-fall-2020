using System.Data;
using OpenTK;
using OpenTK.Graphics;

namespace CART_457.Renderer
{
    public class DirectionLight
    {
        public Vector3 Direction;
        public Vector3 Color;

        public DirectionLight(Vector3 direction, Vector3 color)
        {
            if (!direction.EqualsAprox(direction.Normalized()))
                throw new DataException($"direction at directionLight is not normalized!");
            Direction = direction;
            Color = color;
        }
    }
}