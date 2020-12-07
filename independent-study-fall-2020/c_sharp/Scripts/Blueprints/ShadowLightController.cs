using CART_457.EntitySystem;
using CART_457.Renderer;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Indpendent_Study_Fall_2020.c_sharp.Scripts.Blueprints
{
    public class ShadowLightController : Entity
    {
        private float sensitivity = 0.008f;
        private float rotation = 0.08f;
        public override void OnUpdate(EntityUpdateEventArgs eventArgs)
        {
            Globals.ShadowCastingLight.Position = Vector3.Lerp(Globals.ShadowCastingLight.Position, Globals.PlayerCamera.Position,sensitivity);
            Globals.ShadowCastingLight.Rotation = Quaternion.Slerp(Globals.ShadowCastingLight.Rotation, Globals.PlayerCamera.Rotation, rotation);
            Globals.PointLights[0].Position = Vector3.Lerp( Globals.PointLights[0].Position, Globals.PlayerCamera.Position,rotation);
            var targetVec = Globals.PlayerCamera.Rotation * new Vector3(0,0,-1);
            Globals.ShadowDirection = Vector3.Lerp( Globals.ShadowDirection, targetVec,sensitivity);
        
            
        }
    }
}