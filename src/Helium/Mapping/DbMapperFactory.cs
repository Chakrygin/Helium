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

            return CreateScalarMapper(type);
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
    }
}
