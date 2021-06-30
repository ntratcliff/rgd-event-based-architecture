/*
 * Copied with some modifications from macsalad,
 * Aesthetician Labs' internal toolset.
 * Copyright 2018-2021 Aesthetician Labs.
 * Released under MIT license.
 */

using UnityEngine;

namespace Demo.Utilities.Pooling
{
	public class Pool : MonoBehaviour, IPool<GameObject>
	{
		/// <summary>
		/// The maximum number of objects this pool holds
		/// </summary>
		[Tooltip("The maximum number of objects this pool holds")]
		[Min(0)]
		public int Capacity = 10;

		/// <summary>
		/// The source object for pooled object instances.
		/// </summary>
		[Tooltip("The source object for pooled object instances.")]
		public GameObject Prefab;

		/// <summary>
		/// The parent of the pooled objects
		/// </summary>
		[Tooltip("The parent of the pooled objects")]
		public Transform Parent;

		protected IPool<GameObject> pool;

		int IPool<GameObject>.Capacity => pool.Capacity;
		public int Count => pool.Count;
		public int ActiveCount => pool.ActiveCount;
		public int InactiveCount => pool.InactiveCount;

		protected virtual void Awake()
		{
			InitPool();
		}

		/// <summary>
		/// Creates the internal <see cref="IPool{T}"/> instance and fills it
		/// with new instances of GameObject.
		/// </summary>
		protected virtual void InitPool()
		{
			pool = new Pool<GameObject>(Capacity);

			for (int i = 0; i < pool.Capacity; i++)
			{
				pool.Add(CreatePrefabInstance(i));
			}
		}

		protected virtual GameObject CreatePrefabInstance(int count)
		{
			GameObject o = Instantiate(Prefab);
			o.name = Prefab.name + " " + count;

			// assign transform parent
			if(Parent) o.transform.SetParent(Parent);

			// assign PooledObject parent
			if(o.TryGetComponent(out PooledObject pooled))
			{
				pooled.Parent = this;
			}

			return o;
		}

		/// <summary>
		/// Returns the first available object from the pool, if any.
		/// Returns default (null for nullable objects) if no inactive objects 
		/// in pool. If the object has an attached PooledObject, it will be 
		/// passed via <paramref name="po"/>.
		/// </summary>
		/// <param name="po">
		/// The attached <see cref="PooledObject"/>, if any.
		/// </param>
		public virtual GameObject Get(out PooledObject po)
		{
			GameObject go = Get();
			po = go.GetComponent<PooledObject>();
			return go;
		}


		/// <summary>
		/// Attempts to get an object from the pool.
		/// Returns whether or not the attempt was successful.
		/// If successful, the object is returned via <paramref name="go"/>
		/// If the object has an attached PooledObject, it will be 
		/// passed via <paramref name="po"/>.
		/// </summary>
		/// <param name="go">
		/// The object returned from the pool, if any.
		/// </param>
		/// <param name="po">
		/// The attached <see cref="PooledObject"/>, if any.
		/// </param>
		public virtual bool TryGet(out GameObject go, out PooledObject po)
		{
			po = null;
			if(TryGet(out go))
			{
				po = go.GetComponent<PooledObject>();
			}

			return go != null;
		}

		// Pool passthroughs

		/// <summary>
		/// Adds an object to pool as an inactive object
		/// </summary>
		public virtual void Add(GameObject o) => pool.Add(o);

		/// <summary>
		/// Returns an object to the pool.
		/// </summary>
		public virtual void Return(GameObject o) => pool.Return(o);

		/// <summary>
		/// Returns the first available object from the pool, if any.
		/// Returns null if no inactive objects in pool.
		/// </summary>
		public virtual GameObject Get() => pool.Get();

		/// <summary>
		/// Attempts to get an object from the pool.
		/// Returns whether or not the attempt was successful.
		/// If successful, the object is returned via <paramref name="o"/>
		/// </summary>
		public virtual bool TryGet(out GameObject o) => pool.TryGet(out o);
	}
}