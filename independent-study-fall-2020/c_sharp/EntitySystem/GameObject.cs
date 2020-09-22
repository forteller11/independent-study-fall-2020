using System;
using Indpendent_Study_Fall_2020.MaterialRelated;
using OpenTK;
using OpenTK.Input;

namespace Indpendent_Study_Fall_2020.EntitySystem
{
    public abstract class GameObject
    {
        public readonly Guid GUID = Guid.NewGuid();
        public virtual string MaterialName { get; } = String.Empty; // "" means no material are being used
        public Material Material; //this is set automatically by drawManager before unLoad is called
        
        public virtual void OnLoad() { }
        public virtual void OnUpdate(GameObjectUpdateEventArgs eventArgs) {}
        public virtual void SendUniformsToShader() { }
        public virtual void OnClose() { }

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