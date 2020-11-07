using System;
using CART_457.c_sharp.Renderer;
using CART_457.EntitySystem;
using CART_457.Helpers;
using CART_457.MaterialRelated;
using CART_457.PhysicsRelated;
using CART_457.Renderer;
using CART_457.Scripts.Blueprints;
using CART_457.Scripts.Setups;

using OpenTK.Mathematics;

namespace CART_457.Scripts.EntityPrefabs
{
    public class CameraVisualizer : EntitySystem.Entity
    {
        //onload, look at every camera using reflection, add gameobject

        private Entity Table;
        private Entity _playerCamVisualizer;
        private Entity _webCamVisualizer;

        public CameraVisualizer(Entity table)
        {
            Table = table;
        }
        public override void OnLoad()
        {
            Wobble CreateAndAddEye(Vector3 position)
            {
                var eyeRotation = Quaternion.FromEulerAngles(0, 0,MathF.PI);
                var eye = new Wobble(.005f, 70f, position, SetupMaterials.EyeBall);
                eye.LocalScale = Vector3.One * .13f;
                eye.LocalRotation = eyeRotation;
                
                eye.Parent = _playerCamVisualizer;
                EntityManager.AddToWorldAndRenderer(eye);
                return eye;
            }

            
            _webCamVisualizer = EntityManager.AddToWorldAndRenderer(new Empty(SetupMaterials.EyeBall, SetupMaterials.ShadowMapSphere));
        
            _webCamVisualizer.LocalPosition = new Vector3(.15f,1.79f,-.37f);
            _webCamVisualizer.LocalScale *= 0.05f;
            _webCamVisualizer.LocalRotation = Quaternion.FromEulerAngles(0,MathF.PI,0); //negate table rolled
            _webCamVisualizer.AddCollider(new SphereCollider(_webCamVisualizer, 6f));

            _playerCamVisualizer = EntityManager.AddToWorldAndRenderer(new Empty());
            
            CreateAndAddEye(new Vector3(-.33f, 0, 0));
            CreateAndAddEye(new Vector3(.33f, 0, 0));
        }

        public override void OnUpdate(EntityUpdateEventArgs eventArgs)
        {
            EntityTransformToCamera(_playerCamVisualizer, Globals.PlayerCameraRoom1);
            Globals.WebCamRoom1.ToEntityOrientation(_webCamVisualizer);

            // Debug.Log(PhysicsHelpersInd.IsPointWithinFrustrum(_playerCamVisualizer.WorldPosition, Globals.ShadowCastingLightRoom1));
        }

        private void EntityTransformToCamera(Entity entity, Camera camera)
        {
            entity.LocalPosition = camera.Position;
            entity.LocalRotation = camera.Rotation;
        }

        
    }
}