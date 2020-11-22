using System;
using CART_457.Helpers;
using CART_457.MaterialRelated;
using CART_457.PhysicsRelated;
using CART_457.Scripts.Blueprints;
using CART_457.Scripts.Setups;
using OpenTK;
using OpenTK.Mathematics;


namespace CART_457.EntitySystem
{
    public abstract class Entity 
    {
        public readonly Guid GUID;
        public Material [] Materials  { get; private set; }
        public ColliderGroup ColliderGroup = new ColliderGroup();
        
        public bool AddedToRenderer;
        public bool AddedToCollisionWorld;
        
        
        
        #region transform related
        public Vector3 LocalPosition = Vector3.Zero;
        public Vector3 WorldPosition {
            get
            {
                if (Parent == null) 
                    return LocalPosition;
                var parentRot = Parent.WorldRotation;
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
            EntityManager.AddToWorldAndRenderer(this);
        }
        
        public Entity(Material [] materials) 
        {
            GUID = Guid.NewGuid();
            AssignMaterials(materials);
            EntityManager.AddToWorldAndRenderer(this);
        }

        public void AssignMaterials(params Material[] materialTypes)
        {
            Materials = materialTypes;
        }

        public void VisualizeColliders()
        {
            for (int i = 0; i < ColliderGroup.Spheres.Count; i++)
            {
                var visualizer = new EmptySolid(new Vector4(1f, 1f, 1f, 1f), ColliderGroup.Spheres[i].Radius, SetupMaterials.SolidSphereR1);
                visualizer.LocalPosition = WorldPosition;
                // visualizer.Parent = this;
            }
        }
        public void AddCollider(SphereCollider collider)
        {
            ColliderGroup.AddCollider(collider);
            CollisionWorld.ColliderGroup.AddCollider(collider);
        }
        
        public void AddCollider(TriangleCollider collider)
        {
            ColliderGroup.AddCollider(collider);
            CollisionWorld.ColliderGroup.AddCollider(collider);
        }
        
        public void AddCollider(PlaneCollider collider)
        {
            ColliderGroup.AddCollider(collider);
            CollisionWorld.ColliderGroup.AddCollider(collider);
        }

        public void AddColliders(TriangleCollider[] triangles)
        {
            for (int i = 0; i < triangles.Length; i++)
                AddCollider(triangles[i]);
        }
        
        public void AddColliders(Mesh mesh, bool shouldParent=true)
        {
            var parentEntity = shouldParent ? this : null;
            AddColliders(ModelImporter.GetTrianglesMesh(mesh, parentEntity, shouldParent));
        }
        

        public void SetLocalTransform(Vector3 position, Quaternion rotation, Vector3 scale)
        {
            LocalPosition = position;
            LocalRotation = rotation;
            LocalScale = scale;
        }
        public virtual void OnLoad() { }

        public virtual void OnUpdate(EntityUpdateEventArgs eventArgs) { }
        public virtual void SendUniformsPerEntity(Material material) { }

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