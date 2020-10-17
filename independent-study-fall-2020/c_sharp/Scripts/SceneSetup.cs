using System;
using System.Collections.Generic;
using CART_457.EntitySystem;
using CART_457.EntitySystem.Scripts.Entity;
using CART_457.Renderer;
using OpenTK;

namespace CART_457.Scripts
{
    public class SceneSetup
    {
        public static Entity[] CreateGameObjects()
        {
            List<Entity> gameObjects = new List<Entity>();
            
            gameObjects.Add(new CameraControllerSingleton());
            
            gameObjects.Add(new Sphere(new Vector3(-3,0,4),MaterialSetup.DirtSphere, MaterialSetup.ShadowMap));
            gameObjects.Add(new Sphere(new Vector3(0,0,0),MaterialSetup.DirtSphere, MaterialSetup.ShadowMap));
            gameObjects.Add(new Sphere(new Vector3(3,0,4),MaterialSetup.TileSphere, MaterialSetup.ShadowMap));
            var s = new Sphere(new Vector3(0, -5, 0), MaterialSetup.DirtPlane);
            s.Scale = new Vector3(5);
            gameObjects.Add(s);
            
            gameObjects.Add(new FBOVisualizationInput());

            for (int i = 0; i < Globals.PointLights.Count; i++)
                gameObjects.Add(new PointLightVisualizer(i,MaterialSetup.SolidSphere, MaterialSetup.ShadowMap));

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