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
            return CreateScalarMapper(type);
        }

        private object CreateScalarMapper(Type scalarType)
        {
            var returnType = new DbScalarTypeDescriptor(scalarType);
            var builder = new DbScalarTypeMapperBuilder(DataReaderType, returnType);

            return builder.CreateMapper();
        }
    }
}
