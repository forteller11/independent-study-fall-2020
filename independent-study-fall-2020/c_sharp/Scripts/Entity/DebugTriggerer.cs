using Indpendent_Study_Fall_2020.Scripts;
using OpenTK.Input;

namespace Indpendent_Study_Fall_2020.EntitySystem.Scripts.Gameobjects
{
    public class DebugTriggerer : Entity
    {
        private bool _keyDownLastFrame = false;

        public DebugTriggerer(MaterialFactory.MaterialType [] materialTypes=null, BehaviorFlags flags=BehaviorFlags.None) : base(flags, materialTypes)
        {
        }
        

        public override void OnUpdate(EntityUpdateEventArgs eventArgs)
        {
            bool keyStateDownThisFrame = eventArgs.KeyboardState.IsKeyDown(Key.F);
            if (_keyDownLastFrame == false && keyStateDownThisFrame == true)
            {
                Debug.Log("BLIT");
                DrawManager.CycleFBOBlit();
            }
            
            _keyDownLastFrame = keyStateDownThisFrame;
        }
    }
}