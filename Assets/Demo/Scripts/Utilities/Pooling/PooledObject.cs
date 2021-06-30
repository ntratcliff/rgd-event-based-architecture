/*
 * Copied with some modifications from macsalad,
 * Aesthetician Labs' internal toolset.
 * Copyright 2018-2021 Aesthetician Labs.
 * Released under MIT license.
 */

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Demo.Utilities.Pooling
{
    public class PooledObject : MonoBehaviour, IPooled
    {
        [NonSerialized] public Pool Parent;

        public bool AutoReturnToPool = true;
        public float ReturnDelay = 1f;

        public UnityEvent PoolEntered;
        public UnityEvent PoolLeft;

        IPool IPooled.Parent => (IPool) Parent;

        /// <summary>
        /// Called by <see cref="Parent"/> when this object enters the pool.
        /// </summary>
        public virtual void OnPoolEntered()
        {
            PoolEntered.Invoke();
        }

        /// <summary>
        /// Called by <see cref="Parent"/> when this object leaves the pool.
        /// </summary>
        public virtual void OnPoolLeft()
        {
            PoolLeft.Invoke();

            if (AutoReturnToPool) StartCoroutine(AutoReturnRoutine());
        }

        private IEnumerator AutoReturnRoutine()
        {
            yield return new WaitForSeconds(ReturnDelay);
            Parent.Return(gameObject);
        }

        /// <summary>
        /// Returns this object to the parent pool.
        /// </summary>
        public virtual void ReturnToPool() => Parent.Return(gameObject);
    }
}