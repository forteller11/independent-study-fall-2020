﻿using CART_457.c_sharp.Renderer;
using CART_457.EntitySystem;
using CART_457.MaterialRelated;
using CART_457.Renderer;
using CART_457.Scripts.Setups;
using OpenTK;
using OpenTK.Mathematics;

namespace CART_457.Scripts.Blueprints
{
    public class EmptySolid : Entity
    {
        public Vector4 Color;

        public EmptySolid(Vector4 color, float scale, params Material[] materials) : base(materials)
        {
            Color = color;
            LocalScale *= scale;
        }
        

        public override void SendUniformsPerEntity(Material material)
        {
            UniformSender.SetVector4(material, "Color", Color, false);
            // Debug.Log("====");
        }
    }
}