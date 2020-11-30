using CART_457.EntitySystem;
using CART_457.Helpers;
using CART_457.PhysicsRelated;
using CART_457.Scripts.Blueprints;
using CART_457.Scripts.Setups;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Indpendent_Study_Fall_2020.c_sharp.Scripts.Blueprints
{
    public class UberBag : Entity
    {
        private ColliderGroup _floor;
        private PlayerController _player;
        private bool IsPickedUp;
        private bool _rayHitThisFrame;
        private Vector3 _targetPosition;
        
        public UberBag(ColliderGroup floor, PlayerController player) : base(new []{SetupMaterials.UberBag})
        {
            _floor = floor;
            _player = player;
            AddCollider(new MeshCollider(this, true, SetupMeshes.UberBag));
        }

        public override void OnLoad()
        {
            _targetPosition = LocalPosition;
        }

        public override void OnRaycastHit() =>_rayHitThisFrame = true;

        public override void OnUpdate(EntityUpdateEventArgs eventArgs)
        {
            bool mouseDown = eventArgs.MouseState.IsButtonDown(MouseButton.Left);
            if (!IsPickedUp && mouseDown && _rayHitThisFrame)
            {
                IsPickedUp = true;
            }

            if (IsPickedUp && !mouseDown)
            {
                IsPickedUp = false;
                var ray = new Ray(WorldPosition, new Vector3(0,-1,0));
                if (_floor.Raycast(ray, out var hits, true, false))
                {
                    _targetPosition = hits[0].NearestOrHitPosition;
                }
                else 
                    _targetPosition -= new Vector3(0,.4f,0);
            }

            if (IsPickedUp)
            {
                _targetPosition = (_player.WorldRotation * -new Vector3(0,0,3)) + _player.WorldPosition;
            }

            LocalPosition = Vector3.Lerp(LocalPosition, _targetPosition, 0.3f);
        }
    }
}