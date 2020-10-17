using System;
using CART_457.c_sharp.Renderer;
using CART_457.MaterialRelated;
using CART_457.Renderer;
using CART_457.Scripts;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace CART_457.EntitySystem.Scripts.Entity
{
    public class Sphere : EntitySystem.Entity
    {


        public override void OnLoad()
        {
            Scale *= 1f;
        }

        public Sphere(Vector3 position, params Material [] material)
        {
            Position = position;
            SetupMaterials(material);
        }
        

        public override void OnUpdate(EntityUpdateEventArgs eventArgs)
        {
//            Rotation = Quaternion.FromEulerAngles(MathF.Sin(Globals.AbsTimeF/8)*3,MathF.Cos(Globals.AbsTimeF/30)*9,Globals.AbsTimeF/20);
//            Position =  new Vector3(MathF.Sin(Globals.AbsTimeF/20)*1,0,0);

        }
        

        public override void SendUniformsPerObject(Material material)
        {
            if (material != MaterialSetup.ShadowMap)
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