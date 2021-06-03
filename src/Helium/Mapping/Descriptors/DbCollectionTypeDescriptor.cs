using System;
using System.Reflection;

using Helium.Common.Descriptors;

namespace Helium.Mapping.Descriptors
{
    internal abstract class DbCollectionTypeDescriptor<TItemTypeDescriptor> : DbCollectionTypeDescriptorBase
        where TItemTypeDescriptor : TypeDescriptorBase
    {
        protected DbCollectionTypeDescriptor(Type collectionType, Type itemType) :
            base(collectionType)
        {
            var constructor = collectionType.GetConstructor(Type.EmptyTypes);
            if (constructor == null)
            {
                var message =
                    $"Collection type {collectionType} should have a public default constructor.";

                throw new InvalidOperationException(message);
            }

            var addMethod = collectionType.GetMethod("Add", new[] {itemType});
            if (addMethod == null)
            {
                var message =
                    $"Collection type {collectionType} should have a public non-static Add method " +
                    $"with a parameter of type {itemType}.";

                throw new InvalidOperationException(message);
            }

            Constructor = constructor;
            AddMethod = addMethod;

            ItemType = (TItemTypeDescriptor) Activator.CreateInstance(typeof(TItemTypeDescriptor), itemType);
        }

        public override ConstructorInfo Constructor { get; }

        public override MethodInfo AddMethod { get; }

        public TItemTypeDescriptor ItemType { get; }
    }
}
