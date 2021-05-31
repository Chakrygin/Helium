using System;
using System.Reflection;

using Helium.Common.Descriptors;

namespace Helium.Mapping.Descriptors
{
    internal sealed class DbScalarTypeDescriptor : TypeDescriptorBase
    {
        public DbScalarTypeDescriptor(Type scalarType) :
            base(scalarType)
        {
            var underlyingType = Nullable.GetUnderlyingType(scalarType);
            if (underlyingType != null)
            {
                scalarType = underlyingType;
                NullableConstructor = Type.GetConstructor(new[] {scalarType})!;
            }

            UnderlyingType = scalarType.IsEnum
                ? scalarType.GetEnumUnderlyingType()
                : scalarType;
        }

        public Type UnderlyingType { get; }

        public ConstructorInfo? NullableConstructor { get; }
    }
}
