namespace Indpendent_Study_Fall_2020.MaterialRelated
{
    public struct AttributeBuffer
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
    }
}