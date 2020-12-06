using CART_457.EntitySystem;
using CART_457.Renderer;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Indpendent_Study_Fall_2020.c_sharp.Scripts.Blueprints
{
    public class ShadowLightController : Entity
    {
        public override void OnUpdate(EntityUpdateEventArgs eventArgs)
        {
            if (eventArgs.MouseState.IsButtonDown(MouseButton.Middle))
            {
                Globals.ShadowCastingLight.CopyFrom(Globals.PlayerCamera);
            }
            
            if (eventArgs.MouseState.IsButtonDown(MouseButton.Right))
            {
                Globals.ShadowDirection = Globals.PlayerCamera.Rotation * new Vector3(0,0,-1);
            }
        }
    }
}