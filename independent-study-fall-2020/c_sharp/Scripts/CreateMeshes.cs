
using Indpendent_Study_Fall_2020.Helpers;
using Indpendent_Study_Fall_2020.MaterialRelated;

namespace Indpendent_Study_Fall_2020.c_sharp.Scripts
{
    /// <summary>
    /// Must be called BEFORE create materials
    /// </summary>
    public static class CreateMeshes
    {
        public static Mesh IcoSphereHighPoly;
        public static Mesh Plane;

        public static void Create()
        {
            IcoSphereHighPoly =  ModelImporter.GetAttribBuffersFromObjFile("ico_sphere", true, true, true);
            Plane =  ModelImporter.GetAttribBuffersFromObjFile("Plane", true, true, true);
        }
    }
}