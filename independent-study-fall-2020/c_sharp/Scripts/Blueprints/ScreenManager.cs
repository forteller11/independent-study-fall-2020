using CART_457.EntitySystem;

namespace Indpendent_Study_Fall_2020.c_sharp.Scripts.Blueprints
{
    public class ScreenManager : Entity
    {
        private Screen[] _screens;
        ScreenManager(params  Screen[] screens)
        {
            _screens = screens;
        }

        public override void OnUpdate(EntityUpdateEventArgs eventArgs)
        {
            //do the state management stuff?
        }
    }
}