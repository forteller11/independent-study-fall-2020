using OpenTK;

namespace Indpendent_Study_Fall_2020.c_sharp.EntitySystem.Renderer
{
    public struct PointLight
    {
        public Vector3 Position;
        public float Intensity;

        public PointLight(Vector3 position, float intensity)
        {
            Position = position;
            Intensity = intensity;
        }
    }
}