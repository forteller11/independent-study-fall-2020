using System.Data;
using OpenTK;
using OpenTK.Graphics;

namespace Indpendent_Study_Fall_2020
{
    public struct DirectionLight
    {
        public Vector3 Direction;
        public Vector3 Color;

        public DirectionLight(Vector3 direction, Color4 color)
        {
            if (!direction.EqualsAprox(direction.Normalized()))
                throw new DataException($"direction at directionLight is not normalized!");
            Direction = direction;
            Color = color;
        }
    }
}