using System;
using System.Reflection;

using Helium.Common.Descriptors;

namespace Helium.Mapping.Descriptors
{
    internal abstract class DbDictionaryTypeDescriptor<TKeyTypeDescriptor, TValueTypeDescriptor> : DbCollectionTypeDescriptorBase
        where TKeyTypeDescriptor : TypeDescriptorBase
        where TValueTypeDescriptor : TypeDescriptorBase
    {
        protected DbDictionaryTypeDescriptor(Type dictionaryType, Type keyType, Type valueType) :
            base(dictionaryType)
        {
            var constructor = dictionaryType.GetConstructor(Type.EmptyTypes);
            if (constructor == null)
            {
                var message =
                    $"Dictionary type {dictionaryType} should have a public default constructor.";

                throw new InvalidOperationException(message);
            }

            var addMethod = dictionaryType.GetMethod("Add", new[] {keyType, valueType});
            if (addMethod == null)
            {
                var message =
                    $"Dictionary type {dictionaryType} should have a public non-static Add method " +
                    $"with parameters of type {keyType} and {valueType}.";

                throw new InvalidOperationException(message);
            }

            Constructor = constructor;
            AddMethod = addMethod;

            KeyType = (TKeyTypeDescriptor) Activator.CreateInstance(typeof(TKeyTypeDescriptor), keyType);
            ValueType = (TValueTypeDescriptor) Activator.CreateInstance(typeof(TValueTypeDescriptor), valueType);
        }

        public override ConstructorInfo Constructor { get; }

        public override MethodInfo AddMethod { get; }

        public TKeyTypeDescriptor KeyType { get; }

        public TValueTypeDescriptor ValueType { get; }
    }
}
