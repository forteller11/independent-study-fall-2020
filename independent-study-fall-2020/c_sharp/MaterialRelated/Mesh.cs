using System.Collections.Generic;
using System.Data;

namespace Indpendent_Study_Fall_2020.MaterialRelated
{
    public class Mesh
    {
        public const string POSITION_UNIFORM_NAME = "in_position";
        public const string UV_UNIFORM_NAME = "in_uv";
        public const string NORMAL_UNIFORM_NAME = "in_normal";
        
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
            
            #region error checking
            if (Normals != null && Normals.AttributeName != NORMAL_UNIFORM_NAME)
                throw new DataException($"Normals Attrib Buffer's must be \"{NORMAL_UNIFORM_NAME}\"");

            if (UVs != null && UVs.AttributeName != UV_UNIFORM_NAME)
                throw new DataException($"Uvs Attrib Buffer's must be \"{UV_UNIFORM_NAME}\"");

            if (Positions != null && Positions.AttributeName != POSITION_UNIFORM_NAME)
                throw new DataException($"Position Attrib Buffer's must be \"{POSITION_UNIFORM_NAME}\"");
            #endregion
        }
    }
}