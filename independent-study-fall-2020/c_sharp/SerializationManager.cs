using System.IO;

namespace CART_457
{
    public static class SerializationManager
    {
        public static string ProjectPath;
        public static string AssetPath;
        public static string MeshPath;
        public static string TexturePath;
        public static string ShaderPath;
        public static string ShaderLibraryPath;

        static SerializationManager()
        {
            var dllPath = Directory.GetCurrentDirectory();
            ProjectPath = Path.GetFullPath(Path.Combine(dllPath, @"..\..\..\..\"));
            AssetPath = ProjectPath + @"\assets";
            MeshPath = AssetPath + @"\meshes";
            TexturePath = AssetPath + @"\textures";
            ShaderPath = ProjectPath + @"\independent-study-fall-2020\shaders";
            ShaderLibraryPath = ShaderPath + @"\standard_libraries";
        }

    }
}