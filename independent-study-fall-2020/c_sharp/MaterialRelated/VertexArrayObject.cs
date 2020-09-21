using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;

namespace Indpendent_Study_Fall_2020
{
    
    public struct VertexArrayObject
    {
        private float[] _buffer;
        public int VerticesCount { get; private set; }
        public int StrideLength { get; private set; }
        public int VBOHandle { get; private set; }
        public int VAOHandle { get; private set; }
        public Dictionary<string, int> AttributeIndex;

        public VertexArrayObject(ShaderProgram program, params AttributeBuffer [] attributeBuffers)
        {
            StrideLength = 0;
            VerticesCount = attributeBuffers[0].VerticesCount;
            AttributeIndex = new Dictionary<string, int>(attributeBuffers.Length);
            int totalAttributesCount = 0;

            for (int i = 0; i < attributeBuffers.Length; i++)
            {
                if (attributeBuffers[i].VerticesCount != attributeBuffers[0].VerticesCount) //error checking
                {
                    throw new Exception($"The attribute \"{attributeBuffers[i].AttributeName}\"" +
                                        $" doesn't have the same number of vertices as attribute: \"{attributeBuffers[i].AttributeName}!\"");
                }
//                AttributeIndex.Add(GL.GetAttribLocation(program.Handle, attributeBuffers[i].AttributeName), i);
                totalAttributesCount += attributeBuffers[i].Stride * VerticesCount;
                StrideLength += attributeBuffers[i].Stride;
            }
            
            _buffer = new float[totalAttributesCount];
            
            for (int vertIndex = 0; vertIndex < attributeBuffers.Length; vertIndex++)
            {
                for (int strideIndex = 0; strideIndex < attributeBuffers[vertIndex].Stride; strideIndex++)
                {
                    int startOfStride = vertIndex * attributeBuffers[vertIndex].Stride;
                    _buffer[startOfStride + strideIndex] = attributeBuffers[vertIndex].Buffer[strideIndex];
                }
            }

            //to
            
            VBOHandle = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBOHandle);
            GL.BufferData(BufferTarget.ArrayBuffer, _buffer.Length * sizeof(float), _buffer, BufferUsageHint.StaticDraw);

            VAOHandle = GL.GenVertexArray();
            GL.BindVertexArray(VAOHandle);
            int offset = 0;
            for (int i = 0; i < attributeBuffers.Length; i++)
            {
                GL.VertexAttribPointer(
                    GL.GetAttribLocation(program.Handle, attributeBuffers[i].AttributeName),
                    attributeBuffers[i].Stride,
                    VertexAttribPointerType.Float,
                    false,
                    StrideLength * sizeof(float),
                    offset
                );
                GL.EnableVertexAttribArray(program.GetAttribLocation(attributeBuffers[i].AttributeName));
                   
               offset += attributeBuffers[i].Stride * sizeof(float);
            }

        }
    }
}