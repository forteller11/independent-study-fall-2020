using System;
using System.Numerics;
using Indpendent_Study_Fall_2020.c_sharp.Renderer;
using Indpendent_Study_Fall_2020.MaterialRelated;
using OpenTK.Input;
using Quaternion = OpenTK.Quaternion;
using Vector3 = OpenTK.Vector3;

namespace Indpendent_Study_Fall_2020.EntitySystem
{
    public abstract class Entity : IBatchable //todo... make mega object with flags... add physics component?
    {
        public readonly Guid GUID;
        public string ParentNameInDrawingManager  = String.Empty; // "" means no material are being used
        public string UniqueName { get; set; } = "entity"
        public Material Material; //this is set automatically by drawManager before unLoad is called
        public Behaviors Flags;
        [Flags]
        public enum Behaviors
        {
            Transform, //todo declare bits?
            Physics,
            Gravity,
            Collision
        }
        //todo only send pos/rot/scale if the values are dirty
        public Vector3 Position = new Vector3(0,0,0);
        public Quaternion Rotation = Quaternion.Identity;
        public Vector3 Scale = new Vector3(1,1,1);

        public Vector3 Velocity;
        public Vector3 VelocityAngular;
        public float Mass;

        public Entity(string materialName)
        {
            is = materialName;
            GUID = Guid.NewGuid();
        }
        public virtual void OnLoad() { }

        public virtual void OnUpdate(EntityUpdateEventArgs eventArgs)
        {
//            if (Flags.HasFlag(Behaviors.Physics) && Flags.HasFlag(Behaviors.Transform))
//            {
//                if (Flags.HasFlag(Behaviors.Gravity))
//                {
//                    Velocity += Globals.Gravity;
//                }
//            }
        }
        public virtual void SendUniformsPerObject() { }
        public virtual void SendUniformsPerMaterial() { }
        public virtual void OnClose() { }

        #region dictionary performance stuff
        public override bool Equals(object obj)
        {
            var otherGameObject = obj as Entity;
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