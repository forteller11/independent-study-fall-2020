

using System;
using Indpendent_Study_Fall_2020.c_sharp.Renderer;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace Indpendent_Study_Fall_2020.EntitySystem.Gameobjects
{
    public class TestTriangleTexture : GameObject
    {
        public override string MaterialName => "shaded_mat";

        public override void OnLoad()
        {
            Scale *= 1f;
        }

        public override void OnUpdate(GameObjectUpdateEventArgs eventArgs)
        {
            Rotation = Quaternion.FromEulerAngles(MathF.Sin(Globals.AbsTimeF/2)*3,MathF.Cos(Globals.AbsTimeF/7)*9,Globals.AbsTimeF);
            Position =  new Vector3(MathF.Sin(Globals.AbsTimeF/6)*2,0,0);
            
            
        }
        
        public override void SendUniformsToShaderPerObject()
        {
            UniformSender.SendTransformMatrices(this);
            UniformSender.SendLights(this);
        }
        
    }
}