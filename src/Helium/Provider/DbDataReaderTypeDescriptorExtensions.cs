using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;

namespace Helium.Provider
{
    internal static class DbDataReaderTypeDescriptorExtensions
    {
        public static DbDataReaderTypeDescriptor GetDataReaderTypeDescriptor(this DbProviderFactory providerFactory)
        {
            var command = providerFactory.CreateCommand();
            if (command == null)
                throw new InvalidOperationException("command == null");

            var commandType = command.GetType();
            var methods = commandType.GetMethods()
                .Where(x => x.Name == "ExecuteReader")
                .ToArray<MethodBase>();

            // ReSharper disable once PossibleNullReferenceException
            var method = (MethodInfo) Type.DefaultBinder
                .SelectMethod(BindingFlags.Default, methods, new[] {typeof(CommandBehavior)}, null);
            if (method == null)
                throw new InvalidOperationException("method == null");

            var dataReaderType = method.ReturnType;
            return new DbDataReaderTypeDescriptor(dataReaderType);
        }
    }
}
