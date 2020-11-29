using System;
using CART_457.EntitySystem;
using CART_457.Scripts.Blueprints;
using CART_457.Scripts.Setups;
using OpenTK.Mathematics;

namespace Indpendent_Study_Fall_2020.c_sharp.Scripts.Blueprints
{
    public class DoorOpen : Entity
    {
        private PlayerController Player;

        private Vector3 _playerOffsetAtStartOfOpen;
        private bool _openingDoor;

        public DoorOpen(PlayerController player) : base(new[] {SetupMaterials.DoorOpen})
        {
            Player = player;
        }
        public void BeginOpen()
        {
            _playerOffsetAtStartOfOpen = Player.WorldPosition - WorldPosition;
            _openingDoor = true;
        }

        public void EndOpen()
        {
            _openingDoor = false;
        }

        public override void OnUpdate(EntityUpdateEventArgs eventArgs)
        {
            if (_openingDoor)
            {
                //Vector2 positionOffset = Player.WorldPosition.Xz - _playerPosAtStartOfOpen.Xz;
                Vector2 positionOffset = Player.WorldPosition.Xz - WorldPosition.Xz;
                positionOffset -= _playerOffsetAtStartOfOpen.Xz;
                positionOffset = Vector2.Normalize(positionOffset);
                var angle = MathF.Atan2(positionOffset.Y, positionOffset.X);
                LocalRotation = Quaternion.FromEulerAngles(0f, angle, 0f);
            }
        }
    }
}