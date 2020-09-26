﻿namespace Indpendent_Study_Fall_2020.MaterialRelated
{
    public interface Uniform
    {
        string ShaderName { get; set; }
        void UploadToShader();
        void Use();
    }
}