using System.Reflection.Emit;

using Helium.Common.Emitters;
using Helium.Provider;

namespace Helium.Mapping.Emitters
{
    internal sealed class DbMapperEmitter : DbMapperEmitterBase
    {
        public DbMapperEmitter(ILGenerator il, EmitterState state, DbDataReaderTypeDescriptor dataReaderType) :
            base(il, state, dataReaderType)
        {
        }
    }
}
