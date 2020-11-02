using System;

using CART_457.MaterialRelated;
using OpenTK.Mathematics;

namespace CART_457.EntitySystem.Scripts.Blueprints
{
    public class Empty : EntitySystem.Entity
    {
        
        public Action<Entity> UpdateAction;
        public Empty(params Material[] materials)
        {
            AssignMaterials(materials);
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
            // if (material.IsShadowMapMaterial)
            // {
            //     UniformSender.SendTransformMatrices(this, material, material.RenderTarget.MainCamera, "Light");
            // }
            // else
            // {
            //     UniformSender.SendTransformMatrices(this, material,  material.RenderTarget.MainCamera);
            //     UniformSender.SendTransformMatrices(this, material, Globals.ShadowCastingLight, "Light");
            //     UniformSender.SendLights(material);
            //     UniformSender.SendGlobals(material);
            // }

        }
    }
}