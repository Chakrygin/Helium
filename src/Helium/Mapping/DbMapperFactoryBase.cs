using System;
using System.Collections.Generic;
using System.Threading;

namespace Helium.Mapping
{
    internal abstract class DbMapperFactoryBase : IDisposable
    {
        private readonly ReaderWriterLockSlim _lock;
        private readonly Dictionary<Type, object> _objects;

        protected DbMapperFactoryBase()
        {
            _lock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
            _objects = new Dictionary<Type, object>();
        }

        protected object GetMapperWithLock(Type type)
        {
            _lock.EnterReadLock();

            try
            {
                if (_objects.TryGetValue(type, out var mapper))
                {
                    return mapper;
                }
            }
            finally
            {
                _lock.ExitReadLock();
            }

            _lock.EnterWriteLock();

            try
            {
                return GetMapperWithoutLock(type);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        protected object GetMapperWithoutLock(Type type)
        {
            if (!_objects.TryGetValue(type, out var mapper))
            {
                mapper = CreateMapper(type);
                _objects.Add(type, mapper);
            }

            return mapper;
        }

        protected abstract object CreateMapper(Type type);

        public void Dispose()
        {
            _lock.Dispose();
            _objects.Clear();
        }
    }
}
