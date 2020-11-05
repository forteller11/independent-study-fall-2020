using System;
using OpenTK.Mathematics;

namespace CART_457.PhysicsRelated
{
    public struct Ray
    {
        public Vector3 Origin;
        public Vector3 Direction;

        public Ray(Vector3 origin, Vector3 direction)
        {
            Origin = origin;
            Direction = Vector3.Normalize(direction);
        }

        public static Ray CreateAndValidate(Vector3 origin, Vector3 direction) => new Ray(origin,  Vector3.Normalize(direction));
        // public static Ray Create(Vector3 origin, Vector3 direction) => new Ray(origin, direction);

        public new string ToString()
        {
            return $"Ray: Origin {Origin.ToString()}, Direction {Direction.ToString()};";
        }
    }
}