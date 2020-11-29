using System;
using CART_457;
using CART_457.EntitySystem;
using CART_457.PhysicsRelated;
using CART_457.Scripts.Blueprints;
using CART_457.Scripts.Setups;
using OpenTK.Mathematics;

namespace Indpendent_Study_Fall_2020.c_sharp.Scripts.Blueprints
{
    public class DoorOpen : Entity
    {
        private PlayerController Player;
        
        private float _angleAtStartOfOpen;
        private Quaternion _rotFromLastOpen;
        private bool _openingDoor;
        private SphereCollider _doorStopper = new SphereCollider(null, false, 3f, new Vector3(-12.788f, -11.252f, 53.329f));
        private SphereCollider _doorEnd;

        public DoorOpen(PlayerController player) : base(new[] {SetupMaterials.DoorOpen})
        {
            Player = player;
            _doorEnd = new SphereCollider(this, true, 1.2f, new Vector3(-3.46f, 2.248f, 0.1f));
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
            if (_openingDoor)
            {
                var angle = GetAngle();
                var finalAngle = -angle  + _angleAtStartOfOpen;
                var rotCache = LocalRotation;
                LocalRotation = _rotFromLastOpen * Quaternion.FromEulerAngles(0f, finalAngle, 0f);

                LocalRotation.ToAxisAngle(out Vector3 _, out float axisDifference);
                if (MathF.Abs(axisDifference) > MathHelper.DegreesToRadians(160f))
                {
                    LocalRotation = rotCache;
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