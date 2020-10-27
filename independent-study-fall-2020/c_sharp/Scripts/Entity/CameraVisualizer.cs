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
            _webCamVisualizer = EntityManager.AddToWorldAndRenderer(new EmptySolid(new Vector4(1,0,1,1), 1f, InitMaterials.Camera, InitMaterials.ShadowMapDiamond));

            _playerCamVisualizer = EntityManager.AddToWorldAndRenderer(new Empty());
            var eye1 = Empty.FromPositionRotationScale(new Vector3( -.33f, 0, 0), Quaternion.FromAxisAngle(Vector3.UnitX, MathF.PI/2), Vector3.One*.13f, InitMaterials.EyeBall);
            var eye2 = Empty.FromPositionRotationScale(new Vector3(.33f, 0, 0), Quaternion.FromAxisAngle(Vector3.UnitX, MathF.PI/2),Vector3.One*.13f, InitMaterials.EyeBall);
            eye1.Parent = _playerCamVisualizer;
            eye2.Parent = _playerCamVisualizer;

            EntityManager.AddToWorldAndRenderer(eye1);
            EntityManager.AddToWorldAndRenderer(eye2);
        }

        public override void OnUpdate(EntityUpdateEventArgs eventArgs)
        {
            EntityTransformToCamera(_playerCamVisualizer, Globals.PlayerCamera);
            EntityTransformToCamera(_webCamVisualizer, Globals.WebCam);
        }

        private Entity AddCamera(Vector4 color)
        {
            var entity = new EmptySolid(color, 1f, InitMaterials.Camera, InitMaterials.ShadowMapDiamond);
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