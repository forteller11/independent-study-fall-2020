using Indpendent_Study_Fall_2020.EntitySystem;
using Indpendent_Study_Fall_2020.MaterialRelated;
using OpenTK.Graphics.OpenGL4;

namespace Indpendent_Study_Fall_2020.Scripts.Materials
{
    public class CreateFBOs
    {
        public enum FBOID
        {
            Default = default,
            Shadow,
            PostProcessing
        };
        
        public static FBO Shadow; 
        public static FBO Default;
        public static FBO[] Create()
        {
        
            Shadow = FBO.Custom(FBOID.Shadow, DrawManager.TKWindowSize, true, true, null);


            Default = FBO.Default(() => {
                GL.Enable(EnableCap.Texture2D);
                GL.Enable(EnableCap.DepthTest);
                GL.Enable(EnableCap.CullFace);});
            
            return new[]
            {
                Shadow,
                Default
            };
        }
    }
}