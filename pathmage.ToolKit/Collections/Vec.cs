using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace pathmage.ToolKit.Collections;

/// <summary>
/// Fast grow-only array.
/// </summary>
/// <typeparam name="T"></typeparam>
public struct Vec<T>
{
	[JsonInclude]
	T[] items;

	/// <summary>
	/// Describes how many elements are in this collection.
	/// </summary>
	public int Count { get; private set; }

	/// <summary>
	/// Returns the index of the last element in this collection, or -1 if the collection is empty.
	/// </summary>
	public int LastIndex => Count - 1;

	public T this[int at]
	{
		get => items[at];
		set => items[at] = value;
	}

	public static Vec<T> With(int capacity)
	{
#if ERR
		ArgumentOutOfRangeException.ThrowIfNegative(capacity);
#endif
		return new() { items = new T[capacity], Count = 0 };
	}

	public static Vec<T> From(params T[] array) =>
		new() { items = array, Count = array.Length };

	public static Vec<T> From(T[] array, int count)
	{
#if ERR
		ArgumentOutOfRangeException.ThrowIfNegative(count);
#endif
		return new() { items = array, Count = count };
	}

	public static Vec<T> Copy(T[] array, int add_capacity = 0)
	{
#if ERR
		ArgumentOutOfRangeException.ThrowIfNegative(add_capacity);
#endif
		var result = new Vec<T>
		{
			items = new T[array.Length + add_capacity],
			Count = array.Length,
		};

		array.CopyTo(result.items, 0);

		return result;
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

	public static Vec<T> operator +(Vec<T> left, T right)
	{
		left.Append(right);
		return left;
	}

	/// <summary>
	/// Adds the given item to the end of this collection.
	/// </summary>
	/// <param name="item"></param>
	public void Append(T item)
	{
		int i = Count++;

		if (i == items.Length)
			Array.Resize(ref items, Count << 2);

		items[i] = item;
	}

	public static Vec<T> operator +(Vec<T> left, T[] right)
	{
		left.Append(right);
		return left;
	}

	/// <summary>
	/// Adds one or more items to the end of this collection.
	/// </summary>
	/// <param name="items"></param>
	public void Append(params T[] items)
	{
		int i = Count;
		Count += items.Length;

		if (this.items.Length < Count)
		{
			int new_size = i;

			while (new_size < Count)
				new_size <<= 2;

			Array.Resize(ref this.items, new_size);
		}

		items.CopyTo(this.items, i);
	}

	public static Vec<T> operator --(Vec<T> array)
	{
		array.Pop();
		return array;
	}

	/// <summary>
	/// Removes the element from the end of this collection.
	/// </summary>
	public void Pop()
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

	public static implicit operator T[](Vec<T> array) => array.ToArray();

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

	public IEnumerator<T> GetEnumerator()
	{
		foreach (var i in Count)
			yield return items[i];
	}
}
