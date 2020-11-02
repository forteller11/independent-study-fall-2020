using System;
using OpenTK.Input;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace CART_457.EntitySystem.Scripts.Blueprints
{
    public struct KeyState
    {
        private Keys _key;
        private int _pressedFramesCount;
        public event Action OnPressed;
        public event Action OnReleased;
        public event Action OnHeldDown;

        public bool IsPressed { get; private set; }
        public bool IsReleased { get; private set; }
        public bool IsToggled { get; private set; }
        public bool IsHeldDown { get; private set; }
        
        public KeyState(Keys key) : this()
        {
            _key = key;
        }

        public void Update(KeyboardState state)
        {
            if (state.IsKeyDown(_key))
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