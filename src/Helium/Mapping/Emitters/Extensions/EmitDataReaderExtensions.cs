using System;
using System.Reflection.Emit;

namespace Helium.Mapping.Emitters.Extensions
{
    internal static class EmitDataReaderExtensions
    {
        public static void EmitIfHasRows(this DbMapperEmitterBase emitter, Action emitThen)
        {
            var exit = emitter.IL.DefineLabel();

            // if (reader.HasRows)
            emitter.EmitLoadDataReader();
            emitter.IL.Emit(OpCodes.Callvirt, emitter.DataReaderType.HasRowsProperty.GetMethod);
            emitter.IL.Emit(OpCodes.Brfalse, exit);
            {
                emitThen();
            }

            emitter.IL.MarkLabel(exit);
        }

        public static void EmitIfRead(this DbMapperEmitterBase emitter, Action emitThen)
        {
            var exit = emitter.IL.DefineLabel();

            // if (reader.Read())
            emitter.EmitLoadDataReader();
            emitter.IL.Emit(OpCodes.Callvirt, emitter.DataReaderType.ReadMethod);
            emitter.IL.Emit(OpCodes.Brfalse, exit);
            {
                emitThen();
            }

            emitter.IL.MarkLabel(exit);
        }

        public static void EmitWhileRead(this DbMapperEmitterBase emitter, Action emitDo)
        {
            var exit = emitter.IL.DefineLabel();
            var loop = emitter.IL.DefineLabel();

            // while (reader.Read())
            emitter.IL.MarkLabel(loop);
            emitter.EmitLoadDataReader();
            emitter.IL.Emit(OpCodes.Callvirt, emitter.DataReaderType.ReadMethod);
            emitter.IL.Emit(OpCodes.Brfalse, exit);
            {
                emitDo();
                emitter.IL.Emit(OpCodes.Br, loop);
            }

            emitter.IL.MarkLabel(exit);
        }

        public static void EmitIfNextResult(this DbMapperEmitterBase emitter, Action emitThen)
        {
            var exit = emitter.IL.DefineLabel();

            // if (reader.Read())
            emitter.EmitLoadDataReader();
            emitter.IL.Emit(OpCodes.Callvirt, emitter.DataReaderType.NextResultMethod);
            emitter.IL.Emit(OpCodes.Brfalse, exit);
            {
                emitThen();
            }

            emitter.IL.MarkLabel(exit);
        }

        public static void EmitIfNextResult(this DbMapperEmitterBase emitter, Action emitThen, Action emitElse)
        {
            var exit = emitter.IL.DefineLabel();

            // if (!reader.IsDBNull(ordinal))
            emitter.EmitIfNextResult(() =>
            {
                emitThen();
                emitter.IL.Emit(OpCodes.Br, exit);
            });
            // else
            {
                emitElse();
            }

            emitter.IL.MarkLabel(exit);
        }

        public static void EmitIfHasValue(this DbMapperEmitterBase emitter,
            int ordinal, Action emitThen)
        {
            var exit = emitter.IL.DefineLabel();

            // if (!reader.IsDBNull(ordinal))
            emitter.EmitLoadDataReader();
            emitter.IL.EmitLdc(ordinal);
            emitter.IL.Emit(OpCodes.Callvirt, emitter.DataReaderType.IsDBNullMethod);
            emitter.IL.Emit(OpCodes.Brtrue, exit);
            {
                emitThen();
            }

            emitter.IL.MarkLabel(exit);
        }

        public static void EmitIfHasValue(this DbMapperEmitterBase emitter,
            int ordinal, LocalBuilder fieldCount, Action emitThen)
        {
            var exit = emitter.IL.DefineLabel();

            // if (ordinal < fieldCount)
            emitter.IL.EmitLdc(ordinal);
            emitter.IL.Emit(OpCodes.Ldloc, fieldCount);
            emitter.IL.Emit(OpCodes.Bge, exit);
            {
                // if (!reader.IsDBNull(ordinal))
                emitter.EmitLoadDataReader();
                emitter.IL.EmitLdc(ordinal);
                emitter.IL.Emit(OpCodes.Callvirt, emitter.DataReaderType.IsDBNullMethod);
                emitter.IL.Emit(OpCodes.Brtrue, exit);
                {
                    emitThen();
                }
            }

            emitter.IL.MarkLabel(exit);
        }

        public static void EmitIfHasValue(this DbMapperEmitterBase emitter,
            int ordinal, Action emitThen, Action emitElse)
        {
            var exit = emitter.IL.DefineLabel();

            // if (!reader.IsDBNull(ordinal))
            emitter.EmitIfHasValue(ordinal, () =>
            {
                emitThen();
                emitter.IL.Emit(OpCodes.Br, exit);
            });
            // else
            {
                emitElse();
            }

            emitter.IL.MarkLabel(exit);
        }

        public static void EmitIfHasValue(this DbMapperEmitterBase emitter,
            int ordinal, LocalBuilder fieldCount, Action emitThen, Action emitElse)
        {
            var exit = emitter.IL.DefineLabel();

            // if (ordinal < fieldCount &&!reader.IsDBNull(ordinal))
            emitter.EmitIfHasValue(ordinal, fieldCount, () =>
            {
                emitThen();
                emitter.IL.Emit(OpCodes.Br, exit);
            });
            // else
            {
                emitElse();
            }

            emitter.IL.MarkLabel(exit);
        }

        public static void EmitIfHasValue(this DbMapperEmitterBase emitter,
            LocalBuilder ordinal, Action emitThen)
        {
            var exit = emitter.IL.DefineLabel();

            // if (ordinal >= 0)
            emitter.IL.Emit(OpCodes.Ldloc, ordinal);
            emitter.IL.Emit(OpCodes.Ldc_I4_0);
            emitter.IL.Emit(OpCodes.Blt, exit);
            {
                // if (!reader.IsDBNull(ordinal))
                emitter.EmitLoadDataReader();
                emitter.IL.Emit(OpCodes.Ldloc, ordinal);
                emitter.IL.Emit(OpCodes.Callvirt, emitter.DataReaderType.IsDBNullMethod);
                emitter.IL.Emit(OpCodes.Brtrue_S, exit);
                {
                    emitThen();
                }
            }

            emitter.IL.MarkLabel(exit);
        }

        public static void EmitIfHasValue(this DbMapperEmitterBase emitter,
            LocalBuilder ordinal, Action emitThen, Action emitElse)
        {
            var exit = emitter.IL.DefineLabel();

            // if (ordinal >= 0 && !reader.IsDBNull(ordinal))
            emitter.EmitIfHasValue(ordinal, () =>
            {
                emitThen();
                emitter.IL.Emit(OpCodes.Br, exit);
            });
            // else
            {
                emitElse();
            }

            emitter.IL.MarkLabel(exit);
        }
    }
}
