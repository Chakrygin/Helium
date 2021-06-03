using System;

namespace Helium.Mapping.Descriptors
{
    internal sealed class DbCollectionOfTupleTypeDescriptor : DbCollectionTypeDescriptor<DbTupleTypeDescriptor>
    {
        public DbCollectionOfTupleTypeDescriptor(Type collectionType, Type itemType) :
            base(collectionType, itemType)
        { }
    }
}
