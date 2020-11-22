﻿using System;
using System.Collections.Generic;
using CART_457.Blueprints;
using CART_457.EntitySystem;
using CART_457.Helpers;
using CART_457.PhysicsRelated;
using CART_457.Renderer;
using CART_457.Scripts.Blueprints;
using CART_457.Scripts.EntityPrefabs;
using CART_457Scripts.Blueprints;
using Indpendent_Study_Fall_2020.c_sharp.Scripts.Blueprints;
using OpenTK.Mathematics;

namespace CART_457.Scripts.Setups
{
    //TODO make entity constructor automatically add entities to game world... Instant, Delayed creation CreateAndAdd vs Create
    public class SetupEntities
    {
        public static void CreateGameObjects() 
        {

            #region room 1
            #region table
            var table = Empty.FromPosition(new Vector3(0, 1, 0), SetupMaterials.TableProto, SetupMaterials.ShadowMapTable);

            table.LocalScale *= 0.4f;
            table.LocalRotation = Quaternion.FromAxisAngle(Vector3.UnitY, MathF.PI);
            // table.UpdateAction += (entity) => entity.LocalRotation *= Quaternion.FromAxisAngle(Vector3.UnitY, 0.002f);
            
            var table2 = Empty.FromPosition(new Vector3(7, 2, 0), SetupMaterials.TableProto, SetupMaterials.ShadowMapTable);
             table2.UpdateAction += (entity) => entity.LocalRotation *= Quaternion.FromAxisAngle(Vector3.UnitY, 0.004f);
             table2.LocalScale *= 0.5f;
            table2.Parent = table;
            
            var table3 = Empty.FromPosition(new Vector3(15, 0, 0), SetupMaterials.TableProto, SetupMaterials.ShadowMapTable);
            table3.UpdateAction += (entity) => entity.LocalRotation *= Quaternion.FromAxisAngle(Vector3.UnitY, 0.004f);
            table3.LocalScale *= 0.5f;
            table3.Parent = table2;
            

            
            

            
            #endregion
            
            #region screen
            var screen = new Screen();
            screen.Parent = table;
            screen.LocalRotation = Quaternion.FromAxisAngle(Vector3.UnitY, MathF.PI);
            screen.LocalPosition = new Vector3(-.3f,1.2f,.8f);
            screen.LocalScale = new Vector3(.9f,.6f,1f);
            screen.VisualizeColliders();

            #endregion
  
  
            var planeFloor = FrustrumNormal.FromPositionRotationScale(true,new Vector3(0,0,0), Quaternion.FromEulerAngles(0,0,0),  new Vector3(1), SetupMaterials.DirtPlane, SetupMaterials.ShadowMapPlane);
                  // planeFloor.UpdateAction += (planeFloor) => planeFloor.LocalRotation *= Quaternion.FromEulerAngles(MathF.PI/800,0,0);
                var floor = ModelImporter.GetTrianglesFromObjFile("path", planeFloor, true);
                var plane =ModelImporter.GetTrianglesFromObjFile("Plane", planeFloor, true);
               planeFloor.AddColliders( plane);
               // var p1 = new Vector3(1,0,0);
               // var p2 = new Vector3(-1,0,0);
               // var p3 = new Vector3(0,0,1);
               // planeFloor.AddCollider(new TriangleCollider(planeFloor, true, p1,p2,p3));
  
            // var tris = ModelImporter.GetTrianglesFromObjFile("room_proto_table", table, true);
            // var tris2 = ModelImporter.GetTrianglesFromObjFile("room_proto_table", table2, true);
            // var tris3 = ModelImporter.GetTrianglesFromObjFile("room_proto_table", table3, true);
             // table.AddColliders(tris);
             // table2.AddColliders(tris2);
             // table2.AddColliders(tris3);
      
            // f.AddCollider(new SphereCollider(f, 2));
            FrustrumNormal.FromPositionRotationScale(false,new Vector3(0,-2f,4f), Quaternion.Identity,  new Vector3(5), SetupMaterials.CarpetPlaneR1Frustrum, SetupMaterials.ShadowMapPlane);
            FrustrumNormal.FromPositionRotationScale(true,new Vector3(1,-4,4), Quaternion.Identity,  new Vector3(5), SetupMaterials.DirtPlaneR1Frustrum, SetupMaterials.ShadowMapPlane);
            FrustrumNormal.FromPositionRotationScale(false,new Vector3(1,-4,4), Quaternion.Identity,  new Vector3(5), SetupMaterials.CarpetPlaneR1Frustrum, SetupMaterials.ShadowMapPlane);
            FrustrumNormal.FromPositionRotationScale(true, new Vector3(1,-4,16),Quaternion.Identity, new Vector3(6), SetupMaterials.DirtSphereR1Frustrum, SetupMaterials.ShadowMapPlane);
           
            // var dirtPlane01 = Empty.FromPosition(new Vector3(0, 0, 0), SetupMaterials.DirtPlane, SetupMaterials.ShadowMapPlane);
            // dirtPlane01.LocalScale = new Vector3(4);
            var dirtPlane02 = Empty.FromPosition(new Vector3(0, -12, 0), SetupMaterials.DirtPlaneR1Frustrum, SetupMaterials.ShadowMapPlane);
            dirtPlane02.LocalScale = new Vector3(7);

            #region camera related
            var cameraVisualizer = new CameraVisualizer();

            var camController = new CameraControllerSingleton(planeFloor.ColliderGroup, Globals.PlayerCameraRoom1);

            
            var raycastTest = new WebcamRotater(cameraVisualizer.WebCamVisualizer);
            raycastTest.Parent = camController;

            new CameraInterperlator();
            
            #endregion
            #region visualizers and debuggers
            new FBOVisualizationInput();

            new RaycastDebugger();
                
            #endregion
            for (int i = 0; i < Globals.PointLights.Count; i++)
                new PointLightVisualizer(i,SetupMaterials.SolidSphereR1);
            #endregion
            
            #region room2
            // gameObjects.Add(new SinMover(new Vector3(-3,1,0), Vector3.Zero, SetupMaterials.SolidSphereR2));
            // gameObjects.Add(new SinMover(new Vector3(0,2,4), Vector3.Zero, SetupMaterials.SolidSphereR2));
            // gameObjects.Add(new SinMover(new Vector3(3,0,0), Vector3.Zero, SetupMaterials.SolidSphereR2));
            // gameObjects.Add(new SinMover(new Vector3(-3,-1,4), Vector3.Zero, SetupMaterials.SolidSphereR2));
            // gameObjects.Add(new SinMover(new Vector3(0,-2,4), Vector3.Zero, SetupMaterials.SolidSphereR2));
            
            var head = new Wobble(.03f, 100f, new Vector3(0, 1.9f, 4), SetupMaterials.WeirdHeadR2);
            head.LocalRotation = Quaternion.FromEulerAngles(0, MathF.PI/2,0);
            #endregion
            
            #region screen combiner
            //screen managers
            //todo, have gameobject for every screen, manage transform/quad based on state....
            {
                // var s1 = new Screen(TextureUnit.Texture0);
                // var s2 = new Screen(TextureUnit.Texture1);
                // var screenManager = new ScreenManager(s1, s2);
                //
                // gameObjects.Add(s1); 
                // gameObjects.Add(s2); 
                // gameObjects.Add(screenManager);
            }

            #endregion
            
  
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