using System.Collections.Generic;
using Indpendent_Study_Fall_2020.EntitySystem;
using Indpendent_Study_Fall_2020.EntitySystem.Gameobjects;

namespace Indpendent_Study_Fall_2020.Scripts
{
    public class CreateGameObjects
    {
        public static GameObject[] Create()
        {
            List<GameObject> gameObjects = new List<GameObject>();
            
            gameObjects.Add(new CameraControllerSingleton());
            gameObjects.Add(new TestTriangleTexture());

            for (int i = 0; i < Globals.PointLights.Count; i++)
                gameObjects.Add(new PointLightVisualizer(i));

            return gameObjects.ToArray();
        }   
    }
}