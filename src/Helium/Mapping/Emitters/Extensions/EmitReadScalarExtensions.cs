using System;
using System.Reflection.Emit;

using Helium.Common.Emitters.Extensions;
using Helium.Mapping.Descriptors;

namespace Helium.Mapping.Emitters.Extensions
{
    internal static class EmitReadScalarExtensions
    {
        public static void EmitReadScalarOrDefault(this DbMapperEmitterBase emitter,
            DbScalarTypeDescriptor scalarType, int ordinal)
        {
            emitter.EmitIfHasValue(ordinal,
                () => emitter.EmitReadScalar(scalarType, ordinal),
                () => emitter.EmitDefault(scalarType));
        }

        public static void EmitReadScalar(this DbMapperEmitterBase emitter,
            DbScalarTypeDescriptor scalarType, int ordinal)
        {
            emitter.EmitReadScalar(scalarType,
                () => emitter.IL.EmitLdc(ordinal));
        }

        public static void EmitReadScalar(this DbMapperEmitterBase emitter,
            DbScalarTypeDescriptor scalarType, LocalBuilder ordinal)
        {
            emitter.EmitReadScalar(scalarType,
                () => emitter.IL.Emit(OpCodes.Ldloc, ordinal));
        }

        private static void EmitReadScalar(this DbMapperEmitterBase emitter,
            DbScalarTypeDescriptor scalarType, Action emitLoadOrdinal)
        {
            emitter.EmitLoadDataReader();

            emitLoadOrdinal();

            var method =
                emitter.DataReaderType.GetNativeValueMethod(scalarType.UnderlyingType) ??
                emitter.DataReaderType.GetFieldValueMethod(scalarType.UnderlyingType);

            emitter.IL.Emit(OpCodes.Callvirt, method);

            if (scalarType.NullableConstructor != null)
            {
                emitter.IL.Emit(OpCodes.Newobj, scalarType.NullableConstructor);
            }
        }
    }
}
