using System.Reflection;

using Helium.Common.Descriptors;

namespace Helium.Mapping.Descriptors
{
    internal sealed class DbEntityPropertyDescriptor : PropertyDescriptorBase
    {
        public DbEntityPropertyDescriptor(PropertyInfo property, string columnName, int ordinalIndex) :
            base(property)
        {
            PropertyType = new DbScalarTypeDescriptor(property.PropertyType);
            ColumnName = columnName;
            OrdinalIndex = ordinalIndex;
        }

        public DbScalarTypeDescriptor PropertyType { get; }

        public string ColumnName { get; }

        public int OrdinalIndex { get; }
    }
}
