
using CART_457.EntitySystem;
using CART_457.Renderer;
using OpenTK;
using OpenTK.Mathematics;


namespace CART_457.Scripts.Blueprints
{
    public class ScreenManager : Entity
    {
        private Screen room1;
        private Screen room2;
        // ScreenManager(params  Screen[] screens)
        // {
        //     _screens = screens;
        // }

        public ScreenManager(Screen r1, Screen r2)
        {
            room1 = r1;
            room2 = r2;
        }


        
        public override void OnUpdate(EntityUpdateEventArgs eventArgs)
        {
            room1.LocalRotation = Globals.MainCamera.Rotation;
            Vector3 viewDir = Globals.MainCamera.Rotation * Vector3.UnitZ;
            room1.LocalPosition = Globals.MainCamera.Position - viewDir * 2;
        }


    }
}