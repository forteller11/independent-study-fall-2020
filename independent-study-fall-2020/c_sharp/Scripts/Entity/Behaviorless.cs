﻿using Indpendent_Study_Fall_2020.MaterialRelated;
using Indpendent_Study_Fall_2020.Scripts;

namespace Indpendent_Study_Fall_2020.EntitySystem.Scripts.Gameobjects
{
    public class Behaviorless : Entity
    {
        public Behaviorless(Material [] mat)
        {
            SetupMaterials(mat);
        }
    }
}