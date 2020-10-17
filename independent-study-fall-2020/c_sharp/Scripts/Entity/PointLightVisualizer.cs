using System;
using Indpendent_Study_Fall_2020.c_sharp.Renderer;
using Indpendent_Study_Fall_2020.MaterialRelated;
using Indpendent_Study_Fall_2020.Scripts;
using OpenTK;

namespace Indpendent_Study_Fall_2020.EntitySystem.Scripts.Gameobjects
{
    public class PointLightVisualizer : Entity
    {

        public int Index;

        public PointLightVisualizer(int index, params Material [] material) : base(BehaviorFlags.None, material)
        {
            Index = index;
            Scale *= 0.2f;
        }

        public override void OnUpdate(EntityUpdateEventArgs eventArgs)
        {
            var light = Globals.PointLights[Index];
            light.Position += new Vector3(0, MathF.Sin(Globals.AbsTimeF)/20, 0);
            Position = light.Position;
        }


        public override void SendUniformsPerObject(Material material)
        {
            if (material == MaterialSetup.ShadowMap)
            {
                UniformSender.SendTransformMatrices(this, material, Globals.ShadowCastingLight);
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