using System;
using System.Reflection.Emit;

using Helium.Common.Emitters;
using Helium.Provider;

namespace Helium.Mapping.Emitters
{
    internal abstract class DbMapperEmitterBase : EmitterBase
    {
        private LocalBuilder? _dataReaderLocal;

        protected DbMapperEmitterBase(ILGenerator il, EmitterState state, DbDataReaderTypeDescriptor dataReaderType) :
            base(il, state)
        {
            DataReaderType = dataReaderType;
        }

        public DbDataReaderTypeDescriptor DataReaderType { get; }

        public void EmitAssignDataReader()
        {
            if (_dataReaderLocal != null)
                throw new InvalidOperationException("_dataReaderLocal != null");

            _dataReaderLocal = IL.DeclareLocal(DataReaderType);

            IL.Emit(State.IsEmpty ? OpCodes.Ldarg_0 : OpCodes.Ldarg_1);
            IL.Emit(OpCodes.Castclass, DataReaderType);
            IL.Emit(OpCodes.Stloc, _dataReaderLocal);
        }

        public void EmitLoadDataReader()
        {
            if (_dataReaderLocal == null)
                throw new InvalidOperationException("_dataReaderLocal == null");

            IL.Emit(OpCodes.Ldloc, _dataReaderLocal);
        }
    }
}
