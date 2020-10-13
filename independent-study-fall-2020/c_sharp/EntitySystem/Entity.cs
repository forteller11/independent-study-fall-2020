﻿using System;
using System.Numerics;
using Indpendent_Study_Fall_2020.MaterialRelated;
using Indpendent_Study_Fall_2020.Scripts;
using OpenTK.Input;
using Quaternion = OpenTK.Quaternion;
using Vector3 = OpenTK.Vector3;

namespace Indpendent_Study_Fall_2020.EntitySystem
{
    public abstract class Entity //todo... make mega object with flags... add physics component?
    {
        public readonly Guid GUID;
        public readonly CreateMaterials.MaterialType MaterialType; // "" means no material are being used
        public readonly CreateMaterials.MaterialType MeshType; // "" means no material are being used

        //todo only send pos/rot/scale if the values are dirty
        public Vector3 Position = new Vector3(0,0,0);
        public Quaternion Rotation = Quaternion.Identity;
        public Vector3 Scale = new Vector3(1,1,1);
        

        public Entity(CreateMaterials.MaterialType materialType)
        {
            MaterialType = materialType;
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
        public virtual void SendUniformsPerObject(Material material) { }

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