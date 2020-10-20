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
            
            gameObjects.Add(new Sphere(new Vector3(-3,0,4), new Vector3(1,0.2f, -1f), MaterialSetup.DirtSphere, MaterialSetup.ShadowMapSphere));
            gameObjects.Add(new Sphere(new Vector3(0,0,0),new Vector3(-.2f, 0, -.5f), MaterialSetup.DirtSphere, MaterialSetup.ShadowMapSphere));
            gameObjects.Add(new Sphere(new Vector3(3,0,4),new Vector3(.55f, .1f, .6f), MaterialSetup.TileSphere, MaterialSetup.ShadowMapSphere));
            gameObjects.Add(new Sphere(new Vector3(6,3,4),new Vector3(.1f,-.5f,.456f), MaterialSetup.DirtSphere, MaterialSetup.ShadowMapSphere));
            gameObjects.Add(new Sphere(new Vector3(1,1,2),new Vector3(.19f,-.1f,.56f), MaterialSetup.TileSphere, MaterialSetup.ShadowMapSphere));
            gameObjects.Add(new Sphere(new Vector3(1,4,2),new Vector3(.13f,-.1f,.45f), MaterialSetup.DirtSphere, MaterialSetup.ShadowMapSphere));
            gameObjects.Add(new Sphere(new Vector3(1,7,2),new Vector3(.1f,-.1f,.50f), MaterialSetup.TileSphere, MaterialSetup.ShadowMapSphere));
            var s = new Sphere(new Vector3(0, -5, 0), Vector3.Zero, MaterialSetup.DirtPlane, MaterialSetup.ShadowMapPlane);
            s.Scale = new Vector3(15);
            gameObjects.Add(s);
            
            gameObjects.Add(new FBOVisualizationInput());

            for (int i = 0; i < Globals.PointLights.Count; i++)
                gameObjects.Add(new PointLightVisualizer(i,MaterialSetup.SolidSphere, MaterialSetup.ShadowMapSphere));

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