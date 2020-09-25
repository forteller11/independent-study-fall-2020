using System;
using System.Collections.Generic;
using System.Data;
using OpenTK.Graphics.OpenGL4;

namespace Indpendent_Study_Fall_2020.MaterialRelated
{
    
    public class VAOAndBuffers
    {
        public float[] Buffer;
        public int[] IndicesBuffer;
        
        public int VerticesCount { get; private set; }
        public int StrideLength { get; private set; }
        public int VBOHandle { get; private set; }
        public int VAOHandle { get; private set; }
        public int IndicesHandle { get; private set; }
        public bool UseIndices;
        public Dictionary<string, int> AttributeIndex;

        public VAOAndBuffers(ShaderProgram program, int[] indices, params AttributeBuffer [] attributeBuffers)
        {
            MergeBuffers(attributeBuffers, out Buffer);

            VBOHandle = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBOHandle);
            GL.BufferData(BufferTarget.ArrayBuffer, Buffer.Length * sizeof(float), Buffer, BufferUsageHint.StaticDraw);

            GenerateVAOFromBuffer(program, attributeBuffers);

            SetupIndices(indices);
        }

        private void MergeBuffers(AttributeBuffer[] attributeBuffers, out float[] buffer)
        {
            int totalAttributesCount = 0;
            StrideLength = 0;
            VerticesCount = attributeBuffers[0].VerticesCount;
            AttributeIndex = new Dictionary<string, int>(attributeBuffers.Length);
            
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
            
            buffer = new float[totalAttributesCount];

            int mergedBufferIndex = 0;
            for (int vertIndex = 0; vertIndex < VerticesCount; vertIndex++) //TODO FIX THIS COMBINATION ALGO
            {
                for (int bufferIndex = 0; bufferIndex < attributeBuffers.Length; bufferIndex++)
                {
                    for (int intraStrideIndex = 0; intraStrideIndex < attributeBuffers[bufferIndex].Stride; intraStrideIndex++)
                    {
 
                        int startOfStrideIndex = vertIndex * attributeBuffers[bufferIndex].Stride;
                        int attribIndex = startOfStrideIndex + intraStrideIndex;
                        
                        buffer[mergedBufferIndex] = attributeBuffers[bufferIndex].Buffer[attribIndex];
                        mergedBufferIndex++;
                    }
                }
                
            }
        }
        private void GenerateVAOFromBuffer(ShaderProgram program, AttributeBuffer[] attributeBuffers)
        {
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
        
        private void SetupIndices(int[] indices)
        {
            UseIndices = indices != null;
            if (UseIndices)
            {
                for (int i = 0; i < indices.Length; i++)
                    if (indices[i] > VerticesCount - 1)
                        throw new DataException($"Indices reference a vertex which don't not exist in the VAO");

                IndicesBuffer = indices;
                
                IndicesHandle = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, IndicesHandle);
                GL.BufferData(
                    BufferTarget.ArrayBuffer, 
                    IndicesBuffer.Length * sizeof(int),
                    IndicesBuffer,
                    BufferUsageHint.StaticDraw);
            }
        }
    }
}