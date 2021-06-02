using System;

namespace Helium.Annotations
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class ColumnAttribute : Attribute
    {
        public ColumnAttribute()
        {
            Name = "";
        }

        public ColumnAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
