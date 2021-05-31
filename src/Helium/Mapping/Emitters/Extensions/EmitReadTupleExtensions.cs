using System.Reflection.Emit;

using Helium.Common.Emitters.Extensions;
using Helium.Mapping.Descriptors;

namespace Helium.Mapping.Emitters.Extensions
{
    internal static class EmitReadTupleExtensions
    {
        public static void EmitReadTuple(this DbMapperEmitterBase emitter,
            DbTupleTypeDescriptor tupleType)
        {
            if (!tupleType.IsValueType)
            {
                emitter.EmitReadClassTuple(tupleType);
            }
            else
            {
                emitter.EmitReadStructTuple(tupleType);
            }
        }

        private static void EmitReadClassTuple(this DbMapperEmitterBase emitter,
            DbTupleTypeDescriptor tupleType)
        {
            emitter.EmitReadTupleItems(tupleType);

            emitter.IL.Emit(OpCodes.Newobj, tupleType.Constructor);
        }

        private static void EmitReadStructTuple(this DbMapperEmitterBase emitter,
            DbTupleTypeDescriptor tupleType)
        {
            var result = emitter.Locals.Declare(tupleType);

            emitter.EmitReadTupleItems(tupleType);

            emitter.IL.Emit(OpCodes.Newobj, tupleType.Constructor);
            emitter.IL.Emit(OpCodes.Stloc, result);
            emitter.IL.Emit(OpCodes.Ldloc, result);

            if (tupleType.NullableConstructor != null)
            {
                emitter.IL.Emit(OpCodes.Newobj, tupleType.NullableConstructor);
            }

            emitter.Locals.Release(result);
        }

        private static void EmitReadTupleItems(this DbMapperEmitterBase emitter,
            DbTupleTypeDescriptor tupleType)
        {
            var fieldCount = emitter.Locals.Declare(typeof(int));

            // var fieldCount = reader.FieldCount;
            emitter.EmitAssignFieldCount(fieldCount);

            var ordinal = 0;
            foreach (var itemType in tupleType.ItemTypes)
            {
                // if (ordinal <= fieldCount && !reader.IsDBNull(ordinal))
                emitter.EmitIfHasValue(ordinal, fieldCount,
                    // var arg = reader.GetValue<T>(ordinal);
                    () => emitter.EmitReadScalar(itemType, ordinal++),
                    // var arg = default(T);
                    () => emitter.EmitDefault(itemType));
            }

            emitter.Locals.Release(fieldCount);
        }

        private static void EmitAssignFieldCount(this DbMapperEmitterBase emitter,
            LocalBuilder fieldCount)
        {
            emitter.EmitLoadDataReader();
            emitter.IL.Emit(OpCodes.Callvirt, emitter.DataReaderType.FieldCountProperty.GetMethod);
            emitter.IL.Emit(OpCodes.Stloc, fieldCount);
        }
    }
}
