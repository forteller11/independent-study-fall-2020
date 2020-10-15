using Indpendent_Study_Fall_2020.MaterialRelated;
using OpenTK.Graphics.OpenGL4;

namespace Indpendent_Study_Fall_2020.Scripts.Materials
{
    public class CreateFBOs
    {
        public enum FBOType
        {
            Default = default,
            Primary,
            Shadow
        };
        
        public static FBO ShadowBuffer; 
        public static FBO PrimaryBuffer; 
        public static FBO[] Create()
        {
        
            ShadowBuffer = new FBO(FBOType.Shadow, FramebufferAttachment.DepthAttachment, Texture.EmptyDepth(2560, 2560, TextureUnit.Texture3),
                () =>
                {
                    
                });
            
            // PrimaryBuffer = new FBO(FBOType.Shadow, FramebufferAttachment.ColorAttachment0, Texture.EmptyDepth(1000, 1000, TextureUnit.Texture3),
            //     () =>
            //     {
            //         
            //     });
            
            //todo only call relevant enable caps at initialization, not every frame
            var defaultBuffer = new FBO(() => {
                GL.Enable(EnableCap.Texture2D);
                GL.Enable(EnableCap.DepthTest);
                GL.Enable(EnableCap.CullFace);});
            
            return new[]
            {
                ShadowBuffer,
                defaultBuffer
            };
        }
    }
}