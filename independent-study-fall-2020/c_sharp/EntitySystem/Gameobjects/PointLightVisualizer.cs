using System;
using Indpendent_Study_Fall_2020.c_sharp.Renderer;
using OpenTK;

namespace Indpendent_Study_Fall_2020.EntitySystem.Gameobjects
{
    public class PointLightVisualizer : GameObject
    {
        public override string MaterialName => "solidColor_mat";
        public int Index;

        public PointLightVisualizer(int index)
        {
            Index = index;
        }

        public override void OnUpdate(GameObjectUpdateEventArgs eventArgs)
        {
            base.OnUpdate(eventArgs);
            Position = Globals.PointLights[Index].Position;
        }


        public override void SendUniformsToShaderPerObject()
        {
            UniformSender.SendTransformMatrices(this);
            UniformSender.SetVector4(Material, "Color", new Vector4(Globals.PointLights[Index].Color,1), false);
//            UniformSender.SendLights(this);
        }
    }
}