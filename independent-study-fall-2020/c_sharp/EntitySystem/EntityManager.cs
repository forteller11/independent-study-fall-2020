using System.Collections.Generic;
using CART_457.Renderer;
using OpenTK;
using OpenTK.Input;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace CART_457.EntitySystem
{
    public static class EntityManager
    {
        private static List<Entity> _gameObjects = new List<Entity>();
        public static EntityUpdateEventArgs EntityUpdateEventArgs { get; } = new EntityUpdateEventArgs();

        public static void AddRangeToWorldAndRenderer(params Entity[] gameObjects)
        {
            for (int i = 0; i < gameObjects.Length; i++)
                AddToWorldAndRenderer(gameObjects[i]);
        }

        /// <summary>
        /// must be called after setupStaticHierarchy on draw manager has been called
        /// </summary>
        /// <param name="entity"></param>
        public static Entity AddToWorldAndRenderer(Entity entity)
        {
            _gameObjects.Add(entity);
            DrawManager.AddEntity(entity);
            return entity;
        }

        // public void Remove(Entity entity) => _gameObjects.Remove(entity);

        public static void InvokeOnLoad()
        {
            for (int i = 0; i < _gameObjects.Count; i++)
                _gameObjects[i].OnLoad();
        }

        public static void RefreshUpdateEventArgs(GameWindow gameWindow, FrameEventArgs eventArgs)
        {
    
            var  m = gameWindow.MouseState;
            var keyboardState = gameWindow.KeyboardState;
            EntityUpdateEventArgs.DeltaTime = eventArgs.Time;
            EntityUpdateEventArgs.KeyboardState =  keyboardState;
            EntityUpdateEventArgs.MouseState = m;
            EntityUpdateEventArgs.MouseDelta = new Vector2(m.X-Globals.MousePositionLastFrame.X,-m.Y+Globals.MousePositionLastFrame.Y);
            EntityUpdateEventArgs.InputState.Update(keyboardState);
        }
        public static void  InvokeOnUpdate()
        {
            for (int i = 0; i < _gameObjects.Count; i++)
                _gameObjects[i].OnUpdate(EntityUpdateEventArgs);
        }
        

    }
}