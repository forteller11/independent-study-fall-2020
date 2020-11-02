using CART_457.MaterialRelated;

namespace CART_457.Scripts
{
    public static class SetupShaders
    {
        public static ShaderProgram Normal = ShaderProgram.Standard("normal_map");
        public static ShaderProgram Solid = ShaderProgram.Standard("textureless");
        public static ShaderProgram Screen = ShaderProgram.Standard("screen");
        public static ShaderProgram Shadow = ShaderProgram.Standard("shadow_map");
    }
}