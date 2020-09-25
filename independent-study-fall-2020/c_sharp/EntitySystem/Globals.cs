using OpenTK;

namespace Indpendent_Study_Fall_2020.EntitySystem
{
    public static class Globals
    {
        public static DrawManager DrawManager = new DrawManager();
        
        public static Vector3 CameraPosition;
        public static Quaternion CameraRotation;
        public static Matrix4 CameraPerspective;

        public static void Init()
        {
            Matrix4.CreatePerspectiveFieldOfView(MHelper.DEG_TO_RAD * 90, 1f, 0.5f, 1000f, out CameraPerspective);
        }

    }
}