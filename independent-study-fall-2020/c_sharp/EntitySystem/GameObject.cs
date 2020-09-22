using System;

namespace Indpendent_Study_Fall_2020
{
    public class GameObject
    {
        public readonly Guid GUID = Guid.NewGuid();
        public bool ShouldRender;
        public Material Material;
        public void OnLoad()
        {
            
        }

        public void OnUpdate()
        {
            
        }
        
        public void SendUniformsToShader()
        {
            //todo batch like draw calls together?
            if (ShouldRender)
                Material.PrepareAndDraw();
        }

        public void OnClose()
        {
            
        }

        #region dictionary performance stuff
        public override bool Equals(object obj)
        {
            var otherGameObject = obj as GameObject;
            if (otherGameObject != null)
                return otherGameObject.GUID == this.GUID;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return GUID.GetHashCode();
        }
        #endregion
    }
}