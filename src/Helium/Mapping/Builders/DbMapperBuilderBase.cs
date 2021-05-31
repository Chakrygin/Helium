using System;
using System.Data;
using System.Data.Common;
using System.Reflection.Emit;

using Helium.Common.Descriptors;
using Helium.Common.Emitters;
using Helium.Mapping.Emitters;
using Helium.Provider;

namespace Helium.Mapping.Builders
{
    internal abstract class DbMapperBuilderBase<TReturnTypeDescriptor>
        where TReturnTypeDescriptor : TypeDescriptorBase
    {
        protected DbMapperBuilderBase(DbDataReaderTypeDescriptor dataReaderType, TReturnTypeDescriptor returnType)
        {
            DataReaderType = dataReaderType;
            ReturnType = returnType;
        }

        public DbDataReaderTypeDescriptor DataReaderType { get; }

        public TReturnTypeDescriptor ReturnType { get; }

        public CommandBehavior CommandBehavior { get; protected set; }

        public object CreateMapper()
        {
            var state = new EmitterState();

            Init(state);

            var dm = CreateDynamicMethod(ReturnType, state);
            var il = dm.GetILGenerator();

            var emitter = new DbMapperEmitter(il, state, DataReaderType);

            Emit(emitter);

            var mapperType = typeof(DbMapper<>)
                .MakeGenericType(ReturnType);

            var target = state.IsEmpty ? null : state.GetTarget();
            var mapper = Activator.CreateInstance(mapperType, dm, target, CommandBehavior);

            return mapper;
        }

        protected abstract void Init(EmitterState state);

        protected abstract void Emit(DbMapperEmitter emitter);

        private static DynamicMethod CreateDynamicMethod(Type returnType, EmitterState state)
        {
            var parameterTypes = state.IsEmpty
                ? new[] {typeof(DbDataReader)}
                : new[] {state.GetTargetType(), typeof(DbDataReader)};

            return new DynamicMethod("", returnType, parameterTypes, restrictedSkipVisibility: true);
        }
    }
}
