using System.Collections.Generic;

namespace Indpendent_Study_Fall_2020.EntitySystem
{
    public class GameObjectManager
    {
        private List<GameObject> _gameObjects;

        public void Add(params GameObject[] gameObjects)
        {
            for (int i = 0; i < gameObjects.Length; i++)
                Add(gameObjects[i]);
        }

        public void Add(GameObject gameObject)
        {
            _gameObjects.Add(gameObject);
            Globals.DrawManager.UseMaterial(gameObject, gameObject.MaterialName);
            //todo automatically add to draw manager
        }

        public void Remove(GameObject gameObject) => _gameObjects.Remove(gameObject);

        void LoadAllGameObjects()
        {
            for (int i = 0; i < _gameObjects.Count; i++)
                _gameObjects[i].OnLoad();
        }
        
        void UpdateAllGameObjects(GameObjectUpdateEventArgs eventArgs)
        {
            for (int i = 0; i < _gameObjects.Count; i++)
                _gameObjects[i].OnUpdate(eventArgs);
        }
        

    }
}