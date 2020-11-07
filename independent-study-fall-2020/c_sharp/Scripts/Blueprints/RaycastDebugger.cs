using CART_457;
using CART_457.EntitySystem;
using CART_457.PhysicsRelated;
using CART_457.Renderer;
using CART_457.Scripts.Blueprints;
using CART_457.Scripts.Setups;
using OpenTK.Mathematics;

namespace Indpendent_Study_Fall_2020.c_sharp.Scripts.Blueprints
{
    public class RaycastDebugger : Entity
    {
        private EmptySolid [] VisulizerHits;
        private Vector3 [] Directions;

        public override void OnLoad()
        {
            const int num = 5;
            VisulizerHits = new EmptySolid[num];
            Directions = new Vector3[num];
            for (int i = 0; i < VisulizerHits.Length; i++)
            {
                VisulizerHits[i] = new EmptySolid(new Vector4(1,0,0,1), .002f, SetupMaterials.SolidSphereR1);
                Directions[i] = Vector3.Normalize(Vector3.Lerp(Globals.Random.NextDirection(), Vector3.UnitZ, 0.8f));
            }
        }

        public override void OnUpdate(EntityUpdateEventArgs eventArgs)
        {
            for (int i = 0; i < VisulizerHits.Length; i++)
            {
                VisulizerHits[i] = new EmptySolid(new Vector4(0,1,1,1), .08f, SetupMaterials.SolidSphereR1);
                 //var dir = WorldRotation * Directions[i];
                var dir = Globals.MainCamera.Rotation * new Vector3(0,0,1);
                if (CollisionWorld.ColliderGroup.Raycast(new Ray(Globals.MainCamera.Position, dir), out var results, true))
                {
                    // var dir = WorldRotation * Vector3.UnitZ;
                    VisulizerHits[i].LocalPosition = results[0].NearestOrHitPosition;
                    VisulizerHits[i].Color = new Vector4(0, 1, 0, 1);
                }
                else
                {
                    VisulizerHits[i].Color = new Vector4(.5f, 0, 0, 1);
                }
            }
        }
    }
}