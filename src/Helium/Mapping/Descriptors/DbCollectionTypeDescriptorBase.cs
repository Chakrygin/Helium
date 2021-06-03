using System;
using System.Reflection;

using Helium.Common.Descriptors;

namespace Helium.Mapping.Descriptors
{
    internal abstract class DbCollectionTypeDescriptorBase : TypeDescriptorBase
    {
        protected DbCollectionTypeDescriptorBase(Type collectionType) :
            base(collectionType)
        { }

        public abstract ConstructorInfo Constructor { get; }

        public abstract MethodInfo AddMethod { get; }
    }
}
