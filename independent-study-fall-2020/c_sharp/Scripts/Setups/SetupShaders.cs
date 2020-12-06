using CART_457.MaterialRelated;

namespace CART_457.Scripts.Setups
{
    public static class SetupShaders
    {
        public static ShaderProgram NormalReceiveShadow = ShaderProgram.Create("normal_map_receive_shadow");
        public static ShaderProgram NormalReceiveShadowFrustrum = ShaderProgram.Create("normal_map_receive_shadow_frustrum");
        public static ShaderProgram Normal = ShaderProgram.Create("normal_map");
        public static ShaderProgram NormalFrustrum = ShaderProgram.Create("normal_map_frustrum");

        public static ShaderProgram Solid = ShaderProgram.Create("textureless");
        public static ShaderProgram Screen = ShaderProgram.Create("screen");
        public static ShaderProgram Shadow = ShaderProgram.Create("shadow_map");
        public static ShaderProgram PostFX = ShaderProgram.Create("position_pass_through","post_fx");
    }
}