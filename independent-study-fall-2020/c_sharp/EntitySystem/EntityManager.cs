using System.Collections.Generic;
using OpenTK;
using OpenTK.Input;

namespace Indpendent_Study_Fall_2020.EntitySystem
{
    public class EntityManager
    {
        private List<Entity> _gameObjects;
        public EntityUpdateEventArgs UpdateEventArgs;
        
        public EntityManager()
        {
            _gameObjects = new List<Entity>();
        }
        public void AddRange(params Entity[] gameObjects)
        {
            for (int i = 0; i < gameObjects.Length; i++)
                Add(gameObjects[i]);
        }

        public void Add(Entity entity)
        {
            _gameObjects.Add(entity);
            Globals.DrawManager.AddEntity(entity);
        }
        

        public void InvokeOnLoad()
        {
            for (int i = 0; i < _gameObjects.Count; i++)
                _gameObjects[i].OnLoad();
        }

        public void RefreshUpdateEventArgs(FrameEventArgs e)
        {
            UpdateEventArgs.DeltaTime = e.Time;
            UpdateEventArgs.KeyboardState = Keyboard.GetState();
            UpdateEventArgs.MouseState = Mouse.GetState();
            UpdateEventArgs.MouseDelta = new Vector2( 
                UpdateEventArgs.MouseState.X - Globals.MousePositionLastFrame.X,
                - UpdateEventArgs.MouseState.Y + Globals.MousePositionLastFrame.Y);
        }
        
        public void  InvokeOnUpdate()
        {
            for (int i = 0; i < _gameObjects.Count; i++)
                _gameObjects[i].OnUpdate(UpdateEventArgs);
        }
        

    }
}