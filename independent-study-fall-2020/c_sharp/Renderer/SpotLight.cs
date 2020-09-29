using OpenTK;

namespace Indpendent_Study_Fall_2020.c_sharp.EntitySystem.Renderer
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