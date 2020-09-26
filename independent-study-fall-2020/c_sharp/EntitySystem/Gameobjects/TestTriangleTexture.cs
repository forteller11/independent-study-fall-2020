

using System;

using OpenTK;

namespace Indpendent_Study_Fall_2020.EntitySystem.Gameobjects
{
    public class TestTriangleTexture : GameObject
    {
        public override string MaterialName => "test_mat";

        public override void SendUniformsToShader()
        {
            var worldTranslation = Matrix4.CreateTranslation(Globals.CameraPosition);
            var worldRotation = Matrix4.CreateFromQuaternion(Globals.CameraRotation);
            var modelToWorld = worldRotation * worldTranslation;

            var worldToView = Globals.CameraPerspective;
            
            Material.SetMatrix4("ModelToWorld", modelToWorld);
            Material.SetMatrix4("WorldToView", worldToView);
//            Material.SetVector3("CamPosition", Globals.CameraPosition);
        }
        
    }
}