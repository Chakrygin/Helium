using System;

namespace Helium.Common.Descriptors
{
    internal abstract class TypeDescriptorBase
    {
        protected TypeDescriptorBase(Type type)
        {
            Type = type;
        }

        public Type Type { get; }

        public static implicit operator Type(TypeDescriptorBase descriptor)
        {
            return descriptor.Type;
        }
    }
}
