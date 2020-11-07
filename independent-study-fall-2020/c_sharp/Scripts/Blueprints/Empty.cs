using System;
using CART_457.c_sharp.Renderer;
using CART_457.EntitySystem;
using CART_457.MaterialRelated;
using OpenTK.Mathematics;

namespace CART_457.Scripts.Blueprints
{
    public class Empty : EntitySystem.Entity
    {
        
        public Action<Entity> UpdateAction;
        public Empty(params Material[] materials) : base(materials) { }

        
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
        
    
        public override void SendUniformsPerEntity(Material material)
        {
            // UniformSender.SetFloat(material, UniformSender.SPECULAR_ROUGHNESS, 128);
            // UniformSender.SetFloat(material, UniformSender.NORMAL_MAP_STRENGTH, 64);
        }

        
    }
}