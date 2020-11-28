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
    public class CameraController : Entity
    {
        public Camera Camera;

        public CameraController(Vector3 position, Quaternion rotation, Camera cameraToControl, float cameraFOVDegrees, Entity visualizerToParent)
        {
            Camera = cameraToControl;
            
            float near = 0.01f;
            float far = 100f;
            Globals.WebCamRoom1.CopyFrom(Camera.CreatePerspective(new Vector3(0), Quaternion.Identity, MathHelper.DegreesToRadians(cameraFOVDegrees), near, far));
            
            LocalPosition = position;
            LocalRotation = rotation;
            
            if (visualizerToParent != null)
                visualizerToParent.Parent = this;
        }

        public override void OnUpdate(EntityUpdateEventArgs eventArgs)
        {
            Camera.ToEntityOrientation(this);
        }


    }
}