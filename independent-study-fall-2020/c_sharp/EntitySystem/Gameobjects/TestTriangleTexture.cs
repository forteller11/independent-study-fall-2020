﻿

using System;

using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace Indpendent_Study_Fall_2020.EntitySystem.Gameobjects
{
    public class TestTriangleTexture : GameObject
    {
        public override string MaterialName => "test_mat";
        public Vector3 Position = new Vector3(0,0,1);

        public override void SendUniformsToShader()
        {
            var worldTranslation = Matrix4.CreateTranslation(Position - Globals.CameraPosition);
            var worldRotation = Matrix4.CreateFromQuaternion(Globals.CameraRotation);

            //apparently matrix mult combines matrices as if matrix left matrix transformed THEN the right... opposite to how it works in math
            var modelToWorld = Matrix4.Mult(worldTranslation, Matrix4.Transpose(worldRotation));
            Material.SetMatrix4("ModelToWorld", modelToWorld);
            
            var worldToView = Globals.CameraPerspective;
            Material.SetMatrix4("WorldToView", worldToView);
            
//            Material.SetMatrix4("Projection", Globals.CameraPerspective);
//            Material.SetVector3("CamPosition", Globals.CameraPosition);
        }
        
    }
}