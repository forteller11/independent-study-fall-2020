using System;

namespace CART_457.Attributes
{
    [AttributeUsage(System.AttributeTargets.Field, AllowMultiple = true)]
    public class IncludeInPostFX : Attribute { }
}