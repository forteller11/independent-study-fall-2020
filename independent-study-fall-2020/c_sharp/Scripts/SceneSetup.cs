using System;
using System.Collections.Generic;
using Indpendent_Study_Fall_2020.c_sharp.EntitySystem.Renderer;
using Indpendent_Study_Fall_2020.EntitySystem;
using Indpendent_Study_Fall_2020.EntitySystem.Scripts.Gameobjects;
using OpenTK;

namespace Indpendent_Study_Fall_2020.Scripts
{
    public class SceneSetup
    {
        public static Entity[] CreateGameObjects()
        {
            List<Entity> entities = new List<Entity>();
            
            entities.Add(new CameraControllerSingleton(CreateMaterials.MaterialName.None));
            
            entities.Add(new Sphere(CreateMaterials.MaterialName.Dirt, new Vector3(-1,0,0)));
            entities.Add(new Sphere(CreateMaterials.MaterialName.Dirt, new Vector3(0,0,0)));
            entities.Add(new Sphere(CreateMaterials.MaterialName.Tile, new Vector3(1,0,0)));


            for (int i = 0; i < Globals.PointLights.Count; i++)
                entities.Add(new PointLightVisualizer(CreateMaterials.MaterialName.Solid, i));

            return entities.ToArray();
        }

        public static void CreateGlobals()
        {
            Globals.DirectionLights.Add(new DirectionLight(new Vector3(0,1,0), new Vector3(.8f)));
//            Globals.DirectionLights.Add(new DirectionLight(new Vector3(1,0,0), new Vector3(1,0,0)));
//            Globals.DirectionLights.Add(new DirectionLight(new Vector3(0,0,1), new Vector3(0,1,1)));
            
            Globals.PointLights.Add(new PointLight(new Vector3(-2,-3f,0), new Vector3(.3f,.3f,1)));
            Globals.PointLights.Add(new PointLight(new Vector3(2,-3f,0), new Vector3(1,.3f,.3f)));
        }
    }
}