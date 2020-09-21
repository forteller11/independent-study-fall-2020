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
        private int _vboHandle;
        private int _vaoHandle;
        private float[] _buffer;

        //combine multiple vbos into single vbo, then automagically vao it up
        //todo, join multiple buffers into single vbo each with names, create vao
        void JoinBuffersAndCreateAttributes(params AttributeBuffer [] attributeBuffers)
        {
            int totalStrideLength = 0;
            for (int i = 0; i < attributeBuffers.Length; i++)
            {
                if (attributeBuffers[i].VerticesCount != attributeBuffers[0].VerticesCount)
                {
                    throw new Exception($"The attribute \"{attributeBuffers[i].AttributeName}\"" +
                                        $" doesn't have the same number of vertices as attribute: \"{attributeBuffers[i].AttributeName}!\"");
                }

                totalStrideLength += attributeBuffers[i].Stride;
            } //error checking
            
            for (int vertIndex = 0; vertIndex < attributeBuffers.Length; vertIndex++)
            {
                for (int strideIndex = 0; strideIndex < attributeBuffers[vertIndex].Stride; strideIndex++)
                {
                    int startOfStride = vertIndex * attributeBuffers[vertIndex].Stride;
                    _buffer[startOfStride + strideIndex] = attributeBuffers[vertIndex].Buffer[strideIndex];
                }
            }

            //todo get dictionary wokring
//            for (int i = 0; i < UPPER; i++)
//            {
//                //does calling get attriblocation() work b4 it's actually bound in shader?
//            }

            _shader.Use();
            
            _vboHandle = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vboHandle);
            GL.BufferData(BufferTarget.ArrayBuffer, _buffer.Length * sizeof(float), _buffer, BufferUsageHint.StaticDraw);

            _vaoHandle = GL.GenVertexArray();
            GL.BindVertexArray(_vaoHandle);
            int offset = 0;
            for (int i = 0; i < attributeBuffers.Length; i++)
            {
                GL.VertexAttribPointer(
                    GL.GetAttribLocation(_shader.Handle, attributeBuffers[i].AttributeName),
                    attributeBuffers[i].Stride,
                    VertexAttribPointerType.Float,
                    false,
                    totalStrideLength * sizeof(float),
                    offset
                );
                GL.EnableVertexAttribArray(_shader.GetAttribLocation(attributeBuffers[i].AttributeName));
                   
               offset += attributeBuffers[i].Stride * sizeof(float);
            }

        }

        //todo, create way to use vao, and then draw using mat
        void Draw()
        {
            GL.BindVertexArray(_vaoHandle);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
        }

        int UseAttrib(string attributeName)
        {
            return _shader.GetAttribLocation(attributeName);
        }
    }
}