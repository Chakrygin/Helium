using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Helium.Common.Emitters.Extensions
{
    internal static class EmitDefaultExtensions
    {
        private static readonly FieldInfo DefaultDecimalField = typeof(Decimal).GetField("Zero");
        private static readonly FieldInfo DefaultGuidField = typeof(Guid).GetField("Empty");

        public static void EmitDefault(this EmitterBase emitter, Type type)
        {
            if (!type.IsValueType)
            {
                // null
                emitter.IL.Emit(OpCodes.Ldnull);
                return;
            }

            var underlyingType = Nullable.GetUnderlyingType(type);
            if (underlyingType != null)
            {
                // default(T?)
                emitter.IL.Emit(OpCodes.Ldnull);
                emitter.IL.Emit(OpCodes.Unbox_Any, type);
                return;
            }

            if (type.IsEnum)
            {
                type = type.GetEnumUnderlyingType();
            }

            if (type == typeof(bool) ||
                type == typeof(byte) ||
                type == typeof(char) ||
                type == typeof(int) ||
                type == typeof(sbyte) ||
                type == typeof(short) ||
                type == typeof(uint) ||
                type == typeof(ushort))
            {
                // 0
                emitter.IL.Emit(OpCodes.Ldc_I4_0);
                return;
            }

            if (type == typeof(long) ||
                type == typeof(ulong))
            {
                // 0L
                emitter.IL.Emit(OpCodes.Ldc_I4_0);
                emitter.IL.Emit(OpCodes.Conv_I8);
                return;
            }

            if (type == typeof(float))
            {
                // 0.0f
                emitter.IL.Emit(OpCodes.Ldc_R4, 0.0f);
                return;
            }

            if (type == typeof(double))
            {
                // 0.0d
                emitter.IL.Emit(OpCodes.Ldc_R8, 0.0d);
                return;
            }

            if (type == typeof(decimal))
            {
                // Decimal.Zero
                emitter.IL.Emit(OpCodes.Ldsfld, DefaultDecimalField);
                return;
            }

            if (type == typeof(Guid))
            {
                // Guid.Empty
                emitter.IL.Emit(OpCodes.Ldsfld, DefaultGuidField);
                return;
            }

            var local = emitter.Locals.Declare(type);

            // default(T)
            emitter.IL.Emit(OpCodes.Ldloca, local);
            emitter.IL.Emit(OpCodes.Initobj, type);
            emitter.IL.Emit(OpCodes.Ldloc, local);

            emitter.Locals.Release(local);
        }
    }
}
