using CART_457;
using CART_457.EntitySystem;
using CART_457.PhysicsRelated;
using CART_457.Renderer;
using CART_457.Scripts.Blueprints;
using CART_457.Scripts.Setups;
using OpenTK.Mathematics;

namespace Indpendent_Study_Fall_2020.c_sharp.Scripts.Blueprints
{
    public class RaycastChecker : Entity
    {
        private EmptySolid VisulizerHit = new EmptySolid(Vector4.Zero, .2f, SetupMaterials.SolidSphereR1);
        private EmptySolid VisulizerSphere = new EmptySolid(Vector4.Zero, .2f, SetupMaterials.SolidSphereR1);
        public override void OnUpdate(EntityUpdateEventArgs eventArgs)
        {
            Parent = null;
            LocalRotation = Globals.MainCamera.Rotation;
            LocalPosition = Globals.MainCamera.Position;
            var dir = WorldRotation * Vector3.UnitZ;
            
            var ray = new Ray(WorldPosition, dir);
            CollisionWorld.ColliderGroup.Raycast(ray, out var results);

            var result = results[0];
           
            
            VisulizerHit.Color = result.Hit ? new Vector4(.2f,.2f,1f,1) :  new Vector4(.6f,.2f,.2f,1);
            if (result.Inside)
            {
                VisulizerHit.Color = new Vector4(0,.8f,.8f,1);
            }
            if (result.Hit)
                VisulizerHit.LocalPosition = result.NearestOrHitPosition;
            
            
        }
    }
}