using System.Reflection;

namespace Helium.Common.Descriptors
{
    internal abstract class ParameterDescriptorBase
    {
        protected ParameterDescriptorBase(ParameterInfo parameter)
        {
            Parameter = parameter;
        }

        public ParameterInfo Parameter { get; }
    }
}
