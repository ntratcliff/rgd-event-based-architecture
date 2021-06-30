/*
 * Copied with some modifications from macsalad,
 * Aesthetician Labs' internal toolset.
 * Copyright 2018-2021 Aesthetician Labs.
 * Released under MIT license.
 */

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Utilities.Pooling
{
    /// <summary>
    /// A pool of objects
    /// </summary>
    public class Pool<T> : IPool<T>
    {
        public Queue<T> Inactive { get; }
        public List<T> Active { get; }

        public Pool(int size)
        {
            Capacity = size;
            Inactive = new Queue<T>(size);
            Active = new List<T>(size);
        }

        /// <summary>
        /// The total number of objects this pool can hold
        /// </summary>
        public int Capacity { get; }

        public int Count => ActiveCount + InactiveCount;

        /// <summary>
        /// The total number of active objects
        /// </summary>
        public int ActiveCount => Active.Count;

        /// <summary>
        /// The total number of inactive objects
        /// </summary>
        public int InactiveCount => Inactive.Count;

        /// <summary>
        /// Adds an object to pool as an inactive object
        /// </summary>
        public virtual void Add(T o)
        {
            if (Count < Capacity)
            {
                Inactive.Enqueue(o);

                if (TryGetPooled(o, out var p)) p.OnPoolEntered();
            }
            else
            {
                throw new Exception(
                    "Can't add object because this pool is full!"
                );
            }
        }

        /// <summary>
        /// Returns the first available object from the pool, if any.
        /// Returns default (null for nullable objects)
        /// if no inactive objects in pool.
        /// </summary>
        public virtual T Get()
        {
            if (InactiveCount > 0)
            {
                T o = Inactive.Dequeue();
                Active.Add(o);

                if (TryGetPooled(o, out var p)) p.OnPoolLeft();

                return o;
            }

            return default;
        }

        /// <summary>
        /// Attempts to get an object from the pool.
        /// Returns whether or not the attempt was successful.
        /// If successful, the object is returned via <paramref name="o" />
        /// </summary>
        public virtual bool TryGet(out T o)
        {
            o = Get();
            return !EqualityComparer<T>.Default.Equals(o, default);
        }

        /// <summary>
        /// Returns an object to the pool.
        /// </summary>
        public virtual void Return(T o)
        {
            bool isActive = Active.Contains(o);

            if (InactiveCount < Capacity && isActive)
            {
                Active.Remove(o);
                Inactive.Enqueue(o);

                if (TryGetPooled(o, out var p)) p.OnPoolEntered();
            }
            else if (!isActive)
            {
                throw new Exception(
                    "Can't return object {0} because it isn't in the active pool."
                );
            }
            else if (InactiveCount >= Capacity)
            {
                throw new Exception(
                    "Can't return object as inactive list is full! " +
                    "This shouldn't happen unless you're attempting to return " +
                    "objects that aren't in the pool."
                );
            }
        }

        /// <summary>
        /// Tries to extract an instance of <see cref="IPooled" />
        /// from <paramref name="o" />.
        /// </summary>
        protected virtual bool TryGetPooled(T o, out IPooled pooled)
        {
            switch (o)
            {
                // try simple cast first
                case IPooled p:
                    pooled = p;
                    return true;
                // try getting as attached component
                case GameObject go when go.TryGetComponent(out pooled):
                    return true;
                default:
                    pooled = null;
                    return false;
            }
        }
    }
}