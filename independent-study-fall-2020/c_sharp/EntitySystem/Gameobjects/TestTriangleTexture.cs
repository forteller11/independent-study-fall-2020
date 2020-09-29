

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
            Rotation = Quaternion.FromEulerAngles(0,0,Globals.AbsTimeF);
            Position = Globals.CameraPosition + new Vector3(0,0,-3);
            
            
        }
        
        public override void SendUniformsToShaderPerObject()
        {
            UniformSender.SendTransformMatrices(this);
            UniformSender.SendLights(this);
        }
        
    }
}