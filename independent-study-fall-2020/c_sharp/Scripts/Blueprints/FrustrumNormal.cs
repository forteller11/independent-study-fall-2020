using System;
using CART_457;
using CART_457.c_sharp.Renderer;
using CART_457.EntitySystem;
using CART_457.MaterialRelated;
using OpenTK.Mathematics;

namespace Indpendent_Study_Fall_2020.c_sharp.Scripts.Blueprints
{
    public class FrustrumNormal : Entity
    {
        public bool AppearsInFrustrum; //1 = true, -1 = false
        public Material FrustrumMaterial;
        public Action<Entity> UpdateAction;
        public FrustrumNormal (Material[]mats): base(mats){}
        
        public static FrustrumNormal FromPositionRotationScale (bool appearInFrustrum, Vector3 position, Quaternion rotation, Vector3 scale,  Material frustrumMat)
        {
            var frustrumNormal = new FrustrumNormal(new []{frustrumMat});
            frustrumNormal.LocalPosition = position;
            frustrumNormal.LocalRotation = rotation;
            frustrumNormal.LocalScale = scale;
            frustrumNormal.AppearsInFrustrum = appearInFrustrum;
            frustrumNormal.FrustrumMaterial = frustrumMat;
            return frustrumNormal;
        }

        public static FrustrumNormal FromPositionRotationScale (bool appearInFrustrum, Vector3 position, Quaternion rotation, Vector3 scale,  Material frustrumMat, Material shadowMat)
        {
            var frustrumNormal = new FrustrumNormal(new []{frustrumMat, shadowMat});
            frustrumNormal.LocalPosition = position;
            frustrumNormal.LocalRotation = rotation;
            frustrumNormal.LocalScale = scale;
            frustrumNormal.AppearsInFrustrum = appearInFrustrum;
            frustrumNormal.FrustrumMaterial = frustrumMat;
            return frustrumNormal;
        }
        

        public override void OnUpdate(EntityUpdateEventArgs eventArgs)
        {
            UpdateAction?.Invoke(this);
        }

        public override void SendUniformsPerEntity(Material material)
        {
            if (material == FrustrumMaterial)
            {
                UniformSender.SetBool(material, UniformSender.VISIBLE_IN_FRUSTRUM, AppearsInFrustrum);
            }
                
        }
    }
}