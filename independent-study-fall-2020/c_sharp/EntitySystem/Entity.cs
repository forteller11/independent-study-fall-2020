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
        public MaterialSetup.MaterialType [] MaterialTypes { get; private set; }
        
        public Vector3 Position = new Vector3(0,0,0);
        public Quaternion Rotation = Quaternion.Identity;
        public Vector3 Scale = new Vector3(1,1,1);

        [Flags]
        public enum BehaviorFlags
        {
            None = 0b_0000_0000_0000_0000,
        }

        public BehaviorFlags Flags;
        

        public Entity(BehaviorFlags flags, params MaterialSetup.MaterialType [] materialTypes)
        {
            MaterialTypes = materialTypes;
            Flags = flags;
            GUID = Guid.NewGuid();
        }

        public Entity()
        {
            GUID = Guid.NewGuid();
        }

        public void SetupMaterials(params MaterialSetup.MaterialType[] materialTypes)
        {
            MaterialTypes = materialTypes;
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

        public bool ContainsMaterial(MaterialSetup.MaterialType materialType)
        {
            for (int i = 0; i < MaterialTypes.Length; i++)
            {
                if (materialType == MaterialTypes[i])
                    return true;
            }

            return false;
        }

        public bool HasAnyMaterial() => MaterialTypes != null;

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