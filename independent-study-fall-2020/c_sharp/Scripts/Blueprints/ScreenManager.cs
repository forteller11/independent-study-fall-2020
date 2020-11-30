
using CART_457.EntitySystem;
using CART_457.Renderer;
using OpenTK;
using OpenTK.Mathematics;


namespace CART_457.Scripts.Blueprints
{
    public class ScreenManager : Entity
    {


        public static Camera Target;
        // ScreenManager(params  Screen[] screens)
        // {
        //     _screens = screens;
        // }

        public ScreenManager(Camera initialTarget)
        {
            Target = initialTarget;
        }

        public static void SetTarget(Camera camera)
        {
            Target = camera;
        }
        
        public override void OnUpdate(EntityUpdateEventArgs eventArgs)
        {
            Globals.MainCamera.CopyFrom(Target);
        }


    }
}