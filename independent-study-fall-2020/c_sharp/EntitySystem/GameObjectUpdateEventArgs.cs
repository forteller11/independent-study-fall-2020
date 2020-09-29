﻿using OpenTK;
using OpenTK.Input;

namespace Indpendent_Study_Fall_2020.EntitySystem
{
    public class GameObjectUpdateEventArgs
    {
        public readonly double DeltaTime;
//        public KeyboardKeyEventArgs KeyboardKeyEventArgs;
        public readonly KeyboardState KeyboardState;
        public readonly MouseState MouseState;
        public readonly Vector2 MouseDelta;

        public GameObjectUpdateEventArgs(double deltaTime, KeyboardState keyboardState, MouseState mouseState, Vector2 mouseDelta)
        {
            DeltaTime = deltaTime;
            KeyboardState = keyboardState;
            MouseState = mouseState;
            MouseDelta = mouseDelta;
        }
    }
}