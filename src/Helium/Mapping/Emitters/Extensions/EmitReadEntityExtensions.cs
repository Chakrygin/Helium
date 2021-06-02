using System;
using System.Reflection.Emit;

using Helium.Common.Emitters.Extensions;
using Helium.Mapping.Descriptors;

namespace Helium.Mapping.Emitters.Extensions
{
    internal static class EmitReadEntityExtensions
    {
        public static void EmitReadEntity(this DbMapperEmitterBase emitter,
            DbEntityTypeDescriptor entityType)
        {
            if (!entityType.IsValueType)
            {
                emitter.EmitReadClassEntity(entityType);
            }
            else
            {
                emitter.EmitReadStructEntity(entityType);
            }
        }

        private static void EmitReadClassEntity(this DbMapperEmitterBase emitter,
            DbEntityTypeDescriptor entityType)
        {
            var ordinal = emitter.Locals.Declare(typeof(int));

            emitter.EmitReadEntityParameters(entityType, ordinal);

            emitter.IL.Emit(OpCodes.Newobj, entityType.Constructor!);

            emitter.EmitReadEntityProperties(entityType, ordinal,
                () => emitter.IL.Emit(OpCodes.Dup));

            emitter.Locals.Release(ordinal);
        }

        private static void EmitReadStructEntity(this DbMapperEmitterBase emitter,
            DbEntityTypeDescriptor entityType)
        {
            var result = emitter.Locals.Declare(entityType);
            var ordinal = emitter.Locals.Declare(typeof(int));

            if (entityType.Constructor != null)
            {
                emitter.EmitReadEntityParameters(entityType, ordinal);

                emitter.IL.Emit(OpCodes.Newobj, entityType.Constructor);
                emitter.IL.Emit(OpCodes.Stloc, result);
            }
            else
            {
                emitter.IL.Emit(OpCodes.Ldloca, result);
                emitter.IL.Emit(OpCodes.Initobj, entityType);
            }

            emitter.EmitReadEntityProperties(entityType, ordinal,
                () => emitter.IL.Emit(OpCodes.Ldloca, result));

            emitter.IL.Emit(OpCodes.Ldloc, result);

            if (entityType.NullableConstructor != null)
            {
                emitter.IL.Emit(OpCodes.Newobj, entityType.NullableConstructor);
            }

            emitter.Locals.Release(result);
            emitter.Locals.Release(ordinal);
        }

        private static void EmitReadEntityParameters(this DbMapperEmitterBase emitter,
            DbEntityTypeDescriptor entityType, LocalBuilder ordinal)
        {
            foreach (var parameter in entityType.Parameters)
            {
                // var ordinal = ordinals[ordinalIndex]
                emitter.EmitAssignOrdinal(ordinal, parameter.OrdinalIndex);

                // if (ordinal >= 0 && !reader.IsDBNull(ordinal))
                emitter.EmitIfHasValue(ordinal,
                    // var arg = reader.GetValue<T>(ordinal);
                    () => emitter.EmitReadScalar(parameter.ParameterType, ordinal),
                    // var arg = default(T);
                    () => emitter.EmitDefault(parameter.ParameterType));
            }
        }

        private static void EmitReadEntityProperties(this DbMapperEmitterBase emitter,
            DbEntityTypeDescriptor entityType, LocalBuilder ordinal, Action emitLoadResult)
        {
            foreach (var property in entityType.Properties)
            {
                // var ordinal = ordinals[ordinalIndex]
                emitter.EmitAssignOrdinal(ordinal, property.OrdinalIndex);

                // if (ordinal >= 0 && !reader.IsDBNull(ordinal))
                emitter.EmitIfHasValue(ordinal, () =>
                {
                    // result.Property = reader.GetValue<T>(ordinal);
                    emitLoadResult();
                    emitter.EmitReadScalar(property.PropertyType, ordinal);
                    emitter.IL.Emit(OpCodes.Callvirt, property.Property.SetMethod);
                });
            }
        }

        private static void EmitAssignOrdinal(this DbMapperEmitterBase emitter,
            LocalBuilder ordinal, int index)
        {
            emitter.EmitLoadOrdinals();
            emitter.IL.EmitLdc(index);
            emitter.IL.Emit(OpCodes.Ldelem_I4);
            emitter.IL.Emit(OpCodes.Stloc, ordinal);
        }
    }
}
