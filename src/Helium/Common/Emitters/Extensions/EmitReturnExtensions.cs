using System.Reflection.Emit;

namespace Helium.Common.Emitters.Extensions
{
    internal static class EmitReturnExtensions
    {
        public static void EmitReturn(this EmitterBase emitter)
        {
            emitter.IL.Emit(OpCodes.Ret);
        }
    }
}
