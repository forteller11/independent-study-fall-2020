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

        public CameraController(Camera cameraToControl, Vector3 position, Quaternion rotation)
        {
            Camera = cameraToControl;
            
            LocalPosition = position;
            LocalRotation = rotation;
        }

        public override void OnUpdate(EntityUpdateEventArgs eventArgs)
        {
            Camera.ToEntityOrientation(this);
        }


    }
}