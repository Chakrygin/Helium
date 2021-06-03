using System;

namespace Helium.Mapping.Descriptors
{
    internal sealed class DbCollectionOfScalarTypeDescriptor : DbCollectionTypeDescriptor<DbScalarTypeDescriptor>
    {
        public DbCollectionOfScalarTypeDescriptor(Type collectionType, Type itemType) :
            base(collectionType, itemType)
        { }
    }
}
