using CART_457;
using CART_457.EntitySystem;
using CART_457.PhysicsRelated;
using CART_457.Renderer;
using CART_457.Scripts.Blueprints;
using CART_457.Scripts.Setups;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Indpendent_Study_Fall_2020.c_sharp.Scripts.Blueprints
{
    public class RaycastDebugger : Entity
    {
        private EmptySolid  VisulizerHit;

        public override void OnLoad()
        {
            VisulizerHit = new EmptySolid(new Vector4(1,0,0,1), .1f, SetupMaterials.SolidSphereR1);
        }

        public override void OnUpdate(EntityUpdateEventArgs eventArgs)
        {
            if (!eventArgs.MouseState.IsButtonDown(MouseButton.Left))
                return;
            
            var dir = Globals.MainCamera.Rotation * Vector3.UnitZ;
            //var dir = Globals.MainCamera.Rotation * new Vector3(0, 0, 1);
            if (CollisionWorld.ColliderGroup.Raycast(new Ray(Globals.MainCamera.Position, dir), out var results, true))
            {
                // var s = new EmptySolid(new Vector4(1,0,0,1), .03f, SetupMaterials.SolidSphereR1);
                // s.LocalPosition = results[0].NearestOrHitPosition;
                // var dir = WorldRotation * Vector3.UnitZ;
                VisulizerHit.LocalPosition = results[0].NearestOrHitPosition;
                VisulizerHit.Color = new Vector4(0, 1, 0, 1);
                // Debug.Log(results[0].NearestOrHitPosition);
            }
            else
            {
                VisulizerHit.Color = new Vector4(.5f, 0, 0, 1);
            }

        
            
            
        }
    }
}