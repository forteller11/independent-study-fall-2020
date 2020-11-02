
using CART_457.c_sharp.Renderer;
using CART_457.EntitySystem;
using CART_457.MaterialRelated;
using CART_457.Scripts.Setups;
using CART_457.Helpers;
using OpenTK.Graphics.OpenGL4;

namespace Indpendent_Study_Fall_2020.c_sharp.Scripts.Blueprints
{
    public class Screen : Entity
    {
        public TextureUnit RoomTextureUnit;
        public Screen(TextureUnit roomTexture)
        {
            RoomTextureUnit = roomTexture;
            AssignMaterials(SetupMaterials.Screen); 
        }

        public override void SendUniformsPerObject(Material material)
        {
            UniformSender.SetInt(material,"TextureIndex",RoomTextureUnit.ToInt(), false );
        }

    }
}