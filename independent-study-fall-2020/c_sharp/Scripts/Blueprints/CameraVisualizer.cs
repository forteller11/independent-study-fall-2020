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

        private Entity _playerCamVisualizer;
        public Entity WebCamVisualizer;

        public override void OnLoad()
        {
            float near = 0.01f;
            float far = 100f;
            Globals.WebCamRoom1.CopyFrom(Camera.CreatePerspective(new Vector3(0), Quaternion.FromEulerAngles(0,MathF.PI,0), MathHelper.DegreesToRadians(160), near, far));
     
        }
        
        public CameraVisualizer(Vector3 cameraPosition, float cameraScale)
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

            
            WebCamVisualizer = EntityManager.AddToWorldAndRenderer(new Empty(SetupMaterials.EyeBall, SetupMaterials.ShadowMapSphere));
        
            WebCamVisualizer.LocalPosition = cameraPosition;
            WebCamVisualizer.LocalScale *= cameraScale;
            WebCamVisualizer.LocalRotation = Quaternion.FromEulerAngles(0,0,0); //negate table rolled
            WebCamVisualizer.AddCollider(new SphereCollider(WebCamVisualizer, true, 1f));

            _playerCamVisualizer = EntityManager.AddToWorldAndRenderer(new Empty());
            
            CreateAndAddEye(new Vector3(-.33f, 0, 0));
            CreateAndAddEye(new Vector3(.33f, 0, 0));
        }

        


        public override void OnUpdate(EntityUpdateEventArgs eventArgs)
        {
            EntityTransformToCamera(_playerCamVisualizer, Globals.PlayerCameraRoom1);
            Globals.WebCamRoom1.ToEntityOrientation(WebCamVisualizer);

            // Debug.Log(PhysicsHelpersInd.IsPointWithinFrustrum(_playerCamVisualizer.WorldPosition, Globals.ShadowCastingLightRoom1));
        }

        private void EntityTransformToCamera(Entity entity, Camera camera)
        {
            entity.LocalPosition = camera.Position;
            entity.LocalRotation = camera.Rotation;
        }

        
    }
}