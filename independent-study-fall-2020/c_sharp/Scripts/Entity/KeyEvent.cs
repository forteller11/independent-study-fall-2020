using System;
using OpenTK.Input;

namespace CART_457.EntitySystem.Scripts.EntityPrefab
{
    public class KeyEvent
    {
        public Key Key;
        private int _pressedFramesCount;
        public Action OnPressed;
        public Action OnReleased;
        public Action OnHeldDown;
        public bool IsToggled { get; private set; } = false;
        
        public KeyEvent(Key key)
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
                    IsToggled = !IsToggled;
                }
                else
                {
                    _pressedFramesCount++;
                    OnHeldDown?.Invoke();
                }
            }

            else
            {
                if (_pressedFramesCount > 0)
                {
                    _pressedFramesCount = -1;
                    OnReleased?.Invoke();
                }
                else
                {
                    _pressedFramesCount--;
                }
            }
        }
    }
}