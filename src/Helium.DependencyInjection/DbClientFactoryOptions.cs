using System;
using System.Collections;
using System.Collections.Generic;

namespace Helium.DependencyInjection
{
    internal sealed class DbClientFactoryOptions
    {
        public IList<Action<DbClientOptionsBuilder, IServiceProvider>> ConfigureActions { get; } =
            new List<Action<DbClientOptionsBuilder, IServiceProvider>>();
    }
}
