using System.Data;

using Helium.Common.Emitters;
using Helium.Common.Emitters.Extensions;
using Helium.Mapping.Descriptors;
using Helium.Mapping.Emitters;
using Helium.Mapping.Emitters.Extensions;
using Helium.Provider;

namespace Helium.Mapping.Builders
{
    internal sealed class DbCollectionOfEntityTypeMapperBuilder : DbMapperBuilderBase<DbCollectionOfEntityTypeDescriptor>
    {
        public DbCollectionOfEntityTypeMapperBuilder(DbDataReaderTypeDescriptor dataReaderType, DbCollectionOfEntityTypeDescriptor returnType) :
            base(dataReaderType, returnType)
        { }

        protected override void Init(EmitterState state)
        {
            CommandBehavior = CommandBehavior.SingleResult;

            state.Add(ReturnType.ItemType.OrdinalIndices);
        }

        protected override void Emit(DbMapperEmitter emitter)
        {
            // var reader = (TDataReader) arg0;
            emitter.EmitAssignDataReader();

            // var result = new Collection();
            emitter.EmitReadCollection(ReturnType, () =>
            {
                // var ordinals = ...;
                emitter.EmitAssignOrdinals(ReturnType.ItemType.OrdinalIndices);

                // while (reader.Read())
                emitter.EmitReadCollectionItems(ReturnType, () =>
                {
                    // var item = new T { ... };
                    // result.Add(item);
                    emitter.EmitReadEntity(ReturnType.ItemType);
                });
            });

            // return result;
            emitter.EmitReturn();
        }
    }
}
