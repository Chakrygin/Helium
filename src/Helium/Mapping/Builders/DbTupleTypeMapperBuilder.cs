using System.Data;

using Helium.Common.Emitters;
using Helium.Common.Emitters.Extensions;
using Helium.Mapping.Descriptors;
using Helium.Mapping.Emitters;
using Helium.Mapping.Emitters.Extensions;
using Helium.Provider;

namespace Helium.Mapping.Builders
{
    internal sealed class DbTupleTypeMapperBuilder : DbMapperBuilderBase<DbTupleTypeDescriptor>
    {
        public DbTupleTypeMapperBuilder(DbDataReaderTypeDescriptor dataReaderType, DbTupleTypeDescriptor returnType) :
            base(dataReaderType, returnType)
        { }

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
                    // return (...);
                    emitter.EmitReadTuple(ReturnType);
                    emitter.EmitReturn();
                });
            });

            // return default(T);
            emitter.EmitDefault(ReturnType);
            emitter.EmitReturn();
        }
    }
}
