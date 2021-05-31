using System;

namespace Helium.Common.Emitters
{
    internal sealed class EmitterState
    {
        public bool IsEmpty => true;

        public object GetTarget()
        {
            throw new NotImplementedException();
        }

        public Type GetTargetType()
        {
            throw new NotImplementedException();
        }
    }
}
