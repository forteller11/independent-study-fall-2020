using System;
using System.IO;

namespace Indpendent_Study_Fall_2020
{
    public class Shader
    {
        int Handle;

        public Shader(string vertexFileName, string fragmentFileName)
        {
            string vertexFile;
            string fragmentFile;
            
            using (var streamReader = new StreamReader(SerializationManager.ShaderPath + "\\" + vertexFileName))
                vertexFile = streamReader.ReadToEnd();
            using (var streamReader = new StreamReader(SerializationManager.ShaderPath + "\\" + fragmentFileName))
                fragmentFile = streamReader.ReadToEnd();
            
            Console.WriteLine(vertexFile);
            Console.WriteLine(fragmentFile);
            

        }
    }
}