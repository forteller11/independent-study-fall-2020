

using System;

using OpenTK;

namespace Indpendent_Study_Fall_2020.EntitySystem.Gameobjects
{
    public class TestTriangleTexture : GameObject
    {
        public override string MaterialName => "test_mat";

        public override void SendUniformsToShader()
        {
            Matrix4.CreatePerspectiveFieldOfView(MHelper.DEG_TO_RAD * 90, 1f, 0.5f, 1000f, out Matrix4 mat);
            Material.SetMatrix4("Rotation", Matrix4.CreateFromQuaternion(Globals.CameraRotation));
            Material.SetMatrix4("Transform", mat);
            Material.SetVector3("CamPosition", Globals.CameraPosition);
        }
        
    }
}