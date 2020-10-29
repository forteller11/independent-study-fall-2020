using System;
using CART_457;
using CART_457.c_sharp.Renderer;
using CART_457.EntitySystem;
using CART_457.EntitySystem.Scripts.EntityPrefab;
using CART_457.MaterialRelated;
using CART_457.Renderer;
using CART_457.Scripts;
using OpenTK;

namespace Indpendent_Study_Fall_2020.c_sharp.Scripts.Entity
{
    public class EmptySolid : CART_457.EntitySystem.Entity
    {
        public Vector4 Color;

        public EmptySolid(Vector4 color, float scale, params Material[] materials)
        {
            Color = color;
            Scale *= scale;
            SetupMaterials(materials);
        }

        public override void OnUpdate(EntityUpdateEventArgs eventArgs)
        {
        
        }

        public override void SendUniformsPerObject(Material material)
        {
            // Debug.Log("SendUniforms");
            if (material == CART_457.Scripts.InitMaterials.ShadowMapSphere || material == CART_457.Scripts.InitMaterials.ShadowMapPlane)
            {
                UniformSender.SendTransformMatrices(this, material, Globals.ShadowCastingLight, "Light");
            }
            else
            { 
                UniformSender.SendTransformMatrices(this, material, Globals.MainCamera);
                UniformSender.SetVector4(material, "Color", Color, false);
            }
            

            // Debug.Log("====");
        }
    }
}