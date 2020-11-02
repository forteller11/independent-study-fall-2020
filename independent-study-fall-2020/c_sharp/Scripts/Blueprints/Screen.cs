using System.Dynamic;
using CART_457.EntitySystem;
using CART_457.MaterialRelated;
using CART_457.Renderer;
using CART_457.Scripts;

namespace Indpendent_Study_Fall_2020.c_sharp.Scripts.Blueprints
{
    public class Screen : Entity
    {
        public Screen()
        {
            AssignMaterials(SetupMaterials.Screen); 
        }


    }
}