using System;

namespace Helium.Annotations
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class ColumnIgnoreAttribute : Attribute
    { }
}
