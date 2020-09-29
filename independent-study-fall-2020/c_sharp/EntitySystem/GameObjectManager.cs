﻿using System.Collections.Generic;

namespace Indpendent_Study_Fall_2020.EntitySystem
{
    public class GameObjectManager
    {
        private List<GameObject> _gameObjects;

        public GameObjectManager()
        {
            _gameObjects = new List<GameObject>();
        }
        public void AddRange(params GameObject[] gameObjects)
        {
            for (int i = 0; i < gameObjects.Length; i++)
                Add(gameObjects[i]);
        }

        public void Add(GameObject gameObject)
        {
            _gameObjects.Add(gameObject);
            Globals.DrawManager.UseMaterial(gameObject, gameObject.MaterialName);
        }

        public void Remove(GameObject gameObject) => _gameObjects.Remove(gameObject);

        public void InvokeOnLoad()
        {
            for (int i = 0; i < _gameObjects.Count; i++)
                _gameObjects[i].OnLoad();
        }
        
        public void  InvokeOnUpdate(GameObjectUpdateEventArgs eventArgs)
        {
            for (int i = 0; i < _gameObjects.Count; i++)
                _gameObjects[i].OnUpdate(eventArgs);
        }
        

    }
}