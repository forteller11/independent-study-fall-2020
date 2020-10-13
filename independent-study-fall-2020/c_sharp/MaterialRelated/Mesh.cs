using System.Collections.Generic;

namespace Indpendent_Study_Fall_2020.MaterialRelated
{
    public class Mesh
    {
        public AttributeBuffer Positions;
        public AttributeBuffer UVs;
        public AttributeBuffer Normals;

        public List<AttributeBuffer> ToList()
        {
            List<AttributeBuffer> buffers = new List<AttributeBuffer>();
            if (Positions != null) 
                buffers.Add(Positions);
            if (UVs != null) 
                buffers.Add(UVs);
            if (Normals != null) 
                buffers.Add(Normals);
            return buffers;
        }
        
        public Mesh(AttributeBuffer positions, AttributeBuffer uvs, AttributeBuffer normals)
        {
            Positions = positions;
            UVs = uvs;
            Normals = normals;
        }
    }
}