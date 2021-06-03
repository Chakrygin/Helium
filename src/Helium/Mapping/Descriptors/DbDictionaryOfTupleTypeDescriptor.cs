using System;

namespace Helium.Mapping.Descriptors
{
    internal sealed class DbDictionaryOfTupleTypeDescriptor :
        DbDictionaryTypeDescriptor<DbScalarTypeDescriptor, DbTupleTypeDescriptor>
    {
        public DbDictionaryOfTupleTypeDescriptor(Type dictionaryType, Type keyType, Type valueType) :
            base(dictionaryType, keyType, valueType)
        { }
    }
}
