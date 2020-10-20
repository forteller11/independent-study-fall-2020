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
            // Position += new Vector3(0,MathF.Sin(Globals.AbsTimeF/2)*0.1f,0);

        }
        

        public override void SendUniformsPerObject(Material material)
        {
            if (material == MaterialSetup.ShadowMapSphere || material == MaterialSetup.ShadowMapPlane)
            {
                UniformSender.SendTransformMatrices(this, material, Globals.ShadowCastingLight, "Light");
            }
            else
            { 
                UniformSender.SendTransformMatrices(this, material, Globals.MainCamera);
                UniformSender.SendTransformMatrices(this, material, Globals.ShadowCastingLight, "Light");
                UniformSender.SendLights(material);
            }

        }
        
    }
}