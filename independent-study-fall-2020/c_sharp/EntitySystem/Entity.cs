using System;
using CART_457.MaterialRelated;
using OpenTK;


namespace CART_457.EntitySystem
{
    public abstract class Entity //todo... make mega object with flags... add physics component?
    {
        public readonly Guid GUID;
        public Material [] Materials  { get; private set; }

        protected Vector3 _position = Vector3.Zero;
        public Vector3 Position {get
        {
            if (Parent == null) return _position;
            var localToWorldPosition = Quaternion.Invert(Parent.Rotation) * (_position * Parent.Scale);
            // Debug.Log(Parent.Position);
            // Debug.Log(_position);
            // Debug.Log("---");
            return Parent.Position + localToWorldPosition;
        }
            set => _position = value;
        }
        
        protected Quaternion _rotation = Quaternion.Identity;
        public Quaternion Rotation {
            get {
     
            if (Parent == null) return _rotation;
            return _rotation * Parent.Rotation; //wrong order?
        }
            set => _rotation = value;
        }

        protected Vector3 _scale = Vector3.One;
        public Vector3 Scale {
            get {
     
                if (Parent == null) return _scale;
                return Parent.Scale * _scale;
            }
            set => _scale = value;
        }

        public Entity Parent;

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