using System.Collections.Generic;

namespace Indpendent_Study_Fall_2020.EntitySystem
{
    public class EntityManager
    {
        private List<Entity> _gameObjects;

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
            Globals.DrawManager.UseMaterial(entity, entity.MaterialName);
        }

        public void Remove(Entity entity) => _gameObjects.Remove(entity);

        public void InvokeOnLoad()
        {
            for (int i = 0; i < _gameObjects.Count; i++)
                _gameObjects[i].OnLoad();
        }
        
        public void  InvokeOnUpdate(EntityUpdateEventArgs eventArgs)
        {
            for (int i = 0; i < _gameObjects.Count; i++)
                _gameObjects[i].OnUpdate(eventArgs);
        }
        

    }
}