﻿using Indpendent_Study_Fall_2020.MaterialRelated;
using Indpendent_Study_Fall_2020.Scripts;
using OpenTK.Input;

namespace Indpendent_Study_Fall_2020.EntitySystem.Scripts.Gameobjects
{
    public class DebugTriggerer : Entity
    {
        private bool _keyDownLastFrame = false;
        private int _blitOffscreenFBOsIndex = -1; //where -1 == default buffer no blit

        private void CycleFBOBlit()
        {
            _blitOffscreenFBOsIndex++;
            if (_blitOffscreenFBOsIndex >= DrawManager.BatchHierachies.Count)
                _blitOffscreenFBOsIndex = -1;
        }


        public override void OnUpdate(EntityUpdateEventArgs eventArgs)
        {
            bool keyStateDownThisFrame = eventArgs.KeyboardState.IsKeyDown(Key.F);
            if (_keyDownLastFrame == false && keyStateDownThisFrame == true)
            {
                CycleFBOBlit();
                if (_blitOffscreenFBOsIndex == -1)
                {
                    DrawManager.FBOToDebugDraw = null;
                    Debug.Log("Showing default fbo");
                }
                else
                {
                    DrawManager.FBOToDebugDraw = DrawManager.BatchHierachies[_blitOffscreenFBOsIndex].FBO;
                    Debug.Log("Showing FBO: " + DrawManager.FBOToDebugDraw.ID);
                }

            }
            
            _keyDownLastFrame = keyStateDownThisFrame;
        }
    }
}