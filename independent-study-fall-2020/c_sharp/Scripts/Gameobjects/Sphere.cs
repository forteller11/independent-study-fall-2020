

using System;
using Indpendent_Study_Fall_2020.c_sharp.Renderer;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace Indpendent_Study_Fall_2020.EntitySystem.Scripts.Gameobjects
{
    public class Sphere : GameObject
    {


        public override void OnLoad()
        {
            Scale *= 1f;
        }

        public Sphere(string materialName, Vector3 position) : base(materialName)
        {
            Position = position;
        }

        public override void OnUpdate(GameObjectUpdateEventArgs eventArgs)
        {
//            Rotation = Quaternion.FromEulerAngles(MathF.Sin(Globals.AbsTimeF/8)*3,MathF.Cos(Globals.AbsTimeF/30)*9,Globals.AbsTimeF/20);
//            Position =  new Vector3(MathF.Sin(Globals.AbsTimeF/20)*1,0,0);
            
            
        }

        public override void SendUniformsPerMaterial()
        {
            UniformSender.SetFloat(Material, "NormalMapStrength", 2);
            UniformSender.SetFloat(Material, "SpecularRoughness", 16);
        }

        public override void SendUniformsPerObject()
        {
            UniformSender.SendTransformMatrices(this);
            UniformSender.SendLights(this);
        }
        
    }
}