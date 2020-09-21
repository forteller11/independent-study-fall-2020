using System.Collections.Generic;

namespace Indpendent_Study_Fall_2020
{
    public class Material
    {
        //todo will include shader, and all relevant data (uniforms, vbos... textures)
        private ShaderProgram _shader;
        private List<VertexBuffer> _vbos;
        private List<Uniform> _uniforms;
    }
}