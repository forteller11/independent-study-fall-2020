using System;
using CART_457.Renderer;
using CART_457.Scripts;

namespace CART_457.EntitySystem.Scripts.EntityPrefabs
{
    public class CameraVisualizer : EntitySystem.Entity
    {
        //onload, look at every camera using reflection, add gameobject

        private Entity _playerCamVisualizer;
        private Entity _webCamVisualizer;

        public override void OnLoad()
        {
            _playerCamVisualizer = AddCamera();
            _webCamVisualizer    = AddCamera();

        }

        public override void OnUpdate(EntityUpdateEventArgs eventArgs)
        {
            EntityTransformToCamera(_playerCamVisualizer, Globals.PlayerCamera);
            EntityTransformToCamera(_webCamVisualizer, Globals.WebCam);
        }

        private Entity AddCamera()
        {
            var entity = new Empty(MaterialSetup.Camera, MaterialSetup.ShadowMapDiamond);
            EntityManager.AddToWorldAndRenderer(entity);
            return entity;
        }

        private void EntityTransformToCamera(Entity entity, Camera camera)
        {
            entity.Position = camera.Position;
            entity.Rotation = camera.Rotation;
        }
    }
}