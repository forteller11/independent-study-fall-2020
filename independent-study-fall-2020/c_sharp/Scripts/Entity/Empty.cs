using System;
using System.IO.Compression;
using CART_457.c_sharp.Renderer;
using CART_457.MaterialRelated;
using CART_457.Renderer;
using CART_457.Scripts;
using OpenTK;

namespace CART_457.EntitySystem.Scripts.Entity
{
    public class Empty : EntitySystem.Entity
    {
        private Empty(Material[] materials)
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
                float t = (MathF.Cos(Globals.AbsTimeF*1f)/2) + .5f;

                t = 0;
                // Debug.Log(t);
                var camResult = new Camera();
                Camera.Lerp(Globals.MainCamera, Globals.ShadowCastingLight, t, ref camResult);
                
                UniformSender.SendTransformMatrices(this, material, camResult);
                UniformSender.SendTransformMatrices(this, material, Globals.ShadowCastingLight, "Light");
                UniformSender.SendLights(material);
                UniformSender.SendTime(material);
            }

        }
    }
}