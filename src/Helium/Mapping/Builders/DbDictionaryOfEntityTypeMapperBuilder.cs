using System.Data;

using Helium.Common.Emitters;
using Helium.Common.Emitters.Extensions;
using Helium.Mapping.Descriptors;
using Helium.Mapping.Emitters;
using Helium.Mapping.Emitters.Extensions;
using Helium.Provider;

namespace Helium.Mapping.Builders
{
    internal sealed class DbDictionaryOfEntityTypeMapperBuilder : DbMapperBuilderBase<DbDictionaryOfEntityTypeDescriptor>
    {
        public DbDictionaryOfEntityTypeMapperBuilder(DbDataReaderTypeDescriptor dataReaderType, DbDictionaryOfEntityTypeDescriptor returnType) :
            base(dataReaderType, returnType)
        { }

        protected override void Init(EmitterState state)
        {
            CommandBehavior = CommandBehavior.SingleResult;

            state.Add(ReturnType.ValueType.OrdinalIndices);
        }

        protected override void Emit(DbMapperEmitter emitter)
        {
            // var reader = (TDataReader) arg0;
            emitter.EmitAssignDataReader();

            // var result = new Dictionary();
            emitter.EmitReadCollection(ReturnType, () =>
            {
                // var ordinals = ...;
                emitter.EmitAssignOrdinals(ReturnType.ValueType.OrdinalIndices);

                // while (reader.Read())
                emitter.EmitReadCollectionItems(ReturnType, () =>
                {
                    // var key = (TKey) reader.GetValue(0);
                    // var value = new T { ... };
                    // result.Add(key, value);
                    emitter.EmitReadScalarOrDefault(ReturnType.KeyType, ordinal: 0);
                    emitter.EmitReadEntity(ReturnType.ValueType);
                });
            });

            // return result;
            emitter.EmitReturn();
        }
    }
}
