using System;
using CART_457.c_sharp.Renderer;
using CART_457.MaterialRelated;
using CART_457.Renderer;
using CART_457.Scripts.Setups;
using OpenTK;
using OpenTK.Mathematics;

namespace CART_457.EntitySystem.Scripts.Blueprints
{
    public class PointLightVisualizer : EntitySystem.Entity
    {

        public int Index;

        public PointLightVisualizer(int index, params Material [] material) : base(BehaviorFlags.None, material)
        {
            Index = index;
            LocalScale *= 0.2f;
        }

        public override void OnUpdate(EntityUpdateEventArgs eventArgs)
        {
            var light = Globals.PointLights[Index];
            light.Position += new Vector3(0, MathF.Sin(Globals.AbsTimeF)/20, 0);
            LocalPosition = light.Position;
        }


        public override void SendUniformsPerObject(Material material)
        {
            if (material == SetupMaterials.ShadowMapSphere || material == SetupMaterials.ShadowMapPlane)
            {
                UniformSender.SendTransformMatrices(this, material, Globals.ShadowCastingLightRoom1, "Light");
            }
            else
            { 
                UniformSender.SendTransformMatrices(this, material, Globals.MainCamera);
                UniformSender.SetVector4(material, "Color", new Vector4(Globals.PointLights[Index].Color, 1), false);
            }
            

//            UniformSender.SendLights(this);
        }
    }
}