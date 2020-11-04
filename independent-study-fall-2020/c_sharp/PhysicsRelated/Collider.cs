﻿using CART_457.EntitySystem;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace CART_457.PhysicsRelated
{
    public abstract class Collider
    {
        public Entity Entity { get; protected set; }

        public Collider(Entity entity)
        {
            Entity = entity;
        }
    }
}