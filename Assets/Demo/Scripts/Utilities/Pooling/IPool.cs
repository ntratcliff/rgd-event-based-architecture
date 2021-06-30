/*
 * Copied with some modifications from macsalad,
 * Aesthetician Labs' internal toolset.
 * Copyright 2018-2021 Aesthetician Labs.
 * Released under MIT license.
 */

namespace Demo.Utilities.Pooling
{
	public interface IPool<T>
	{
		int Capacity { get; }
		int Count { get; }
		int ActiveCount { get; }
		int InactiveCount { get; }

		void Add(T o);
		void Return(T o);
		T Get();
		bool TryGet(out T o);
	}

	public interface IPool : IPool<object> { }
}