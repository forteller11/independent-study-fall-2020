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

        public Vector3 ran;

        public override void OnLoad()
        {
            Scale *= 1f;
        }

        public Sphere(Vector3 position, Vector3 ran2, params Material [] material)
        {
            Position = position;
            SetupMaterials(material);
           ran = ran2;
        }
        

        public override void OnUpdate(EntityUpdateEventArgs eventArgs)
        {
//            Rotation = Quaternion.FromEulerAngles(MathF.Sin(Globals.AbsTimeF/8)*3,MathF.Cos(Globals.AbsTimeF/30)*9,Globals.AbsTimeF/20);
var sin = new Vector3(MathF.Sin(ran.X), MathF.Sin(ran.Y)/2, MathF.Sin(ran.Z))*.01f;
Position += sin;

        }
        

        public override void SendUniformsPerObject(Material material)
        {
            if (material == MaterialSetup.ShadowMapSphere || material == MaterialSetup.ShadowMapPlane)
            {
                UniformSender.SendTransformMatrices(this, material, Globals.ShadowCastingLight, "Light");
            }
            else
            {
                float t = (MathF.Cos(Globals.AbsTimeF*1f)/2) + .5f;

                t = 0;
                // Debug.Log(t);
                var camResult = new Camera();
                Camera.Lerp(Globals.MainCamera, Globals.ShadowCastingLight, t, ref camResult);
                
                UniformSender.SendTransformMatrices(this, material, camResult);
                UniformSender.SendTransformMatrices(this, material, Globals.ShadowCastingLight, "Light");
                UniformSender.SendLights(material);
                UniformSender.SendTime(material);
            }

        }
        
    }
}