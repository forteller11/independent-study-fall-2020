using System;
using System.IO.Compression;
using CART_457.c_sharp.Renderer;
using CART_457.MaterialRelated;
using CART_457.Renderer;
using CART_457.Scripts;
using OpenTK;

namespace CART_457.EntitySystem.Scripts.EntityPrefabs
{
    public class Empty : EntitySystem.Entity
    {
        public Action OnRender;
        public Action OnUpdate;
        public Empty(params Material[] materials)
        {
            SetupMaterials(materials);
        }
        
        public static Empty FromPosition (Vector3 position,  params Material [] materials)
        {
            var empty = new Empty(materials);
            empty.Position = position;
            return empty;
        }
        
        public static Empty FromPositionRotation (Vector3 position, Quaternion rotation,  params Material [] materials)
        {
            var empty = new Empty(materials);
            empty.Position = position;
            empty.Rotation = rotation;
            return empty;
        }
        
        public static Empty FromPositionRotationScale (Vector3 position, Quaternion rotation, Vector3 scale,  params Material [] materials)
        {
            var empty = new Empty(materials);
            empty.Position = position;
            empty.Rotation = rotation;
            empty.Scale = scale;
            return empty;
        }
        
        public override void SendUniformsPerObject(Material material)
        {
            if (material == MaterialSetup.ShadowMapSphere || material == MaterialSetup.ShadowMapPlane)
            {
                UniformSender.SendTransformMatrices(this, material, Globals.ShadowCastingLight, "Light");
            }
            else
            {
                UniformSender.SendTransformMatrices(this, material, Globals.MainCamera);
                UniformSender.SendTransformMatrices(this, material, Globals.ShadowCastingLight, "Light");
                UniformSender.SendLights(material);
                UniformSender.SendTime(material);
            }

        }
    }
}