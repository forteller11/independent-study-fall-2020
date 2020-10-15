using OpenTK;

namespace Indpendent_Study_Fall_2020.MaterialRelated
{
    public class AttributeBuffer
    {
        public readonly string AttributeName;
        public readonly int Stride;
        public readonly float[] Buffer;

        public int VerticesCount => Buffer.Length / Stride;
        

        public AttributeBuffer(string attributeName, int stride, float[] buffer)
        {
            AttributeName = attributeName;
            Stride = stride;
            Buffer = buffer;
        }

        public AttributeBuffer(string attributeName, Vector3[] buffer)
        {
            Stride = 3;
            AttributeName = attributeName;

            Buffer = new float[buffer.Length*Stride];
            for (int i = 0; i < buffer.Length; i++)
            {
                Buffer[i*Stride + 0] = buffer[i].X;
                Buffer[i*Stride + 1] = buffer[i].Y;
                Buffer[i*Stride + 2] = buffer[i].Z;
            }
        }
        
        public AttributeBuffer(string attributeName, Vector2[] buffer)
        {
            Stride = 2;
            AttributeName = attributeName;

            Buffer = new float[buffer.Length*Stride];
            for (int i = 0; i < buffer.Length; i++)
            {
                Buffer[i*Stride + 0] = buffer[i].X;
                Buffer[i*Stride + 1] = buffer[i].Y;
            }
        }

        public static AttributeBuffer PositionAttribute(Vector3[] buffer) => new AttributeBuffer(Mesh.POSITION_UNIFORM_NAME, buffer);
        public static AttributeBuffer NormalAttribute(Vector3[] buffer) => new AttributeBuffer(Mesh.NORMAL_UNIFORM_NAME, buffer);
        public static AttributeBuffer UVAttribute(Vector2[] buffer) => new AttributeBuffer(Mesh.UV_UNIFORM_NAME, buffer);
       
    }
}