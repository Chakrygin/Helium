using System;
using System.Reflection.Emit;

using Helium.Mapping.Descriptors;

namespace Helium.Mapping.Emitters.Extensions
{
    internal static class EmitReadCollectionExtensions
    {
        public static void EmitReadCollection(this DbMapperEmitterBase emitter,
            DbCollectionTypeDescriptorBase collectionType, Action emitReadItems)
        {
            emitter.IL.Emit(OpCodes.Newobj, collectionType.Constructor);

            emitter.EmitIfHasRows(() =>
            {
                emitReadItems();
            });
        }

        public static void EmitReadCollectionItems(this DbMapperEmitterBase emitter,
            DbCollectionTypeDescriptorBase collectionType, Action emitReadItem)
        {
            emitter.EmitWhileRead(() =>
            {
                emitter.IL.Emit(OpCodes.Dup);

                emitReadItem();

                emitter.IL.Emit(OpCodes.Callvirt, collectionType.AddMethod);

                if (collectionType.AddMethod.ReturnType != typeof(void))
                {
                    emitter.IL.Emit(OpCodes.Pop);
                }
            });
        }
    }
}
