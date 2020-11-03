﻿using System;
using CART_457.c_sharp.Renderer;
using CART_457.MaterialRelated;
using CART_457.Renderer;
using CART_457.Scripts;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace CART_457.EntitySystem.Scripts.Blueprints
{
    public class SinMover : EntitySystem.Entity
    {

        public Vector3 ran = Vector3.Zero;

        public override void OnLoad()
        {
            LocalScale *= 1f;
        }

        private SinMover(Material[] materials)
        {
            AssignMaterials(materials);
        }
        public SinMover(Vector3 position, Vector3 ran2, params Material [] material)
        {
            LocalPosition = position;
            AssignMaterials(material);
           ran = ran2;
        }

        public static SinMover PositionSize(Vector3 position, Vector3 size, params Material [] materials)
        {
            var sphere = new SinMover(materials);
            sphere.LocalPosition = position;
            sphere.LocalScale = size;
            return sphere;
        }
        

        public override void OnUpdate(EntityUpdateEventArgs eventArgs)
        {
//            Rotation = Quaternion.FromEulerAngles(MathF.Sin(Globals.AbsTimeF/8)*3,MathF.Cos(Globals.AbsTimeF/30)*9,Globals.AbsTimeF/20);
            var sin = new Vector3(MathF.Sin(ran.X), MathF.Sin(ran.Y)/2, MathF.Sin(ran.Z))*.01f;
            LocalPosition += sin;
            // Rotation = Rotation * Quaternion.FromAxisAngle(new Vector3(0, 1, 0), 0.01f);

        }
        

        public override void SendUniformsPerObject(Material material)
        {
            // if (material == CART_457.Scripts.InitMaterials.ShadowMapSphere || material == CART_457.Scripts.InitMaterials.ShadowMapPlane)
            // {
            //     UniformSender.SendTransformMatrices(this, material, Globals.ShadowCastingLight, "Light");
            // }
            // else
            // {
            //     UniformSender.SendTransformMatrices(this, material, Globals.MainCamera);
            //     UniformSender.SendTransformMatrices(this, material, Globals.ShadowCastingLight, "Light");
            //     UniformSender.SendLights(material);
            //     UniformSender.SendGlobals(material);
            // }

        }
        
    }
}