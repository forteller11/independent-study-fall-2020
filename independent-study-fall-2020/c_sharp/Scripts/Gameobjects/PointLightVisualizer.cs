using System;
using Indpendent_Study_Fall_2020.c_sharp.Renderer;
using OpenTK;

namespace Indpendent_Study_Fall_2020.EntitySystem.Scripts.Gameobjects
{
    public class PointLightVisualizer : Entity
    {

        public int Index;

        public PointLightVisualizer(string materialName, int index) : base(materialName)
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


        public override void SendUniformsPerObject()
        {
            UniformSender.SendTransformMatrices(this);
            UniformSender.SetVector4(Material, "Color", new Vector4(Globals.PointLights[Index].Color,1), false);
//            UniformSender.SendLights(this);
        }
    }
}