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
            
            
            string vertexFile;
            string fragmentFile;
            
            using (var streamReader = new StreamReader(SerializationManager.ShaderPath + "\\" + vertexFileName)) //read text from file
                vertexFile = streamReader.ReadToEnd();
            using (var streamReader = new StreamReader(SerializationManager.ShaderPath + "\\" + fragmentFileName))
                fragmentFile = streamReader.ReadToEnd();

            int vertexHandle = GL.CreateShader(ShaderType.VertexShader); //create handles
            int fragmentHandle = GL.CreateShader(ShaderType.VertexShader);
            
            GL.ShaderSource(vertexHandle, vertexFile); //bind handle to text
            GL.ShaderSource(fragmentHandle, fragmentFile);
            
            
            CompileShaderAndDebug(vertexHandle);
            CompileShaderAndDebug(fragmentHandle);

            Handle = GL.CreateProgram();
            GL.AttachShader(Handle, vertexHandle);
            GL.AttachShader(Handle, fragmentHandle);
            GL.LinkProgram(Handle);
            
            GL.DetachShader(Handle, vertexHandle);
            GL.DetachShader(Handle, fragmentHandle);
            GL.DeleteShader(vertexHandle);
            GL.DeleteShader(fragmentHandle);

        }

        private void CompileShaderAndDebug(int shaderHandle)
        {
            GL.CompileShader(shaderHandle);
            
            string infoLogVert = GL.GetShaderInfoLog(shaderHandle);
            if (infoLogVert != String.Empty) 
                Console.WriteLine(infoLogVert);
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
                throw new Exception("AttribLocation cannot be found (are you using it in the shader?)");
            return location;
        }
    }
}