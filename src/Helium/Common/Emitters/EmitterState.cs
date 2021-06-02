using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Helium.Common.Emitters
{
    internal sealed class EmitterState
    {
        private object? _value;

        public bool IsEmpty => _value == null;

        public void Add(object value)
        {
            if (_value != null)
                throw new InvalidOperationException("_value != null");

            _value = value;
        }

        public void EmitLoad(ILGenerator il, object value)
        {
            if (_value != value)
                throw new InvalidOperationException("_value != value");

            il.Emit(OpCodes.Ldarg_0);
        }

        public object GetTarget()
        {
            if (_value == null)
                throw new InvalidOperationException("_value == null");

            return _value;
        }

        public Type GetTargetType()
        {
            if (_value == null)
                throw new InvalidOperationException("_value == null");

            return _value.GetType();
        }
    }
}
