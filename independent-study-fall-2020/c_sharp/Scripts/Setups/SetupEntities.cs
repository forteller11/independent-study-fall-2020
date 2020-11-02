using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using CART_457.EntitySystem;
using CART_457.EntitySystem.Scripts.Blueprints;
using CART_457.EntitySystem.Scripts.EntityPrefabs;
using CART_457.Renderer;
using Indpendent_Study_Fall_2020.c_sharp.Scripts.Blueprints;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace CART_457.Scripts.Setups
{
    //TODO make entity constructor automatically add entities to game world... Instant, Delayed creation CreateAndAdd vs Create
    public class SetupEntities
    {
        public static Entity[] CreateGameObjects() 
        {
            List<Entity> gameObjects = new List<Entity>();

            #region room1
            var table = Empty.FromPosition(new Vector3(0, 3, 0), SetupMaterials.TableProto, SetupMaterials.ShadowMapTable);
            table.LocalScale *= 0.2f;
            table.UpdateAction += (entity) => entity.LocalRotation *= Quaternion.FromAxisAngle(Vector3.UnitY, 0.002f);
            
            var table2 = Empty.FromPosition(new Vector3(7, 0, 0), SetupMaterials.TableProto, SetupMaterials.ShadowMapTable);
             table2.UpdateAction += (entity) => entity.LocalRotation *= Quaternion.FromAxisAngle(Vector3.UnitY, 0.004f);
             table2.LocalScale *= 0.5f;
            table2.Parent = table;
            
            var table3 = Empty.FromPosition(new Vector3(15, 0, 0), SetupMaterials.TableProto, SetupMaterials.ShadowMapTable);
            table3.UpdateAction += (entity) => entity.LocalRotation *= Quaternion.FromAxisAngle(Vector3.UnitY, 0.004f);
            table3.LocalScale *= 0.5f;
            table3.Parent = table2;
            
            gameObjects.Add(table);
            gameObjects.Add(table2);
            gameObjects.Add(table3);
            gameObjects.Add(new CameraVisualizer());
            
            gameObjects.Add(new CameraControllerSingleton());
            gameObjects.Add(new CameraInterperlator());

            gameObjects.Add(table);
            
            gameObjects.Add(new SinMover(new Vector3(-3,0,4), new Vector3(1,0.2f, -1f), SetupMaterials.DirtSphere, SetupMaterials.ShadowMapSphere));
            gameObjects.Add(new SinMover(new Vector3(0,0,0),new Vector3(-.2f, 0, -.5f), SetupMaterials.DirtSphere, SetupMaterials.ShadowMapSphere));
            gameObjects.Add(new SinMover(new Vector3(3,0,4),new Vector3(.55f, .1f, .05f), SetupMaterials.TileSphere, SetupMaterials.ShadowMapSphere));
            gameObjects.Add(new SinMover(new Vector3(6,3,4),new Vector3(.1f,-.5f,.06f), SetupMaterials.DirtSphere, SetupMaterials.ShadowMapSphere));
            gameObjects.Add(new SinMover(new Vector3(1,1,2),new Vector3(.19f,-.1f,.06f), SetupMaterials.TileSphere, SetupMaterials.ShadowMapSphere));
            gameObjects.Add(new SinMover(new Vector3(1,4,2),new Vector3(.13f,-.1f,.05f), SetupMaterials.DirtSphere, SetupMaterials.ShadowMapSphere));
            gameObjects.Add(new SinMover(new Vector3(1,7,2),new Vector3(.1f,-.1f,.50f), SetupMaterials.TileSphere, SetupMaterials.ShadowMapSphere));
            var dirtPlane01 = Empty.FromPosition(new Vector3(0, -5, 0), SetupMaterials.DirtPlane, SetupMaterials.ShadowMapPlane);
            dirtPlane01.LocalScale = new Vector3(4);
            var dirtPlane02 = Empty.FromPosition(new Vector3(0, -12, 0), SetupMaterials.DirtPlane, SetupMaterials.ShadowMapPlane);
            dirtPlane02.LocalScale = new Vector3(7);
            gameObjects.Add(dirtPlane01);
            gameObjects.Add(dirtPlane02);
            
            gameObjects.Add(new FBOVisualizationInput());

            for (int i = 0; i < Globals.PointLights.Count; i++)
                gameObjects.Add(new PointLightVisualizer(i,SetupMaterials.SolidSphereR1));
            #endregion
            
            #region room2
            gameObjects.Add(new SinMover(new Vector3(-3,1,0), Vector3.Zero, SetupMaterials.SolidSphereR2));
            gameObjects.Add(new SinMover(new Vector3(0,2,-4), Vector3.Zero, SetupMaterials.SolidSphereR2));
            gameObjects.Add(new SinMover(new Vector3(3,0,0), Vector3.Zero, SetupMaterials.SolidSphereR2));
            gameObjects.Add(new SinMover(new Vector3(-3,-1,-4), Vector3.Zero, SetupMaterials.SolidSphereR2));
            gameObjects.Add(new SinMover(new Vector3(0,-2,4), Vector3.Zero, SetupMaterials.SolidSphereR2));
            #endregion
            
            #region screen combiner
            //screen managers
            //todo, have gameobject for every screen, manage transform/quad based on state....
            {
                var s1 = new Screen(TextureUnit.Texture0);
                var s2 = new Screen(TextureUnit.Texture1);
                var screenManager = new ScreenManager(s1, s2);

                gameObjects.Add(s1); 
                gameObjects.Add(s2); 
                gameObjects.Add(screenManager);
            }

            #endregion
            
            
            
            

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