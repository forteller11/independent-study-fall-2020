using OpenTK;

namespace Indpendent_Study_Fall_2020
{
    public struct DirectionLight
    {
        public Vector3 Direction;
        public float Intensity;

        public DirectionLight(Vector3 direction, float intensity)
        {
            Direction = direction;
            Intensity = intensity;
        }
    }
}