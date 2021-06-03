using System.Data;

using Helium.Common.Emitters;
using Helium.Common.Emitters.Extensions;
using Helium.Mapping.Descriptors;
using Helium.Mapping.Emitters;
using Helium.Mapping.Emitters.Extensions;
using Helium.Provider;

namespace Helium.Mapping.Builders
{
    internal sealed class DbCollectionOfScalarTypeMapperBuilder : DbMapperBuilderBase<DbCollectionOfScalarTypeDescriptor>
    {
        public DbCollectionOfScalarTypeMapperBuilder(DbDataReaderTypeDescriptor dataReaderType, DbCollectionOfScalarTypeDescriptor returnType) :
            base(dataReaderType, returnType)
        { }

        protected override void Init(EmitterState state)
        {
            CommandBehavior = CommandBehavior.SingleResult;
        }

        protected override void Emit(DbMapperEmitter emitter)
        {
            // var reader = (TDataReader) arg0;
            emitter.EmitAssignDataReader();

            // var result = new Collection();
            emitter.EmitReadCollection(ReturnType, () =>
            {
                // while (reader.Read())
                emitter.EmitReadCollectionItems(ReturnType, () =>
                {
                    // var item = (T) reader.GetValue(0);
                    // result.Add(item);
                    emitter.EmitReadScalarOrDefault(ReturnType.ItemType, ordinal: 0);
                });
            });

            // return result;
            emitter.EmitReturn();
        }
    }
}
