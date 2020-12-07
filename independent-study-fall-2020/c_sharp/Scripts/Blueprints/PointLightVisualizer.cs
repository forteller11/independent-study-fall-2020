using System;
using CART_457.c_sharp.Renderer;
using CART_457.EntitySystem;
using CART_457.MaterialRelated;
using CART_457.Renderer;
using CART_457.Scripts.Setups;
using OpenTK;
using OpenTK.Mathematics;

namespace CART_457.Blueprints
{
    public class PointLightVisualizer : Entity
    {

        public int Index;

        public PointLightVisualizer(int index, params Material [] material) : base( material)
        {
            Index = index;

        }

        public override void OnUpdate(EntityUpdateEventArgs eventArgs)
        {
             var light = Globals.PointLights[Index];
             //light.Position += new Vector3(0, MathF.Sin(Globals.AbsTimeF)/20, 0);
             LocalPosition = light.Position;
        }


        public override void SendUniformsPerEntity(Material material)
        {

                UniformSender.SendTransformMatrices(this, material, Globals.MainCamera);
                UniformSender.SetVector4(material, "Color", new Vector4(Globals.PointLights[Index].Color, 1), false);

        }
    }
}