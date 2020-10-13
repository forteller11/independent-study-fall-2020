using System.Collections.Generic;
using OpenTK;
using OpenTK.Input;

namespace Indpendent_Study_Fall_2020.EntitySystem
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
        public static void AddToWorldAndRenderer(Entity entity)
        {
            _gameObjects.Add(entity);
            Globals.DrawManager.AddEntity(entity);
        }

        // public void Remove(Entity entity) => _gameObjects.Remove(entity);

        public static void InvokeOnLoad()
        {
            for (int i = 0; i < _gameObjects.Count; i++)
                _gameObjects[i].OnLoad();
        }

        public static void RefreshUpdateEventArgs(FrameEventArgs eventArgs)
        {
            var  m = Mouse.GetState(); 
            EntityUpdateEventArgs.DeltaTime = eventArgs.Time;
            EntityUpdateEventArgs.KeyboardState =  Keyboard.GetState();
            EntityUpdateEventArgs.MouseState = m;
            EntityUpdateEventArgs.MouseDelta = new Vector2(m.X-Globals.MousePositionLastFrame.X,-m.Y+Globals.MousePositionLastFrame.Y);
        }
        public static void  InvokeOnUpdate()
        {
            for (int i = 0; i < _gameObjects.Count; i++)
                _gameObjects[i].OnUpdate(EntityUpdateEventArgs);
        }
        

    }
}