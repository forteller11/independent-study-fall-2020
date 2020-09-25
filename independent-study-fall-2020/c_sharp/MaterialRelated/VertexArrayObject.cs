using System;
using System.Collections.Generic;
using System.Data;
using OpenTK.Graphics.OpenGL4;

namespace Indpendent_Study_Fall_2020.MaterialRelated
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
                    throw new DataException($"The attribute \"{attributeBuffers[i].AttributeName}\"" +
                                        $" doesn't have the same number of vertices as attribute: \"{attributeBuffers[i].AttributeName}!\"");
                }
                totalAttributesCount += attributeBuffers[i].Stride * VerticesCount;
                StrideLength += attributeBuffers[i].Stride;
            }
            
            _buffer = new float[totalAttributesCount];

            int mergedBufferIndex = 0;
            for (int vertIndex = 0; vertIndex < VerticesCount; vertIndex++) //TODO FIX THIS COMBINATION ALGO
            {
                for (int bufferIndex = 0; bufferIndex < attributeBuffers.Length; bufferIndex++)
                {
                    for (int intraStrideIndex = 0; intraStrideIndex < attributeBuffers[bufferIndex].Stride; intraStrideIndex++)
                    {
 
                        int startOfStrideIndex = vertIndex * attributeBuffers[bufferIndex].Stride;
                        int attribIndex = startOfStrideIndex + intraStrideIndex;
                        
                        _buffer[mergedBufferIndex] = attributeBuffers[bufferIndex].Buffer[attribIndex];
                        mergedBufferIndex++;
                    }
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