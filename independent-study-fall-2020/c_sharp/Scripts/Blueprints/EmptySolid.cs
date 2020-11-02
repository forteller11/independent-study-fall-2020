using CART_457.c_sharp.Renderer;
using CART_457.EntitySystem;
using CART_457.MaterialRelated;
using CART_457.Renderer;
using CART_457.Scripts.Setups;
using OpenTK;
using OpenTK.Mathematics;

namespace Indpendent_Study_Fall_2020.c_sharp.Scripts.Blueprints
{
    public class EmptySolid : CART_457.EntitySystem.Entity
    {
        public Vector4 Color;

        public EmptySolid(Vector4 color, float scale, params Material[] materials)
        {
            Color = color;
            LocalScale *= scale;
            AssignMaterials(materials);
        }

        public override void OnUpdate(EntityUpdateEventArgs eventArgs)
        {
        
        }

        public override void SendUniformsPerObject(Material material)
        {
            // Debug.Log("SendUniforms");
            if (material == SetupMaterials.ShadowMapSphere || material == SetupMaterials.ShadowMapPlane)
            {
                UniformSender.SendTransformMatrices(this, material, Globals.ShadowCastingLightRoom1, "Light");
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