using CART_457.c_sharp.Renderer;
using CART_457.EntitySystem;
using CART_457.MaterialRelated;
using OpenTK.Mathematics;

namespace Indpendent_Study_Fall_2020.c_sharp.Scripts.Blueprints
{
    public class FrustrumNormal : Entity
    {
        public int AppearInFrustrum; //1 = true, -1 = false
        public Material FrustrumMaterial;
        public FrustrumNormal (Material[]mats): base(mats){}
        
        public static FrustrumNormal FromPositionRotationScale (bool appearInFrustrum, Vector3 position, Quaternion rotation, Vector3 scale,  Material frustrumMat, Material shadowMat)
        {
            var frustrumNormal = new FrustrumNormal(new []{frustrumMat, shadowMat});
            frustrumNormal.LocalPosition = position;
            frustrumNormal.LocalRotation = rotation;
            frustrumNormal.LocalScale = scale;
            frustrumNormal.AppearInFrustrum = appearInFrustrum ? 1 : 0;
            frustrumNormal.FrustrumMaterial = frustrumMat;
            return frustrumNormal;
        }

        public override void SendUniformsPerObject(Material material)
        {

            // UniformSender.SetFloat(FrustrumMaterial, "ShouldAppearInFrustrum", (float)AppearInFrustrum);
        }
    }
}