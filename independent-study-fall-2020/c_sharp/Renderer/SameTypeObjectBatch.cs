using System;
using System.Collections.Generic;
using Indpendent_Study_Fall_2020.EntitySystem;

namespace Indpendent_Study_Fall_2020.c_sharp.Renderer
{
    public class SameTypeEntityBatch
    {
        public Type EntityType;
        public List<Entity> Entities;
        public void SetGLStates()
        {
            if (Entities.Count > 0)
                Entities[0].SendUniformsPerEntityType();

        }
    }
}