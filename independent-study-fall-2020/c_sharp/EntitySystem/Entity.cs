using System;
using CART_457.MaterialRelated;
using OpenTK;


namespace CART_457.EntitySystem
{
    public abstract class Entity //todo... make mega object with flags... add physics component?
    {
        public readonly Guid GUID;
        public Material [] Materials  { get; private set; }
        
        public Transform Transform;
        #region transform acessors
        public Vector3 Position
        {
            get => Transform.Position;
            set => Transform.Position = value;
        }
        
        public Quaternion Rotation
        {
            get => Transform.Rotation;
            set => Transform.Rotation = value;
        }
        
        public Vector3 Scale
        {
            get => Transform.Scale;
            set => Transform.Scale = value;
        }
        
        public Entity Parent
        {
            get => Transform.Parent;
            set => Transform.Parent = value;
        }
        #endregion

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