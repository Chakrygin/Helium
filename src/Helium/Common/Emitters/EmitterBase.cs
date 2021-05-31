using System.Reflection.Emit;

namespace Helium.Common.Emitters
{
    internal abstract class EmitterBase
    {
        protected EmitterBase(ILGenerator il, EmitterState state)
        {
            IL = il;
            State = state;
            Locals = new EmitterLocals(il);
        }

        public ILGenerator IL { get; }

        public EmitterState State { get; }

        public EmitterLocals Locals { get; }
    }
}
