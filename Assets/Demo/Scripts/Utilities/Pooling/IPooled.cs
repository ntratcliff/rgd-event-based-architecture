/*
 * Copied with some modifications from macsalad,
 * Aesthetician Labs' internal toolset.
 * Copyright 2018-2021 Aesthetician Labs.
 * Released under MIT license.
 */

namespace Demo.Utilities.Pooling
{
	/// <summary>
	/// An object that is pooled
	/// </summary>
	public interface IPooled
	{
		IPool Parent { get; }

		/// <summary>
		/// Action to be taken when pooled object is activated from pool
		/// </summary>
		void OnPoolLeft();

		/// <summary>
		/// Action to be taken when the pooled object enters the pool
		/// </summary>
		void OnPoolEntered();

		/// <summary>
		/// Action to be taken to return the object to the pool 
		/// (including calling <see cref="IPool{T}.Return(T)"/>)
		/// </summary>
		void ReturnToPool();
	}
}