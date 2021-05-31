// ReSharper disable once CheckNamespace

namespace System.Reflection.Emit
{
    internal static class ILGeneratorExtensions
    {
        public static void EmitLdarg(this ILGenerator il, int index)
        {
            if (index == 0)
            {
                il.Emit(OpCodes.Ldarg_0);
            }
            else if (index == 1)
            {
                il.Emit(OpCodes.Ldarg_1);
            }
            else if (index == 2)
            {
                il.Emit(OpCodes.Ldarg_2);
            }
            else if (index == 3)
            {
                il.Emit(OpCodes.Ldarg_3);
            }
            else if (0 <= index && index <= Byte.MaxValue)
            {
                il.Emit(OpCodes.Ldarg_S, index);
            }
            else
            {
                il.Emit(OpCodes.Ldarg, index);
            }
        }

        public static void EmitLdc(this ILGenerator il, int num)
        {
            if (num == 0)
            {
                il.Emit(OpCodes.Ldc_I4_0);
            }
            else if (num == 1)
            {
                il.Emit(OpCodes.Ldc_I4_1);
            }
            else if (num == 2)
            {
                il.Emit(OpCodes.Ldc_I4_2);
            }
            else if (num == 3)
            {
                il.Emit(OpCodes.Ldc_I4_3);
            }
            else if (num == 4)
            {
                il.Emit(OpCodes.Ldc_I4_4);
            }
            else if (num == 5)
            {
                il.Emit(OpCodes.Ldc_I4_5);
            }
            else if (num == 6)
            {
                il.Emit(OpCodes.Ldc_I4_6);
            }
            else if (num == 7)
            {
                il.Emit(OpCodes.Ldc_I4_7);
            }
            else if (num == 8)
            {
                il.Emit(OpCodes.Ldc_I4_8);
            }
            else if (num == -1)
            {
                il.Emit(OpCodes.Ldc_I4_M1);
            }
            else if (SByte.MinValue <= num && num <= SByte.MaxValue)
            {
                il.Emit(OpCodes.Ldc_I4_S, num);
            }
            else
            {
                il.Emit(OpCodes.Ldc_I4, num);
            }
        }
        //
        // public static void EmitCastOrUnbox(this ILGenerator il, Type type)
        // {
        //     il.Emit(type.IsValueType ? OpCodes.Unbox_Any : OpCodes.Castclass, type);
        // }
    }
}
