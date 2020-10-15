using System;
using System.Collections.Generic;
using System.Data;
using OpenTK.Graphics.OpenGL4;

namespace Indpendent_Study_Fall_2020.MaterialRelated
{
    
    public class VAOAndBuffers
    {
        public float[] Buffer;
        public uint[] IndicesBuffer;
        
        public int VerticesCount { get; private set; }
        public int StrideLength { get; private set; }
        public int VBOHandle { get; private set; }
        public int VAOHandle { get; private set; }


 
        public VAOAndBuffers(Material material,  Mesh mesh)
        {
            var attributeBuffersInMesh = mesh.ToList();
            List<AttributeBuffer> attribsInShader = new List<AttributeBuffer>(attributeBuffersInMesh.Count);

            #region debug
            //make sure all attributes in shader have correspondant buffers (in name at least, not necessarily stride)
            foreach (var attribInShader in material.VertexAttribLocations)
            {
                bool foundAttribInBuffer = false;
                for (int i = 0; i < attributeBuffersInMesh.Count; i++)
                {
                    if (attribInShader.Key == attributeBuffersInMesh[i].AttributeName)
                        foundAttribInBuffer = true;
                }

                if (foundAttribInBuffer == false)
                    throw new DataException($"Vertex Attribute with name {attribInShader.Key} has not been found in shader");
            }

            for (int i = 0; i < attributeBuffersInMesh.Count; i++) //only include attribs found in the shader program
            {
                if (material.VertexAttribLocations.ContainsKey(attributeBuffersInMesh[i].AttributeName))
                    attribsInShader.Add(attributeBuffersInMesh[i]);
                else
                    Debug.LogWarning($"Vertex Attribute {attributeBuffersInMesh[i].AttributeName} has not been found in shader {material.Shader.FileName}, it will automatically be removed from the program.");
            }
            
            #endregion

            var attribsInShaderArray = attribsInShader.ToArray();
            
            MergeBuffers(attribsInShaderArray, out Buffer);

            VBOHandle = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBOHandle);
            GL.BufferData(BufferTarget.ArrayBuffer, Buffer.Length * sizeof(float), Buffer, BufferUsageHint.StaticDraw);

            GenerateVAOFromBuffer(material, attribsInShaderArray);

//            SetupIndices(indices); 
        }

        private void MergeBuffers(AttributeBuffer[] attributeBuffers, out float[] buffer)
        {
            int totalAttributesCount = 0;
            StrideLength = 0;
            VerticesCount = attributeBuffers[0].VerticesCount;

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
        
        /// <summary>
        /// requires a shaderProgram and its vertex/uniform locations to be associated with the material
        /// </summary>
        private void GenerateVAOFromBuffer(Material material, AttributeBuffer[] attributeBuffers)
        {
            VAOHandle = GL.GenVertexArray();
            GL.BindVertexArray(VAOHandle);
            int offset = 0;
            for (int i = 0; i < attributeBuffers.Length; i++)
            {
                int attribLocation = material.GetAttribLocation(attributeBuffers[i].AttributeName);
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