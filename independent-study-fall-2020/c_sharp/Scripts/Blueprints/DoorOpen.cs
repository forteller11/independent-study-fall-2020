using System;
using CART_457;
using CART_457.EntitySystem;
using CART_457.PhysicsRelated;
using CART_457.Renderer;
using CART_457.Scripts.Blueprints;
using CART_457.Scripts.Setups;
using OpenTK.Mathematics;

namespace Indpendent_Study_Fall_2020.c_sharp.Scripts.Blueprints
{
    public class DoorOpen : Entity
    {
        public float AngleDifferenceFromIdentity;
        private PlayerController Player;
        private ColliderGroup _floor;
        
        private float _angleAtStartOfOpen;
        private Quaternion _rotFromLastOpen;
        private bool _openingDoor;
        public bool DoorIsOpen;
        public MeshCollider BasementToEntranceFloor = new MeshCollider(null, false, SetupMeshes.BasementFloorCollidersDoor);

        public DoorOpen(PlayerController player, ColliderGroup floor) : base(new[] {SetupMaterials.DoorOpen})
        {
            Player = player;
            _floor = floor;
        }

        public override void OnLoad()
        {
            _rotFromLastOpen = LocalRotation;
        }

        public void BeginOpen()
        {
            _angleAtStartOfOpen = GetAngle();
            _openingDoor = true;
        }

        public void EndOpen()
        {
            _openingDoor = false;
            _rotFromLastOpen = LocalRotation;
        }

        public override void OnUpdate(EntityUpdateEventArgs eventArgs)
        {
            Globals.MainCamera.CopyFrom(Globals.PlayerCameraRoom1);
            if (_openingDoor)
            {
                var angle = GetAngle();
                var finalAngle = -angle  + _angleAtStartOfOpen;
                var rotCache = LocalRotation;
                LocalRotation = _rotFromLastOpen * Quaternion.FromEulerAngles(0f, finalAngle, 0f);

                LocalRotation.ToAxisAngle(out Vector3 _, out float axisDifference);
                AngleDifferenceFromIdentity = MathF.Abs(axisDifference);
                if (AngleDifferenceFromIdentity > MathHelper.DegreesToRadians(160f))
                {
                    LocalRotation = rotCache;
                }

                if (AngleDifferenceFromIdentity > MathHelper.DegreesToRadians(20f))
                {
                    Globals.MainCamera.CopyFrom(Globals.UberDriver);
                }

                if (AngleDifferenceFromIdentity > MathHelper.DegreesToRadians(60f))
                {
                    if (!DoorIsOpen)
                    {
                        DoorIsOpen = true;
                        _floor.Meshes.Add(BasementToEntranceFloor);
                    }
                }
                else if (DoorIsOpen) //if less
                {
                    DoorIsOpen = false;
                    _floor.Meshes.Remove(BasementToEntranceFloor);
                }
        
            }
        }

        float GetAngle()
        {
            Vector2 positionOffset = Player.WorldPosition.Xz - WorldPosition.Xz;
                
            positionOffset = Vector2.Normalize(positionOffset);
            var angle = MathF.Atan2(positionOffset.Y, positionOffset.X);
            return angle;
        }
    }
}