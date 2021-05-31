using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Helium.Common.Emitters
{
    internal sealed class EmitterLocals
    {
        private readonly ILGenerator _il;
        private readonly List<LocalBuilder> _locals = new();

        public EmitterLocals(ILGenerator il)
        {
            _il = il;
        }

        public LocalBuilder Declare(Type localType)
        {
            foreach (var local in _locals)
            {
                if (local.LocalType == localType)
                {
                    _locals.Remove(local);
                    return local;
                }
            }

            return _il.DeclareLocal(localType);
        }

        public void Release(LocalBuilder local)
        {
            _locals.Add(local);
        }
    }
}
