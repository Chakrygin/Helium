using System;

namespace Helium.Mapping.Descriptors
{
    internal sealed class DbDictionaryOfEntityTypeDescriptor :
        DbDictionaryTypeDescriptor<DbScalarTypeDescriptor, DbEntityTypeDescriptor>
    {
        public DbDictionaryOfEntityTypeDescriptor(Type dictionaryType, Type keyType, Type valueType) :
            base(dictionaryType, keyType, valueType)
        { }
    }
}
