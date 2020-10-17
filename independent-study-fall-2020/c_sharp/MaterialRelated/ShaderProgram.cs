using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using OpenTK.Graphics.OpenGL4;

namespace CART_457.MaterialRelated
{
    public class ShaderProgram
    {
        public int Handle { get; }
        public Dictionary<string, int> UniformLocations = new Dictionary<string, int>();
        public string FileName { get; private set; }

        private const string VERTEX_FILE_EXT = ".vert";
        private const string FRAG_FILE_EXT = ".frag";
        private const string LIBRARY_FILE_EXT = ".glsl";
        private static string SHADER_VERSION_DIR = "#version 330 core"+ Environment.NewLine;
        private const string POST_FX_VERTEX_SHADER = "position_pass_through";

        public static ShaderProgram Standard(string shadeFileNames) //TODO capsulate stage of graphics pipeline into class (frag, vert, geo...)
        {
            var shader = new ShaderProgram(shadeFileNames,shadeFileNames);
            shader.FileName = shadeFileNames;
            return shader;
        }
        
        public static ShaderProgram PostProcessing(string fragmentFileName, params string[] shaderLibraryFileNames)
        {
            var shader = new ShaderProgram(POST_FX_VERTEX_SHADER,fragmentFileName);
            shader.FileName = fragmentFileName;
            return shader;
        }

        private ShaderProgram(string vertFileName, string fragmentFileName)
        {

            int vertexHandle = CompileShaderAndDebug(vertFileName + VERTEX_FILE_EXT, ShaderType.VertexShader);
            int fragmentHandle = CompileShaderAndDebug(fragmentFileName + FRAG_FILE_EXT, ShaderType.FragmentShader);

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

            string shaderLibrariesCodeText = GetAllShaderLibraries();
            string shaderCodeAndLibraries = SHADER_VERSION_DIR + shaderLibrariesCodeText + shaderCodeText;

            int shaderHandle = GL.CreateShader(shaderType);
            
            GL.ShaderSource(shaderHandle, shaderCodeAndLibraries);
            
            GL.CompileShader(shaderHandle);
            
            #region Debugging
            string infoLogVert = GL.GetShaderInfoLog(shaderHandle);
            if (infoLogVert != String.Empty)
            {
                string[] lines = shaderCodeAndLibraries.Split(Environment.NewLine);
                StringBuilder shaderWithLineNumbers = new StringBuilder();
                
                for (int i = 0; i < lines.Length; i++)
                {
                    shaderWithLineNumbers.Append(i+1 + ": " + lines[i] + Environment.NewLine);
                }
               
                Debug.LogWarning(shaderWithLineNumbers.ToString());
                throw new Exception($"Shader Compilation Error \"{shaderFileName}\": " + infoLogVert);
            }

            #endregion
            return shaderHandle;
        }

        private string GetAllShaderLibraries()
        {
            DirectoryInfo dir = new DirectoryInfo(SerializationManager.ShaderLibraryPath);
            FileInfo[] files = dir.GetFiles("*"+LIBRARY_FILE_EXT);

            StringBuilder shaderLibrariesText = new StringBuilder();
            for (int i = 0; i < files.Length; i++)
            {
                using
                    var streamReader =
                        new StreamReader(
                            files[i].FullName);
                var shaderLibraryFileText = streamReader.ReadToEnd();
                shaderLibrariesText.Append(shaderLibraryFileText);
                shaderLibrariesText.Append(Environment.NewLine);
            }

            return shaderLibrariesText.ToString();
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
                Debug.LogWarning($"AttribLocation \"{name}\" cannot be found (are you using it in the shader? Is there inconsistency in the attribute's naming?)");
            return location;
        }
        
        public int GetUniformLocation(string name)
        {
            int location = GL.GetUniformLocation(Handle, name);
            if (location == -1)
                Debug.LogWarning($"Uniform Location \"{name}\" cannot be found (are you using it in the shader? Is there inconsistency in the uniforms's naming?)");
            return location;
        }

        public void SetUniformInt(string uniformName, int value)
        {
            GL.UseProgram(Handle);
            int location = GetUniformLocation(uniformName);
            GL.Uniform1(location, value);
        }
        


        
 
    }
}