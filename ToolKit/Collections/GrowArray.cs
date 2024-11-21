using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ToolKit.Collections;

/// <summary>
/// Fast grow-only array.
/// </summary>
/// <typeparam name="T"></typeparam>
public struct GrowArray<T>
{
	T[] items;

	/// <summary>
	/// Describes how many elements are in this collection.
	/// </summary>
	public int Count { get; private set; }

	/// <summary>
	/// Returns the index of the last element in this collection, or -1 if the collection is empty.
	/// </summary>
	public int LastIndex => Count - 1;

	public T this[int i]
	{
		get => items[i];
		set => items[i] = value;
	}

	public GrowArray()
		: this(32) { }

	public GrowArray(int capacity)
	{
#if ERR
		ArgumentOutOfRangeException.ThrowIfNegative(capacity);
#endif
		items = new T[capacity];
		Count = 0;
	}

	public GrowArray(T[] from)
	{
		items = from;
		Count = from.Length;
	}

	public GrowArray(T[] copy, int add_capacity)
	{
#if ERR
		ArgumentOutOfRangeException.ThrowIfNegative(add_capacity);
#endif
		items = new T[copy.Length + add_capacity];
		copy.CopyTo(items, 0);
		Count = copy.Length;
	}

	/// <summary>
	/// Attempts to get the item at the specified index, if the index is within the bounds of this collection.
	/// </summary>
	/// <param name="at">The index of the item to retrieve.</param>
	/// <param name="item">When this method returns, contains the item at the specified index, or the default value of <typeparamref name="T"/> if the index is out of bounds.</param>
	/// <returns>
	/// <c>true</c> if the item was successfully retrieved; otherwise, <c>false</c> if the index is out of bounds.
	/// </returns>
	public bool TryGet(int at, [NotNullWhen(true)] out T item)
	{
		if (at > 0 && at < Count)
		{
			item = items[at]!;
			return true;
		}
		item = default!;
		return false;
	}

	/// <summary>
	/// Adds the given item to the end of this collection.
	/// </summary>
	/// <param name="item"></param>
	public void Add(T item)
	{
		int i = Count++;

		if (i == items.Length)
			Array.Resize(ref items, Count << 2);

		items[i] = item;
	}

	/// <summary>
	/// Adds one or more items to the end of this collection.
	/// </summary>
	/// <param name="range"></param>
	public void AddRange(params T[] range)
	{
		int i = Count;
		Count += range.Length;

		if (items.Length < Count)
		{
			int new_size = i;

			while (new_size < Count)
				new_size <<= 2;

			Array.Resize(ref items, new_size);
		}

		range.CopyTo(items, i);
	}

	/// <summary>
	/// Removes the element from the end of this collection.
	/// </summary>
	public void RemoveLast()
	{
		Count--;
#if ERR
		ArgumentOutOfRangeException.ThrowIfNegative(Count);
#endif
	}

	/// <summary>
	/// Gets the item at a random index within this collection.
	/// </summary>
	/// <returns>The item at a random index.</returns>
	public T GetRandom() => this[Random.Shared.Next(Count)];

	/// <inheritdoc cref="GetRandom()"/>
	public T GetRandom(Random from) => this[from.Next(Count)];

	public IEnumerator<T> GetEnumerator()
	{
		for (int i = 0; i < Count; i++)
			yield return items[i];
	}

	/// <summary>
	/// Returns a new array containing the items of this collection up to the current count.
	/// </summary>
	/// <returns></returns>
	public T[] ToArray() => items[..Count];

	/// <summary>
	/// Returns the underlying array that stores the items of this collection.
	/// </summary>
	/// <returns></returns>
	public T[] AsArray() => items;
}
