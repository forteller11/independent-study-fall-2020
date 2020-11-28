using CART_457;
using CART_457.EntitySystem;
using CART_457.Helpers;
using CART_457.PhysicsRelated;
using CART_457.Renderer;
using CART_457.Scripts.Blueprints;
using CART_457.Scripts.Setups;
using OpenTK.Mathematics;

namespace Indpendent_Study_Fall_2020.c_sharp.Scripts.Blueprints
{
    public class FloorColliderTrigger : Entity
    {
        private Vector3[] _triggerPositions;
        private ColliderGroup _floorColliders;
        private MeshCollider _colliderSection;
        private Camera _frustrum;
        public bool IsWithinFrustrum;

        public FloorColliderTrigger(Camera frustrum, MeshCollider colliderSection, ColliderGroup floorColliders, params Vector3 [] triggerPositions)
        {
            _frustrum = frustrum;
            _colliderSection = colliderSection;
            _floorColliders = floorColliders;
            _triggerPositions = triggerPositions;

            for (int i = 0; i < _triggerPositions.Length; i++)
                Empty.FromPosition(triggerPositions[i], SetupMaterials.SolidSphereR1);
            
        }

        public override void OnUpdate(EntityUpdateEventArgs eventArgs)
        {
            Debug.Log(PhysicsHelpersInd.IsPointWithinFrustrum(_triggerPositions[0], _frustrum));
            if (IsWithinFrustrum == false)
            {
                for (int i = 0; i < _triggerPositions.Length; i++)
                    if (!PhysicsHelpersInd.IsPointWithinFrustrum(_triggerPositions[i], _frustrum))
                        return;
                
                IsWithinFrustrum = true;
                _floorColliders.Meshes.Add(_colliderSection);
            }
            if (IsWithinFrustrum)
            {
                for (int i = 0; i < _triggerPositions.Length; i++)
                    if (!PhysicsHelpersInd.IsPointWithinFrustrum(_triggerPositions[i], _frustrum))
                    {
                        IsWithinFrustrum = false;
                        _floorColliders.Meshes.Remove(_colliderSection);
                        return;
                    }
            }

        }
    }
}