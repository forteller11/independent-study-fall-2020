using System;
using System.Linq;
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
        public Material [] Materials  { get; private set; }
        
        public Vector3 Position = new Vector3(0,0,0);
        public Quaternion Rotation = Quaternion.Identity;
        public Vector3 Scale = new Vector3(1,1,1);

        [Flags]
        public enum BehaviorFlags
        {
            None = 0b_0000_0000_0000_0000,
        }

        public BehaviorFlags Flags;
        

        public Entity(BehaviorFlags flags, params Material [] materialTypes)
        {
            Materials = materialTypes;
            Flags = flags;
            GUID = Guid.NewGuid();
        }

        public Entity()
        {
            GUID = Guid.NewGuid();
        }

        public void SetupMaterials(params Material[] materialTypes)
        {
            Materials = materialTypes;
        }
        public virtual void OnLoad() { }

        public virtual void OnUpdate(EntityUpdateEventArgs eventArgs) { }
        public virtual void SendUniformsPerObject(Material material) { }

        public virtual void OnClose() { }

        public bool ContainsMaterial(Material material)
        {
            for (int i = 0; i < Materials.Length; i++)
            {
                if (material == Materials[i] && material != null)
                    return true;
            }

            return false;
        }

        public bool HasAnyMaterial() => Materials != null;

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

        /// <summary>
        /// does entity have at least all the flags toCompare does?
        /// </summary>
        /// <param name="toCompare"></param>
        /// <returns></returns>
        public bool HasFlags(BehaviorFlags toCompare)
        {
            BehaviorFlags likeFlags = toCompare & Flags;
            return toCompare == likeFlags;
        }
    }
}