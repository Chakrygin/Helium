using System;

using Helium.Common.Descriptors;

namespace Helium.Mapping.Descriptors
{
    internal sealed class DbDynamicTypeDescriptor : TypeDescriptorBase
    {
        public DbDynamicTypeDescriptor(Type dynamicType) :
            base(dynamicType)
        { }
    }
}
