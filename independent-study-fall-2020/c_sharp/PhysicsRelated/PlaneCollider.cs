using System.Numerics;
using CART_457.EntitySystem;
using Vector3 = OpenTK.Mathematics.Vector3;

namespace CART_457.PhysicsRelated
{
    public class PlaneCollider : Collider
    {
        public float Distance; //along normal
        public Vector3 Normal;

        public PlaneCollider(Entity entity, float distance, Vector3 normal) : base(entity)
        {
            Distance = distance;
            Normal = normal;
        }
    }
}