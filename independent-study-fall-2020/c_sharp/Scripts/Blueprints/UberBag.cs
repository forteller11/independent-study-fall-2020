using CART_457;
using CART_457.EntitySystem;
using CART_457.Helpers;
using CART_457.PhysicsRelated;
using CART_457.Renderer;
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
        private UberDriver _uberDriver;
        private ColliderGroup _upstairsFloor = new ColliderGroup();
        
        private bool IsPickedUp;
        private bool _rayHitThisFrame;
        private Vector3 _targetPosition;
        private bool HasDoorBeenOpen;
        private bool IsUpstairs;
        private float HasBeenUpstairsFrameCount = 0;
        
        public UberBag(ColliderGroup floor, PlayerController player, DoorOpen door, UberDriver driver) : base(new []{SetupMaterials.UberBag})
        {
            _floor = floor;
            _player = player;
            AddCollider(new MeshCollider(this, true, SetupMeshes.UberBag));
            _door = door;
            _uberDriver = driver;
        }

        public override void OnLoad()
        {
            _targetPosition = LocalPosition;
            _upstairsFloor.AddCollider(new MeshCollider(null, false, SetupMeshes.RoomClean01Colliders));
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
                
                if (_upstairsFloor.Raycast(ray, out _, false, false))
                    IsUpstairs = true;


            }

            if (IsPickedUp)
            {
                _targetPosition = (_player.WorldRotation * -new Vector3(0,0,3)) + _player.WorldPosition;
            }

            if (IsUpstairs)
                HasBeenUpstairsFrameCount++;
            if (HasBeenUpstairsFrameCount > 140)
            {
                ScreenManager.SetTarget(Globals.UberDriver);
                _uberDriver.BeginDriving = true;
            }

            float interpSpeed = IsPickedUp ? .3f : .1f;
            LocalPosition = Vector3.Lerp(LocalPosition, _targetPosition, interpSpeed);
            _rayHitThisFrame = false;
        }
    }
}