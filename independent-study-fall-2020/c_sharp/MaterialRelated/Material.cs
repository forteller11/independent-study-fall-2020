using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;

namespace Indpendent_Study_Fall_2020
{
    public class Material
    {
        //todo will include shader, and all relevant data (uniforms, vbos... textures)
        private ShaderProgram _shader;
        private Dictionary<string, int> Attrib;
        private List<Uniform> _uniforms;
        private float[] _buffer;

        //combine multiple vbos into single vbo, then automagically vao it up
        //todo, join multiple buffers into single vbo each with names, create vao
        void JoinBuffersAndCreateAttributes(params AttributeBuffer [] attributeBuffers)
        {
            for (int i = 0; i < attributeBuffers.Length; i++)
            {
                if (attributeBuffers[i].VerticesCount != attributeBuffers[0].VerticesCount)
                {
                    throw new Exception($"The attribute \"{attributeBuffers[i].AttributeName}\"" +
                                        $" doesn't have the same number of vertices as attribute: \"{attributeBuffers[i].AttributeName}!\"");
                }
            } //error checking
            
            //todo do joining, add names and handle to dictionary after.... create vao
            for (int vertIndex = 0; vertIndex < attributeBuffers.Length; vertIndex++)
            {
                for (int strideIndex = 0; strideIndex < attributeBuffers[vertIndex].Stride; strideIndex++)
                {
                    int startOfStride = vertIndex * attributeBuffers[vertIndex].Stride;
                    _buffer[startOfStride + strideIndex] = attributeBuffers[vertIndex].Buffer[strideIndex];
                }
            }
            
            //upload vbo
            //do vao work
        }

        int UseAttrib(string attributeName)
        {
            return _shader.GetAttribLocation(attributeName);
        }
    }
}