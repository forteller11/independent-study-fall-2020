using System;
using CART_457.Helpers;
using CART_457.MaterialRelated;
using CART_457.PhysicsRelated;
using OpenTK;
using OpenTK.Mathematics;


namespace CART_457.EntitySystem
{
    public abstract class Entity 
    {
        public readonly Guid GUID;
        public Material [] Materials  { get; private set; }
        public ColliderGroup ColliderGroup;
        
        public bool AddedToRenderer;
        public bool AddedToCollisionWorld;
        
        
        
        #region transform related
        public Vector3 LocalPosition = Vector3.Zero;
        public Vector3 WorldPosition {
            get
            {
                if (Parent == null) 
                    return LocalPosition;
                var parentRot = Parent.LocalRotation;
                var localToWorldPosition = parentRot * (LocalPosition * Parent.WorldScale);
                return Parent.WorldPosition + localToWorldPosition;
            }
        }
        
        public Quaternion LocalRotation = Quaternion.Identity;
        public Quaternion WorldRotation 
        {
            get 
            {
                if (Parent == null) 
                    return LocalRotation;
                return Parent.WorldRotation * LocalRotation;
            }
        }


        public Vector3 LocalScale = Vector3.One;
        public Vector3 WorldScale 
        {
            get 
            {
                if (Parent == null) return LocalScale;
                return Parent.WorldScale * LocalScale;
            }
        }

        public Entity Parent;
        #endregion
        
        

        public Entity()
        {
            GUID = Guid.NewGuid();
        }
        
        public Entity(Material [] materials) 
        {
            GUID = Guid.NewGuid();
            AssignMaterials(materials);
        }

        public void AssignMaterials(params Material[] materialTypes)
        {
            Materials = materialTypes;
        }

        public void AddCollider(SphereCollider collider)
        {
            ColliderGroup.AddCollider(collider);
            CollisionWorld.ColliderGroup.AddCollider(collider);
        }
        
        public void AddCollider(PlaneCollider collider)
        {
            ColliderGroup.AddCollider(collider);
            CollisionWorld.ColliderGroup.AddCollider(collider);
        }

        public void SetLocalTransform(Vector3 position, Quaternion rotation, Vector3 scale)
        {
            LocalPosition = position;
            LocalRotation = rotation;
            LocalScale = scale;
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
        
    }
}