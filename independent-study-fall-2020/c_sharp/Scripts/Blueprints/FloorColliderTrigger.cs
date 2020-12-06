using CART_457;
using CART_457.EntitySystem;
using CART_457.Helpers;
using CART_457.PhysicsRelated;
using CART_457.Renderer;
using OpenTK.Mathematics;

namespace Indpendent_Study_Fall_2020.c_sharp.Scripts.Blueprints
{
    public class FloorColliderTrigger : Entity
    {
        private Vector3[] _triggerPositions;
        private ColliderGroup _floorColliders;
        private MeshCollider _colliderSection;
        private Camera _frustrum;
        public bool Triggered;
        private bool _triggeredWhenInFrustrum;

        public FloorColliderTrigger(Camera frustrum, MeshCollider colliderSection, ColliderGroup floorColliders, bool triggerWhenInFrustrum, params Vector3 [] triggerPositions)
        {
            _frustrum = frustrum;
            _colliderSection = colliderSection;
            _floorColliders = floorColliders;
            _triggerPositions = triggerPositions;
            _triggeredWhenInFrustrum = triggerWhenInFrustrum;

            // for (int i = 0; i < _triggerPositions.Length; i++)
            //     Empty.FromPosition(triggerPositions[i], SetupMaterials.SolidSphereR1);

        }

        public override void OnLoad()
        {
            base.OnLoad();
        }

        public override void OnUpdate(EntityUpdateEventArgs eventArgs)
        {
            //Debug.Log(PhysicsHelpersInd.IsPointWithinFrustrum(_triggerPositions[0], _frustrum));
            if (!Triggered)
            {
                for (int i = 0; i < _triggerPositions.Length; i++)
                {
                    
                    bool isPointWithinFrustrum = PhysicsHelpersInd.IsPointWithinFrustrum(_triggerPositions[i], _frustrum);
                    
                    if (_triggeredWhenInFrustrum && !isPointWithinFrustrum)
                        return;
                    
                    if (!_triggeredWhenInFrustrum && isPointWithinFrustrum)
                        return;
                    
                    Triggered = true;
                    _floorColliders.Meshes.Add(_colliderSection);
                    return;
                    
                }
            }
            
            if (Triggered)
            {
                for (int i = 0; i < _triggerPositions.Length; i++)
                {
                    bool isPointWithinFrustrum = PhysicsHelpersInd.IsPointWithinFrustrum(_triggerPositions[i], _frustrum);
                    
                    if (_triggeredWhenInFrustrum && !isPointWithinFrustrum)
                    {
                        Triggered = false;
                        _floorColliders.Meshes.Remove(_colliderSection);
                        return;
                    }
                    
                    if (!_triggeredWhenInFrustrum && isPointWithinFrustrum)
                    {
                        Triggered = false;
                        _floorColliders.Meshes.Remove(_colliderSection);
                        return;
                    }
                }

            }

        }
    }
}