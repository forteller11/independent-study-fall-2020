

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
            Position = new Vector3(0,0,1);
            Scale *= 0.5f;
        }

        public override void OnUpdate(GameObjectUpdateEventArgs eventArgs)
        {
            base.OnUpdate(eventArgs);
            Rotation = Quaternion.FromEulerAngles((float)Globals.AbsoluteTime,(float)Globals.AbsoluteTime/3,0);
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