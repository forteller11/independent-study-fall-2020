using System;
using CART_457.EntitySystem;
using CART_457.Helpers;
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
                var eyeRotation = Quaternion.FromEulerAngles(MathF.PI/2, 0,MathF.PI);
                var eye = new Wobble(.005f, 70f, position, SetupMaterials.EyeBall);
                eye.LocalScale = Vector3.One * .13f;
                eye.LocalRotation = eyeRotation;
                
                eye.Parent = _playerCamVisualizer;
                EntityManager.AddToWorldAndRenderer(eye);
                return eye;
            }

            
            _webCamVisualizer = EntityManager.AddToWorldAndRenderer(new EmptySolid(new Vector4(1,0,1,1), 1f, SetupMaterials.Camera, SetupMaterials.ShadowMapDiamond));
            _webCamVisualizer.Parent = Table;
            _webCamVisualizer.LocalPosition = new Vector3(-.3f,1.9f,.95f);
            _webCamVisualizer.LocalScale *= 0.8f;
            _webCamVisualizer.LocalRotation = Quaternion.FromEulerAngles(0,0,0);

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

        private Entity AddCamera(Vector4 color)
        {
            var entity = new EmptySolid(color, 1f, SetupMaterials.Camera, SetupMaterials.ShadowMapDiamond);
            EntityManager.AddToWorldAndRenderer(entity);
            return entity;
        }

        private void EntityTransformToCamera(Entity entity, Camera camera)
        {
            entity.LocalPosition = camera.Position;
            entity.LocalRotation = camera.Rotation;
        }
        
    }
}