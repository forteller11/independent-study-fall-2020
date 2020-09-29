

using System;
using Indpendent_Study_Fall_2020.c_sharp.Renderer;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace Indpendent_Study_Fall_2020.EntitySystem.Gameobjects
{
    public class TestTriangleTexture : GameObject
    {
        public override string MaterialName => "test_mat";

        public override void OnLoad()
        {
            Scale *= 0.5f;
        }

        public override void OnUpdate(GameObjectUpdateEventArgs eventArgs)
        {
            base.OnUpdate(eventArgs);
            Rotation = Quaternion.FromEulerAngles(Globals.AbsTimeF,Globals.AbsTimeF/3,MathF.Sin(Globals.AbsTimeF/4));
            Position = new Vector3(MathF.Sin(Globals.AbsTimeF/1)*4, 0, MathF.Sin(Globals.AbsTimeF/3)*3);
        }

        /// <summary>
        /// Sends "ModelToWorld" and "WorldToView" uniforms to shader
        /// </summary>
        public override void SendUniformsToShaderPerObject()
        {
            UniformSender.SendTransformMatrices(this);
            UniformSender.SendLights(this);
        }
        
    }
}