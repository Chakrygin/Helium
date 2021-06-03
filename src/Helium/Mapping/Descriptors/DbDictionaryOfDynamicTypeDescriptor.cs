using System;

namespace Helium.Mapping.Descriptors
{
    internal sealed class DbDictionaryOfDynamicTypeDescriptor :
        DbDictionaryTypeDescriptor<DbScalarTypeDescriptor, DbDynamicTypeDescriptor>
    {
        public DbDictionaryOfDynamicTypeDescriptor(Type dictionaryType, Type keyType, Type valueType) :
            base(dictionaryType, keyType, valueType)
        { }
    }
}
