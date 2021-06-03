using System;

namespace Helium.Mapping.Descriptors
{
    internal sealed class DbDictionaryOfScalarTypeDescriptor :
        DbDictionaryTypeDescriptor<DbScalarTypeDescriptor, DbScalarTypeDescriptor>
    {
        public DbDictionaryOfScalarTypeDescriptor(Type dictionaryType, Type keyType, Type valueType) :
            base(dictionaryType, keyType, valueType)
        { }
    }
}
