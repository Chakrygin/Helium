using System;

namespace Helium.Mapping.Descriptors
{
    internal sealed class DbCollectionOfDynamicTypeDescriptor : DbCollectionTypeDescriptor<DbDynamicTypeDescriptor>
    {
        public DbCollectionOfDynamicTypeDescriptor(Type collectionType, Type itemType) :
            base(collectionType, itemType)
        { }
    }
}
