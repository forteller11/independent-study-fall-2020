using System;
using CART_457.EntitySystem;
using CART_457.Renderer;
using FbxSharp;
using OpenTK;
using Vector3 = OpenTK.Vector3;

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

        public override void OnUpdate(EntityUpdateEventArgs eventArgs)
        {

         room1.LocalScale = new Vector3(4);
         
          room2.LocalRotation = Quaternion.FromAxisAngle(Vector3.UnitY, MathF.PI);
        }
    }
}