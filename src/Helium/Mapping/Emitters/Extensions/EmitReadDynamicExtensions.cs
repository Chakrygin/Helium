using System.Reflection.Emit;

namespace Helium.Mapping.Emitters.Extensions
{
    internal static class EmitReadDynamicExtensions
    {
        public static void EmitReadDynamic(this DbMapperEmitterBase emitter)
        {
            emitter.EmitLoadDataReader();
            emitter.IL.Emit(OpCodes.Call, DbMappingUtils.MapDynamicMethod);
        }
    }
}
