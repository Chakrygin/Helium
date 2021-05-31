using System.Data;

using Helium.Common.Emitters;
using Helium.Common.Emitters.Extensions;
using Helium.Mapping.Descriptors;
using Helium.Mapping.Emitters;
using Helium.Mapping.Emitters.Extensions;
using Helium.Provider;

namespace Helium.Mapping.Builders
{
    internal sealed class DbScalarTypeMapperBuilder : DbMapperBuilderBase<DbScalarTypeDescriptor>
    {
        public DbScalarTypeMapperBuilder(DbDataReaderTypeDescriptor dataReaderType, DbScalarTypeDescriptor returnType) :
            base(dataReaderType, returnType)
        {
        }

        protected override void Init(EmitterState state)
        {
            CommandBehavior = CommandBehavior.SingleResult | CommandBehavior.SingleRow;
        }

        protected override void Emit(DbMapperEmitter emitter)
        {
            // var reader = (TDataReader) arg0;
            emitter.EmitAssignDataReader();

            // if (reader.HasRows)
            emitter.EmitIfHasRows(() =>
            {
                // if (reader.Read())
                emitter.EmitIfRead(() =>
                {
                    const int ordinal = 0;

                    // if (!reader.IsDBNull(0))
                    emitter.EmitIfHasValue(ordinal, () =>
                    {
                        // return (T) reader.GetValue(0);
                        emitter.EmitReadScalar(ReturnType, ordinal);
                        emitter.EmitReturn();
                    });
                });
            });

            // return default(T);
            emitter.EmitDefault(ReturnType);
            emitter.EmitReturn();
        }
    }
}
