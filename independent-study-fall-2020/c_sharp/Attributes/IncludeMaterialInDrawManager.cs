using System;

namespace Indpendent_Study_Fall_2020.c_sharp.Attributes
{
    [AttributeUsage(System.AttributeTargets.Field, AllowMultiple = true)]
    public class IncludeInPostFX : Attribute { }
}