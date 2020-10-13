using System.Collections.Generic;
using System.Data;
using Indpendent_Study_Fall_2020.c_sharp.Renderer;
using OpenTK.Graphics.OpenGL4;

namespace Indpendent_Study_Fall_2020.MaterialRelated
{
    public class Mesh
    {
        public float[] Buffer;
        public uint[] IndicesBuffer;
        
        public int VerticesCount { get; private set; }
        public int StrideLength { get; private set; }
        public int VBOHandle { get; private set; }
        public int VAOHandle { get; private set; }
//        public int IndicesHandle { get; private set; }
//        public bool UseIndices = false;
        public Dictionary<string, int> AttributeIndex;
 
        public Mesh(Material material, params AttributeBuffer [] attributeBuffers)
        {
            ValidateCompatabilityWithMaterial(material, attributeBuffers);

            MergeBuffers(attributeBuffers, out Buffer);

            VBOHandle = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBOHandle);
            GL.BufferData(BufferTarget.ArrayBuffer, Buffer.Length * sizeof(float), Buffer, BufferUsageHint.StaticDraw);

            GenerateVAOFromBuffer(material, attributeBuffers);


        }

        public void ValidateCompatabilityWithMaterial(Material material, AttributeBuffer[] attributeBuffers)
        {
            //make sure all attributes in shader have correspondant buffers (in name at least, not necessarily stride)
            foreach (var attribInShader in material.VertexAttribLocations)
            {
                bool foundAttribInBuffer = false;
                for (int i = 0; i < attributeBuffers.Length; i++)
                {
                    if (attribInShader.Key == attributeBuffers[i].AttributeName)
                        foundAttribInBuffer = true;
                }

                if (foundAttribInBuffer == false)
                    throw new DataException($"Vertex Attribute with name {attribInShader.Key} has not been found in shader");
            }
            
            for (int i = 0; i < attributeBuffers.Length; i++) //warn that there are more buffers than necessary to work with this program
            {
                if (material.VertexAttribLocations.ContainsKey(attributeBuffers[i].AttributeName) == false)
                    Debug.LogWarning($"Vertex Attribute {attributeBuffers[i].AttributeName} has not been found in shader {material.Shader.FileName} but is in Mesh, memory is potentially be wasted.");
            }
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
                                            $" doesn't have the same number of vertices as attribute: \"{attributeBuffers[0].AttributeName}!\"");
                }
                totalAttributesCount += attributeBuffers[i].Stride * VerticesCount;
                StrideLength += attributeBuffers[i].Stride;
            }
            
            buffer = new float[totalAttributesCount];

            int mergedBufferIndex = 0;
            for (int vertIndex = 0; vertIndex < VerticesCount; vertIndex++) 
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
        private void GenerateVAOFromBuffer(Material mat, AttributeBuffer[] attributeBuffers)
        {
            VAOHandle = GL.GenVertexArray();
            GL.BindVertexArray(VAOHandle);
            int offset = 0;
            for (int i = 0; i < attributeBuffers.Length; i++)
            {
                int attribLocation = mat.GetAttribLocation(attributeBuffers[i].AttributeName);
                GL.VertexAttribPointer(
                    attribLocation,
                    attributeBuffers[i].Stride,
                    VertexAttribPointerType.Float,
                    false,
                    StrideLength * sizeof(float),
                    offset
                );
                GL.EnableVertexAttribArray(attribLocation);
                   
                offset += attributeBuffers[i].Stride * sizeof(float);
            }
        }
    }
}