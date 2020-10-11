﻿using Indpendent_Study_Fall_2020.c_sharp.Renderer;
using Indpendent_Study_Fall_2020.MaterialRelated;
using Indpendent_Study_Fall_2020.Scripts;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace Indpendent_Study_Fall_2020.EntitySystem.Scripts.Gameobjects
{
    public class Sphere : Entity
    {
        
        public override void OnLoad()
        {
            Scale *= 1f;
        }

        public Sphere(CreateMaterials.MaterialName materialName, Vector3 position) : base(materialName)
        {
            Position = position;
        }

        public override void OnUpdate(EntityUpdateEventArgs eventArgs)
        {
//            Rotation = Quaternion.FromEulerAngles(MathF.Sin(Globals.AbsTimeF/8)*3,MathF.Cos(Globals.AbsTimeF/30)*9,Globals.AbsTimeF/20);
//            Position =  new Vector3(MathF.Sin(Globals.AbsTimeF/20)*1,0,0);
            
            
        }

        // public override void SendUniformsPerEntityType()
        // {
        //     UniformSender.SetFloat(Material, "NormalMapStrength", 2);
        //     UniformSender.SetFloat(Material, "SpecularRoughness", 16);
        // }

        public override void SendUniformsPerEntity(Material material)
        {
            UniformSender.SetFloat(material, "NormalMapStrength", 2);
            UniformSender.SetFloat(material, "SpecularRoughness", 16);
            
            UniformSender.SendTransformMatrices(this, material);
            UniformSender.SendLights(this, material);
        }
        
    }
}