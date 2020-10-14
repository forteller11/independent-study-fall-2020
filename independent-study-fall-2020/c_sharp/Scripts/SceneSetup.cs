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
            List<Entity> gameObjects = new List<Entity>();
            
            gameObjects.Add(new CameraControllerSingleton(CreateMaterials.MaterialType.None));
            
            gameObjects.Add(new Sphere(CreateMaterials.MaterialType.Dirt, new Vector3(-3,0,4)));
            gameObjects.Add(new Sphere(CreateMaterials.MaterialType.Dirt, new Vector3(0,0,0)));
            gameObjects.Add(new Sphere(CreateMaterials.MaterialType.Tile, new Vector3(3,0,4)));
            
            gameObjects.Add(new Sphere(CreateMaterials.MaterialType.Tile, new Vector3(0, 2, 1)));
            gameObjects.Add(new Sphere(CreateMaterials.MaterialType.DirtPlane, new Vector3(0, -10, 0), new Vector3(10,10,10)));
            
            gameObjects.Add(new DebugTriggerer());
            
            


            for (int i = 0; i < Globals.PointLights.Count; i++)
                gameObjects.Add(new PointLightVisualizer(CreateMaterials.MaterialType.Solid, i));

            return gameObjects.ToArray();
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