using System;
using System.Text;

namespace Jasmine.Format.Utilities
{
    /// <summary>
    /// A thread-safe cache for StringBuilder instances to reduce GC pressure.
    /// Uses ThreadStatic for per-thread caching, ensuring thread safety without locks.
    /// </summary>
    internal static class StringBuilderCache
    {
        // Maximum capacity to cache (4KB is a reasonable limit)
        private const int MaxCachedSize = 4096;
        
        // Default capacity for new StringBuilder instances
        private const int DefaultCapacity = 1024;

        // Per-thread cached instance (thread-safe by design)
        [ThreadStatic]
        private static StringBuilder _cachedInstance;

        // Track if we've ever cached for this thread (optimization)
        [ThreadStatic]
        private static bool _hasCached;

        /// <summary>
        /// Acquires a StringBuilder from the cache, or creates a new one.
        /// Thread-safe: each thread gets its own cached instance.
        /// </summary>
        /// <param name="capacity">Initial capacity of the StringBuilder (default: 1024)</param>
        /// <returns>A StringBuilder instance ready for use</returns>
        public static StringBuilder Acquire(int capacity = DefaultCapacity)
        {
            // Check if we have a cached instance with sufficient capacity
            if (_hasCached && _cachedInstance != null)
            {
                StringBuilder cached = _cachedInstance;
                if (cached.Capacity >= capacity)
                {
                    _cachedInstance = null;
                    _hasCached = false;
                    cached.Clear();
                    return cached;
                }
            }
            
            // Create new instance if no suitable cached one available
            return new StringBuilder(capacity);
        }

        /// <summary>
        /// Releases a StringBuilder back to the cache for reuse.
        /// Thread-safe: each thread caches its own instance.
        /// </summary>
        /// <param name="sb">The StringBuilder to release</param>
        public static void Release(StringBuilder sb)
        {
            if (sb == null) return;
            
            // Only cache if capacity is within limits (prevent memory bloat)
            if (sb.Capacity <= MaxCachedSize)
            {
                _cachedInstance = sb;
                _hasCached = true;
            }
        }

        /// <summary>
        /// Acquires a StringBuilder, executes an action, and releases it back to cache.
        /// Convenience method for common usage pattern.
        /// </summary>
        /// <param name="capacity">Initial capacity</param>
        /// <param name="action">Action to perform with the StringBuilder</param>
        /// <returns>The result string</returns>
        public static string GetString(int capacity, Action<StringBuilder> action)
        {
            StringBuilder sb = Acquire(capacity);
            try
            {
                action(sb);
                return sb.ToString();
            }
            finally
            {
                Release(sb);
            }
        }
    }
}