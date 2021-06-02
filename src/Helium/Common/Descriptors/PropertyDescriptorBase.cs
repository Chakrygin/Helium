using System.Reflection;

namespace Helium.Common.Descriptors
{
    internal abstract class PropertyDescriptorBase
    {
        protected PropertyDescriptorBase(PropertyInfo property)
        {
            Property = property;
        }

        public PropertyInfo Property { get; }
    }
}
