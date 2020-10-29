using System;
using OpenTK.Input;

namespace CART_457.EntitySystem.Scripts.EntityPrefab
{
    public struct KeyState
    {
        public Key Key;
        private int _pressedFramesCount;
        public Action OnPressed;
        public Action OnReleased;
        public Action OnHeldDown;

        public bool IsPressed { get; private set; }
        public bool IsReleased { get; private set; }
        public bool IsToggled { get; private set; }
        public bool IsHeldDown { get; private set; }
        
        public KeyState(Key key) : this()
        {
            Key = key;
        }

        public void Update(KeyboardState state)
        {
            if (state.IsKeyDown(Key))
            {
                if (_pressedFramesCount < 0)
                {
                    _pressedFramesCount = 1;
                    
                    OnPressed?.Invoke();
                    
                    IsPressed = true;
                    IsHeldDown = true;
                    IsToggled = !IsToggled;
                }
                else
                {
                    _pressedFramesCount++;
                    
                    OnHeldDown?.Invoke();
                    
                    IsPressed = false;
                }
            }

            else //if not held down
            {
                if (_pressedFramesCount > 0)
                {
                    _pressedFramesCount = -1;
                    OnReleased?.Invoke();
                    IsReleased = true;
                    IsHeldDown = false;
                }
                else
                {
                    _pressedFramesCount--;

                    IsReleased = false;
                }
            }
        }
    }
}