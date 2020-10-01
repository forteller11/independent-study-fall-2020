

using System;
using Indpendent_Study_Fall_2020.c_sharp.Renderer;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace Indpendent_Study_Fall_2020.EntitySystem.Gameobjects
{
    public class TestTriangleTexture : GameObject
    {
        public override string MaterialName => "normal_mat";

        public override void OnLoad()
        {
            Scale *= 1f;
        }

        public override void OnUpdate(GameObjectUpdateEventArgs eventArgs)
        {
//            Rotation = Quaternion.FromEulerAngles(MathF.Sin(Globals.AbsTimeF/8)*3,MathF.Cos(Globals.AbsTimeF/30)*9,Globals.AbsTimeF/20);
//            Position =  new Vector3(MathF.Sin(Globals.AbsTimeF/20)*1,0,0);
            
            
        }
        
        public override void SendUniformsToShaderPerObject()
        {
            UniformSender.SendTransformMatrices(this);
            UniformSender.SendLights(this);
            UniformSender.SetFloat(Material, "NormalMapStrength", 1);
        }
        
    }
}