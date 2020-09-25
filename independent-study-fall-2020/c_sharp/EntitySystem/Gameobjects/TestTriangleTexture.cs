

using System;

using OpenTK;

namespace Indpendent_Study_Fall_2020.EntitySystem.Gameobjects
{
    public class TestTriangleTexture : GameObject
    {
        public override string MaterialName => "test_mat";

        public override void SendUniformsToShader()
        { 
            Material.SetMatrix4("Rotation", Matrix4.CreateFromQuaternion(Globals.CameraRotation));
            Material.SetMatrix4("Transform", Globals.CameraPerspective);
            Material.SetVector3("CamPosition", Globals.CameraPosition);
        }
        
    }
}