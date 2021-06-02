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
                    throw new NotImplementedException();
                    // return CreateDictionaryOfDynamicMapper(type, keyType, valueType);
                }

                if (valueType.IsTupleType())
                {
                    throw new NotImplementedException();
                    // return CreateDictionaryOfTupleMapper(type, keyType, valueType);
                }

                if (valueType.IsScalarType(DataReaderType))
                {
                    throw new NotImplementedException();
                    // return CreateDictionaryOfScalarMapper(type, keyType, valueType);
                }

                throw new NotImplementedException();
                // return CreateDictionaryOfEntityMapper(type, keyType, valueType);
            }

            if (type.IsDictionaryType())
            {
                throw new InvalidOperationException("Mapping for non-generic dictionary types is not supported.");
            }

            if (type.IsGenericCollectionType(out var itemType))
            {
                if (itemType.IsDynamicType())
                {
                    throw new NotImplementedException();
                    // return CreateCollectionOfDynamicMapper(type, itemType);
                }

                if (itemType.IsTupleType())
                {
                    throw new NotImplementedException();
                    // return CreateCollectionOfTupleMapper(type, itemType);
                }

                if (itemType.IsScalarType(DataReaderType))
                {
                    throw new NotImplementedException();
                    // return CreateCollectionOfScalarMapper(type, itemType);
                }

                throw new NotImplementedException();
                // return CreateCollectionOfEntityMapper(type, itemType);
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
    }
}
