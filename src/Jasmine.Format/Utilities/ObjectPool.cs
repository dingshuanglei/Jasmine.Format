using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Jasmine.Format.Utilities
{
    /// <summary>
    /// A thread-safe object pool for reusing objects to reduce GC pressure.
    /// Uses ConcurrentQueue for better performance in high-concurrency scenarios.
    /// </summary>
    /// <typeparam name="T">Type of objects to pool</typeparam>
    internal sealed class ObjectPool<T> where T : class
    {
        private readonly ConcurrentQueue<T> _pool;
        private readonly Func<T> _factory;
        private readonly int _maxSize;
        private int _currentCount;

        /// <summary>
        /// Initializes a new instance of the ObjectPool class
        /// </summary>
        /// <param name="factory">Function to create new instances</param>
        /// <param name="maxSize">Maximum pool size (default: 16)</param>
        /// <exception cref="ArgumentNullException">Thrown when factory is null</exception>
        public ObjectPool(Func<T> factory, int maxSize = 16)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _maxSize = maxSize;
            _pool = new ConcurrentQueue<T>();
            _currentCount = 0;
        }

        /// <summary>
        /// Gets an object from the pool, or creates a new one if the pool is empty.
        /// Thread-safe and lock-free for better concurrency performance.
        /// </summary>
        /// <returns>An instance of type T</returns>
        public T Get()
        {
            if (_pool.TryDequeue(out T item))
            {
                System.Threading.Interlocked.Decrement(ref _currentCount);
                return item;
            }
            return _factory();
        }

        /// <summary>
        /// Returns an object to the pool.
        /// Thread-safe and lock-free for better concurrency performance.
        /// </summary>
        /// <param name="item">The object to return</param>
        public void Return(T item)
        {
            if (item == null) return;
            
            // Clear the item before returning to pool
            if (item is List<object> list)
            {
                list.Clear();
            }
            
            // Only add if we haven't reached max size
            if (_currentCount < _maxSize)
            {
                System.Threading.Interlocked.Increment(ref _currentCount);
                _pool.Enqueue(item);
            }
        }

        /// <summary>
        /// Gets the current number of items in the pool.
        /// </summary>
        public int Count => _currentCount;
    }
}