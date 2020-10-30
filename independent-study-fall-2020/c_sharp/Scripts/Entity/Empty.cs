using System;
using System.IO.Compression;
using CART_457.c_sharp.Renderer;
using CART_457.MaterialRelated;
using CART_457.Renderer;
using CART_457.Scripts;
using OpenTK;

namespace CART_457.EntitySystem.Scripts.EntityPrefab
{
    public class Empty : EntitySystem.Entity
    {
        
        public Action<Entity> UpdateAction;
        public Empty(params Material[] materials)
        {
            SetupMaterials(materials);
        }

        
        public override void OnUpdate(EntityUpdateEventArgs eventArgs)
        {
            UpdateAction?.Invoke(this);
        }

        public static Empty FromPosition (Vector3 position,  params Material [] materials)
        {
            var empty = new Empty(materials);
            empty.LocalPosition = position;
            return empty;
        }
        
        public static Empty FromPositionRotation (Vector3 position, Quaternion rotation,  params Material [] materials)
        {
            var empty = new Empty(materials);
            empty.LocalPosition = position;
            empty.LocalRotation = rotation;
            return empty;
        }
        
        public static Empty FromPositionRotationScale (Vector3 position, Quaternion rotation, Vector3 scale,  params Material [] materials)
        {
            var empty = new Empty(materials);
            empty.LocalPosition = position;
            empty.LocalRotation = rotation;
            empty.LocalScale = scale;
            return empty;
        }
        
        public override void SendUniformsPerObject(Material material)
        {

        }
    }
}