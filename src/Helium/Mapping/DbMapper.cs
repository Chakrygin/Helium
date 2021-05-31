using System;
using System.Data;
using System.Data.Common;
using System.Reflection.Emit;

namespace Helium.Mapping
{
    internal sealed class DbMapper<T>
    {
        public DbMapper(DynamicMethod dm, object? target, CommandBehavior commandBehavior)
        {
            Map = (Func<DbDataReader, T>) dm
                .CreateDelegate(typeof(Func<DbDataReader, T>), target);

            CommandBehavior = commandBehavior;
        }

        public Func<DbDataReader, T> Map { get; }

        public CommandBehavior CommandBehavior { get; }
    }
}
