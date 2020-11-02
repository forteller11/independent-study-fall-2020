
using CART_457.EntitySystem;
using OpenTK;
using OpenTK.Mathematics;


namespace Indpendent_Study_Fall_2020.c_sharp.Scripts.Blueprints
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

        public override void OnLoad()
        {
            room1.LocalScale = new Vector3(4);
         
            room2.LocalPosition += Vector3.One;
        }


    }
}