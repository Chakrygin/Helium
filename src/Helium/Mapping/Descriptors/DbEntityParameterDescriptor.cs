using System.Reflection;

using Helium.Common.Descriptors;

namespace Helium.Mapping.Descriptors
{
    internal sealed class DbEntityParameterDescriptor : ParameterDescriptorBase
    {
        public DbEntityParameterDescriptor(ParameterInfo parameter, string columnName, int ordinalIndex) :
            base(parameter)
        {
            ParameterType = new DbScalarTypeDescriptor(parameter.ParameterType);
            ColumnName = columnName;
            OrdinalIndex = ordinalIndex;
        }

        public DbScalarTypeDescriptor ParameterType { get; }

        public string ColumnName { get; }

        public int OrdinalIndex { get; }
    }
}
