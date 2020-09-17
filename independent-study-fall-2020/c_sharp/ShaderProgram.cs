using System;
using System.IO;
using OpenTK.Graphics.OpenGL4;

namespace Indpendent_Study_Fall_2020
{
    public class ShaderProgram
    {
        public int Handle { get; }


        public ShaderProgram(string vertexFileName, string fragmentFileName) //TODO capsulate stage of graphics pipeline into class (frag, vert, geo...)
        {
            //todo auto add vbos and stuff here
            int vertexHandle = CompileShaderAndDebug(vertexFileName, ShaderType.VertexShader);
            int fragmentHandle = CompileShaderAndDebug(fragmentFileName, ShaderType.FragmentShader);

            Handle = GL.CreateProgram();
            GL.AttachShader(Handle, vertexHandle);
            GL.AttachShader(Handle, fragmentHandle);
            GL.LinkProgram(Handle);
            
            GL.DetachShader(Handle, vertexHandle);
            GL.DetachShader(Handle, fragmentHandle);
            GL.DeleteShader(vertexHandle);
            GL.DeleteShader(fragmentHandle);

        }

        private int CompileShaderAndDebug(string shaderFileName, ShaderType shaderType)
        {
            string shaderCodeText;
            using (var streamReader = new StreamReader(SerializationManager.ShaderPath + "\\" + shaderFileName))
                shaderCodeText = streamReader.ReadToEnd();

            int shaderHandle = GL.CreateShader(shaderType);
            
            GL.ShaderSource(shaderHandle, shaderCodeText);
            
            GL.CompileShader(shaderHandle);
            
            string infoLogVert = GL.GetShaderInfoLog(shaderHandle);
            if (infoLogVert != String.Empty) 
                Console.WriteLine(infoLogVert);

            return shaderHandle;
        }
        
        public void Use()
        {
            GL.UseProgram(Handle);
        }
        
        ~ShaderProgram()
        {
            GL.DeleteProgram(Handle);
        }
        //TODO implement iDisposable to free up memory?? what is wrong with using destructor? is it that an exception will be thrown if openGL context is not active? put program.delete in window.unload() then

        public int GetAttribLocation(string name)
        {
            int location = GL.GetAttribLocation(Handle, name);
            if (location == -1)
                throw new Exception($"AttribLocation \"{name}\" cannot be found (are you using it in the shader? Is there inconsistency in the attribute's naming?)");
            return location;
        }
    }
}