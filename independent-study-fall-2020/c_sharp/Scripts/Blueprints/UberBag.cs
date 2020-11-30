using CART_457;
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
        private DoorOpen _door;
        
        private bool IsPickedUp;
        private bool _rayHitThisFrame;
        private Vector3 _targetPosition;
        private bool HasDoorBeenOpen;
        
        public UberBag(ColliderGroup floor, PlayerController player, DoorOpen door) : base(new []{SetupMaterials.UberBag})
        {
            _floor = floor;
            _player = player;
            AddCollider(new MeshCollider(this, true, SetupMeshes.UberBag));
            _door = door;
        }

        public override void OnLoad()
        {
            _targetPosition = LocalPosition;
        }

        public override void OnRaycastHit() =>_rayHitThisFrame = true;

        public override void OnUpdate(EntityUpdateEventArgs eventArgs)
        {
            if (_door.DoorIsOpen)
                HasDoorBeenOpen = true;
            
            bool mouseClick = eventArgs.MouseState.IsButtonDown(MouseButton.Left) && !eventArgs.MouseState.WasButtonDown(MouseButton.Left);
            bool mouseDown = eventArgs.MouseState.IsButtonDown(MouseButton.Left);
            if (!IsPickedUp && mouseClick && _rayHitThisFrame && HasDoorBeenOpen)
            {
                IsPickedUp = true;
            }

            if (IsPickedUp && !mouseDown)
            {
                IsPickedUp = false;
                var ray = new Ray(WorldPosition, new Vector3(0,1,0));
                var hasHit = _floor.Raycast(ray, out var hits, true, false);
                if (hasHit)
                    _targetPosition = hits[0].NearestOrHitPosition;
                else 
                    _targetPosition = new Vector3(_targetPosition.X,_player.WorldPosition.Y - _player.PlayerHeight,_targetPosition.Z);
                
            }

            if (IsPickedUp)
            {
                _targetPosition = (_player.WorldRotation * -new Vector3(0,0,3)) + _player.WorldPosition;
            }

            LocalPosition = Vector3.Lerp(LocalPosition, _targetPosition, 0.3f);
            _rayHitThisFrame = false;
        }
    }
}