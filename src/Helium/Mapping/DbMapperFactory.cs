using System;

using Helium.Mapping.Builders;
using Helium.Mapping.Descriptors;
using Helium.Provider;

namespace Helium.Mapping
{
    internal sealed class DbMapperFactory : DbMapperFactoryBase
    {
        public DbMapperFactory(DbDataReaderTypeDescriptor dataReaderType)
        {
            DataReaderType = dataReaderType;
        }

        public DbDataReaderTypeDescriptor DataReaderType { get; }

        public DbMapper<T> GetMapper<T>()
        {
            return (DbMapper<T>) GetMapperWithLock(typeof(T));
        }

        protected override object CreateMapper(Type type)
        {
            if (type.IsDynamicType())
            {
                return CreateDynamicMapper(type);
            }

            if (type.IsTupleType())
            {
                return CreateTupleMapper(type);
            }

            if (type.IsScalarType(DataReaderType))
            {
                return CreateScalarMapper(type);
            }

            if (type.IsGenericDictionaryType(out var keyType, out var valueType))
            {
                if (!keyType.IsScalarType(DataReaderType))
                {
                    throw new InvalidOperationException("Mapping for generic dictionary types is supported only for dictionaries with scalar key type.");
                }

                if (valueType.IsDynamicType())
                {
                    return CreateDictionaryOfDynamicMapper(type, keyType, valueType);
                }

                if (valueType.IsTupleType())
                {
                    return CreateDictionaryOfTupleMapper(type, keyType, valueType);
                }

                if (valueType.IsScalarType(DataReaderType))
                {
                    return CreateDictionaryOfScalarMapper(type, keyType, valueType);
                }

                return CreateDictionaryOfEntityMapper(type, keyType, valueType);
            }

            if (type.IsDictionaryType())
            {
                throw new InvalidOperationException("Mapping for non-generic dictionary types is not supported.");
            }

            if (type.IsGenericCollectionType(out var itemType))
            {
                if (itemType.IsDynamicType())
                {
                    return CreateCollectionOfDynamicMapper(type, itemType);
                }

                if (itemType.IsTupleType())
                {
                    return CreateCollectionOfTupleMapper(type, itemType);
                }

                if (itemType.IsScalarType(DataReaderType))
                {
                    return CreateCollectionOfScalarMapper(type, itemType);
                }

                return CreateCollectionOfEntityMapper(type, itemType);
            }

            if (type.IsCollectionType())
            {
                throw new InvalidOperationException("Mapping for non-generic collection types is not supported.");
            }

            if (type.IsEnumerableType())
            {
                throw new InvalidOperationException("Mapping for enumerable types is not supported.");
            }

            return CreateEntityMapper(type);
        }

        private object CreateDynamicMapper(Type dynamicType)
        {
            var returnType = new DbDynamicTypeDescriptor(dynamicType);
            var builder = new DbDynamicTypeMapperBuilder(DataReaderType, returnType);

            return builder.CreateMapper();
        }

        private object CreateTupleMapper(Type tupleType)
        {
            var returnType = new DbTupleTypeDescriptor(tupleType);
            var builder = new DbTupleTypeMapperBuilder(DataReaderType, returnType);

            return builder.CreateMapper();
        }

        private object CreateScalarMapper(Type scalarType)
        {
            var returnType = new DbScalarTypeDescriptor(scalarType);
            var builder = new DbScalarTypeMapperBuilder(DataReaderType, returnType);

            return builder.CreateMapper();
        }

        private object CreateEntityMapper(Type entityType)
        {
            var returnType = new DbEntityTypeDescriptor(entityType);
            var builder = new DbEntityTypeMapperBuilder(DataReaderType, returnType);

            return builder.CreateMapper();
        }

        private object CreateCollectionOfDynamicMapper(Type collectionType, Type itemType)
        {
            var returnType = new DbCollectionOfDynamicTypeDescriptor(collectionType, itemType);
            var builder = new DbCollectionOfDynamicTypeMapperBuilder(DataReaderType, returnType);

            return builder.CreateMapper();
        }

        private object CreateCollectionOfTupleMapper(Type collectionType, Type itemType)
        {
            var returnType = new DbCollectionOfTupleTypeDescriptor(collectionType, itemType);
            var builder = new DbCollectionOfTupleTypeMapperBuilder(DataReaderType, returnType);

            return builder.CreateMapper();
        }

        private object CreateCollectionOfScalarMapper(Type collectionType, Type itemType)
        {
            var returnType = new DbCollectionOfScalarTypeDescriptor(collectionType, itemType);
            var builder = new DbCollectionOfScalarTypeMapperBuilder(DataReaderType, returnType);

            return builder.CreateMapper();
        }

        private object CreateCollectionOfEntityMapper(Type collectionType, Type itemType)
        {
            var returnType = new DbCollectionOfEntityTypeDescriptor(collectionType, itemType);
            var builder = new DbCollectionOfEntityTypeMapperBuilder(DataReaderType, returnType);

            return builder.CreateMapper();
        }

        private object CreateDictionaryOfDynamicMapper(Type dictionaryType, Type keyType, Type valueType)
        {
            var returnType = new DbDictionaryOfDynamicTypeDescriptor(dictionaryType, keyType, valueType);
            var builder = new DbDictionaryOfDynamicTypeMapperBuilder(DataReaderType, returnType);

            return builder.CreateMapper();
        }

        private object CreateDictionaryOfTupleMapper(Type dictionaryType, Type keyType, Type valueType)
        {
            var returnType = new DbDictionaryOfTupleTypeDescriptor(dictionaryType, keyType, valueType);
            var builder = new DbDictionaryOfTupleTypeMapperBuilder(DataReaderType, returnType);

            return builder.CreateMapper();
        }

        private object CreateDictionaryOfScalarMapper(Type dictionaryType, Type keyType, Type valueType)
        {
            var returnType = new DbDictionaryOfScalarTypeDescriptor(dictionaryType, keyType, valueType);
            var builder = new DbDictionaryOfScalarTypeMapperBuilder(DataReaderType, returnType);

            return builder.CreateMapper();
        }

        private object CreateDictionaryOfEntityMapper(Type dictionaryType, Type keyType, Type valueType)
        {
            var returnType = new DbDictionaryOfEntityTypeDescriptor(dictionaryType, keyType, valueType);
            var builder = new DbDictionaryOfEntityTypeMapperBuilder(DataReaderType, returnType);

            return builder.CreateMapper();
        }
    }
}
