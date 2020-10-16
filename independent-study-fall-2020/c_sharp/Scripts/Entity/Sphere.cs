

using System;
using Indpendent_Study_Fall_2020.c_sharp.Renderer;
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

        public Sphere(Vector3 position, params MaterialSetup.MaterialType [] materialTypes)
        {
            Position = position;
            SetupMaterials(materialTypes);
        }
        

        public override void OnUpdate(EntityUpdateEventArgs eventArgs)
        {
//            Rotation = Quaternion.FromEulerAngles(MathF.Sin(Globals.AbsTimeF/8)*3,MathF.Cos(Globals.AbsTimeF/30)*9,Globals.AbsTimeF/20);
//            Position =  new Vector3(MathF.Sin(Globals.AbsTimeF/20)*1,0,0);

        }
        

        public override void SendUniformsPerObject(Material material)
        {
            if (material.Type != MaterialSetup.MaterialType.ShadowMap)
            {
                UniformSender.SendTransformMatrices(this, material, Globals.MainCamera);
                UniformSender.SendLights(material);
            }
            else
            {
                UniformSender.SendTransformMatrices(this, material, Globals.ShadowCastingLight);
            }
        }
        
    }
}