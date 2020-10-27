﻿using System;
using System.Collections.Generic;
using CART_457.EntitySystem;
using CART_457.EntitySystem.Scripts.EntityPrefab;
using CART_457.EntitySystem.Scripts.EntityPrefabs;
using CART_457.Renderer;
using OpenTK;

namespace CART_457.Scripts
{
    public class SetupEntitys
    {
        public static Entity[] CreateGameObjects() 
        {
            List<Entity> gameObjects = new List<Entity>();
            
            gameObjects.Add(new CameraVisualizer());
            
            gameObjects.Add(new CameraControllerSingleton());

            gameObjects.Add(Empty.FromPosition(new Vector3(0,0,0), InitMaterials.TableProto, InitMaterials.ShadowMapTable));
            
            gameObjects.Add(new Sphere(new Vector3(-3,0,4), new Vector3(1,0.2f, -1f), InitMaterials.DirtSphere, InitMaterials.ShadowMapSphere));
            gameObjects.Add(new Sphere(new Vector3(0,0,0),new Vector3(-.2f, 0, -.5f), InitMaterials.DirtSphere, InitMaterials.ShadowMapSphere));
            gameObjects.Add(new Sphere(new Vector3(3,0,4),new Vector3(.55f, .1f, .05f), InitMaterials.TileSphere, InitMaterials.ShadowMapSphere));
            gameObjects.Add(new Sphere(new Vector3(6,3,4),new Vector3(.1f,-.5f,.06f), InitMaterials.DirtSphere, InitMaterials.ShadowMapSphere));
            gameObjects.Add(new Sphere(new Vector3(1,1,2),new Vector3(.19f,-.1f,.06f), InitMaterials.TileSphere, InitMaterials.ShadowMapSphere));
            gameObjects.Add(new Sphere(new Vector3(1,4,2),new Vector3(.13f,-.1f,.05f), InitMaterials.DirtSphere, InitMaterials.ShadowMapSphere));
            gameObjects.Add(new Sphere(new Vector3(1,7,2),new Vector3(.1f,-.1f,.50f), InitMaterials.TileSphere, InitMaterials.ShadowMapSphere));
            var s = Empty.FromPosition(new Vector3(0, -5, 0), InitMaterials.DirtPlane, InitMaterials.ShadowMapPlane);
            s.Scale = new Vector3(4);
            var s2 = Empty.FromPosition(new Vector3(0, -12, 0), InitMaterials.DirtPlane, InitMaterials.ShadowMapPlane);
            s2.Scale = new Vector3(7);
            gameObjects.Add(s);
            gameObjects.Add(s2);
            
            gameObjects.Add(new FBOVisualizationInput());

            for (int i = 0; i < Globals.PointLights.Count; i++)
                gameObjects.Add(new PointLightVisualizer(i,InitMaterials.SolidSphere));

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