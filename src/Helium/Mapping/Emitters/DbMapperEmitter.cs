using System;
using System.Collections.Generic;
using System.Reflection.Emit;

using Helium.Common.Emitters;
using Helium.Provider;

namespace Helium.Mapping.Emitters
{
    internal sealed class DbMapperEmitter : DbMapperEmitterBase
    {
        private LocalBuilder? _ordinalsLocal;

        public DbMapperEmitter(ILGenerator il, EmitterState state, DbDataReaderTypeDescriptor dataReaderType) :
            base(il, state, dataReaderType)
        { }

        public void EmitAssignOrdinals(Dictionary<string, int> ordinalIndices)
        {
            if (_ordinalsLocal != null)
                throw new InvalidOperationException("_ordinalsLocal != null");

            _ordinalsLocal = IL.DeclareLocal(typeof(int[]));

            EmitLoadDataReader();
            EmitLoadState(ordinalIndices);
            IL.Emit(OpCodes.Call, DbMappingUtils.GetOrdinalsMethod);
            IL.Emit(OpCodes.Stloc, _ordinalsLocal);
        }

        public override void EmitLoadOrdinals()
        {
            if (_ordinalsLocal == null)
                throw new InvalidOperationException("_ordinalsLocal == null");

            IL.Emit(OpCodes.Ldloc, _ordinalsLocal);
        }
    }
}
