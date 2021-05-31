using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Helium.Common.Descriptors;

namespace Helium.Mapping.Descriptors
{
    internal sealed class DbTupleTypeDescriptor : TypeDescriptorBase
    {
        public DbTupleTypeDescriptor(Type tupleType) :
            base(tupleType)
        {
            var underlyingType = Nullable.GetUnderlyingType(tupleType);
            if (underlyingType != null)
            {
                tupleType = underlyingType;
                NullableConstructor = Type.GetConstructor(new[] {tupleType})!;
            }

            Constructor = tupleType
                .GetConstructors(BindingFlags.Public | BindingFlags.Instance)
                .Single();

            ItemTypes = Constructor
                .GetParameters()
                .Select(x => new DbScalarTypeDescriptor(x.ParameterType))
                .ToList();
        }

        public bool IsValueType => Type.IsValueType;

        public ConstructorInfo Constructor { get; }

        public ConstructorInfo? NullableConstructor { get; }

        public List<DbScalarTypeDescriptor> ItemTypes { get; }
    }
}
