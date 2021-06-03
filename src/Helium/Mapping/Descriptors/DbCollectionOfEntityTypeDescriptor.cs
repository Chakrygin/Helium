using System;

namespace Helium.Mapping.Descriptors
{
    internal sealed class DbCollectionOfEntityTypeDescriptor : DbCollectionTypeDescriptor<DbEntityTypeDescriptor>
    {
        public DbCollectionOfEntityTypeDescriptor(Type collectionType, Type itemType) :
            base(collectionType, itemType)
        { }
    }
}
