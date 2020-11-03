using System;
using System.ComponentModel.DataAnnotations.Schema;
using CART_457.EntitySystem.Scripts.Blueprints;
using CART_457.Renderer;
using CART_457.Scripts.Setups;
using Indpendent_Study_Fall_2020.c_sharp.Scripts.Blueprints;
using OpenTK;
using OpenTK.Mathematics;

namespace CART_457.EntitySystem.Scripts.EntityPrefabs
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
            _webCamVisualizer = EntityManager.AddToWorldAndRenderer(new EmptySolid(new Vector4(1,0,1,1), 1f, SetupMaterials.Camera, SetupMaterials.ShadowMapDiamond));
            _webCamVisualizer.Parent = Table;
            _webCamVisualizer.LocalPosition = new Vector3(-.3f,1.9f,.95f);
            _webCamVisualizer.LocalScale *= 0.2f;

            _playerCamVisualizer = EntityManager.AddToWorldAndRenderer(new Empty());
            var eyeRotation = Quaternion.FromEulerAngles(MathF.PI/2, 0,MathF.PI);
            var eye1 = Empty.FromPositionRotationScale(new Vector3( -.33f, 0, 0), eyeRotation, Vector3.One*.13f, SetupMaterials.EyeBall);
  
            var eye2 = Empty.FromPositionRotationScale(new Vector3(.33f, 0, 0), eyeRotation,Vector3.One*.13f, SetupMaterials.EyeBall);
            eye1.Parent = _playerCamVisualizer;
            eye2.Parent = _playerCamVisualizer;

            EntityManager.AddToWorldAndRenderer(eye1);
            EntityManager.AddToWorldAndRenderer(eye2);
        }

        public override void OnUpdate(EntityUpdateEventArgs eventArgs)
        {
            EntityTransformToCamera(_playerCamVisualizer, Globals.PlayerCameraRoom1);
            Globals.WebCamRoom1.ToEntityOrientation(_webCamVisualizer);
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