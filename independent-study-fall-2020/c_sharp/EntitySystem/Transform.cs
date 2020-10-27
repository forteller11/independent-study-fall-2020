using OpenTK;

namespace CART_457.EntitySystem
{
    public struct Transform
    {
        private Vector3 _position;
        public Vector3 Position {get
            {
                if (Parent == null) return _position;
                var localToWorldPosition = Quaternion.Invert(Parent.Rotation) * (_position * Parent.Scale);
                // Debug.Log(Parent.Position);
                // Debug.Log(_position);
                // Debug.Log("---");
                return Parent.Position + localToWorldPosition;
            }
            set => _position = value;
        }

        private Quaternion _rotation;
        public Quaternion Rotation {
            get {
     
                if (Parent == null) return _rotation;
                return _rotation * Parent.Rotation; //wrong order?
            }
            set => _rotation = value;
        }

        private Vector3 _scale;
        public Vector3 Scale {
            get {
     
                if (Parent == null) return _scale;
                return Parent.Scale * _scale;
            }
            set => _scale = value;
        }

        public Entity Parent;
    }
}