using CART_457;
using CART_457.EntitySystem;
using CART_457.PhysicsRelated;
using CART_457.Renderer;
using OpenTK.Mathematics;

namespace Indpendent_Study_Fall_2020.c_sharp.Scripts.Blueprints
{
    public class RaycastChecker : Entity
    {
        public override void OnUpdate(EntityUpdateEventArgs eventArgs)
        {
            var dir = WorldRotation * Vector3.UnitZ;
            
            var ray = new Ray(WorldPosition, dir);
            CollisionWorld.ColliderGroup.Raycast(ray, out var results);
            
            Debug.Log(results[0].ToString());
        }
    }
}