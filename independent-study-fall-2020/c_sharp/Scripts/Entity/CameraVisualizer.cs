using System;
using CART_457.EntitySystem.Scripts.EntityPrefab;
using CART_457.Renderer;
using CART_457.Scripts;
using Indpendent_Study_Fall_2020.c_sharp.Scripts.Entity;
using OpenTK;

namespace CART_457.EntitySystem.Scripts.EntityPrefabs
{
    public class CameraVisualizer : EntitySystem.Entity
    {
        //onload, look at every camera using reflection, add gameobject

        private Entity _playerCamVisualizer;
        private Entity _webCamVisualizer;

        public override void OnLoad()
        {
            _playerCamVisualizer = AddCamera(new Vector4(1,1,0,1));
            _webCamVisualizer = AddCamera(new Vector4(1,0,1,1));

        }

        public override void OnUpdate(EntityUpdateEventArgs eventArgs)
        {
            EntityTransformToCamera(_playerCamVisualizer, Globals.PlayerCamera);
            EntityTransformToCamera(_webCamVisualizer, Globals.WebCam);
        }

        private Entity AddCamera(Vector4 color)
        {
            var entity = new EmptySolid(color, 0.2f, InitMaterials.Camera, InitMaterials.ShadowMapDiamond);
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