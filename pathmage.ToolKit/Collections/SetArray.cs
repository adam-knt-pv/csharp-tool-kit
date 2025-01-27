using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace pathmage.ToolKit.Collections;

/// <summary>
/// The order of the items doesn't matter and their indexes can change.
/// </summary>
/// <typeparam name="T"></typeparam>
public struct SetArray<T>
{
	[JsonInclude]
	T[] items;

	/// <inheritdoc cref="GrowArray{T}.Count"/>
	public int Count { get; private set; }

	/// <inheritdoc cref="GrowArray{T}.LastIndex"/>
	public int LastIndex => Count - 1;

	public T this[int at]
	{
		get => items[at];
		set => items[at] = value;
	}

	public static SetArray<T> With(int capacity)
	{
#if ERR
		ArgumentOutOfRangeException.ThrowIfNegative(capacity);
#endif
		return new() { items = new T[capacity], Count = 0 };
	}

	public static SetArray<T> From(params T[] array) =>
		new() { items = array, Count = array.Length };

	public static SetArray<T> From(T[] array, int count)
	{
#if ERR
		ArgumentOutOfRangeException.ThrowIfNegative(count);
#endif
		return new() { items = array, Count = count };
	}

	public static SetArray<T> Copy(T[] array, int add_capacity = 0)
	{
#if ERR
		ArgumentOutOfRangeException.ThrowIfNegative(add_capacity);
#endif
		var result = new SetArray<T>
		{
			items = new T[array.Length + add_capacity],
			Count = array.Length,
		};

		array.CopyTo(result.items, 0);

		return result;
	}

	public bool TryRemove(int at)
	{
		if (TryGet(at, out _))
		{
			Remove(at);
			return true;
		}

		return false;
	}

	/// <inheritdoc cref="GrowArray{T}.TryGet"/>
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

	public static SetArray<T> operator +(SetArray<T> left, T right)
	{
		left.Append(right);
		return left;
	}

	/// <inheritdoc cref="GrowArray{T}.Append(T)"/>
	public void Append(T item)
	{
		int i = Count++;

		if (i == items.Length)
			Array.Resize(ref items, Count << 2);

		items[i] = item;
	}

	public static SetArray<T> operator +(SetArray<T> left, T[] right)
	{
		left.Append(right);
		return left;
	}

	/// <inheritdoc cref="GrowArray{T}.Append(T[])"/>
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

	/// <summary>
	/// Replaces the item at the given index with the last item of this collection.
	/// </summary>
	/// <param name="at"></param>
	public void Remove(int at)
	{
#if ERR
		ArgumentOutOfRangeException.ThrowIfNegative(at);
		ArgumentOutOfRangeException.ThrowIfGreaterThan(at, LastIndex);
#endif
		items[at] = items[LastIndex];
		Pop();
	}

	public static SetArray<T> operator --(SetArray<T> array)
	{
		array.Pop();
		return array;
	}

	/// <inheritdoc cref="GrowArray{T}.Pop"/>
	public void Pop()
	{
		Count--;
#if ERR
		ArgumentOutOfRangeException.ThrowIfNegative(Count);
#endif
	}

	/// <inheritdoc cref="GrowArray{T}.GetRandom()"/>
	public T GetRandom() => this[Random.Shared.Next(Count)];

	/// <inheritdoc cref="GrowArray{T}.GetRandom(Random)"/>
	public T GetRandom(Random from) => this[from.Next(Count)];

	public static implicit operator T[](SetArray<T> array) => array.ToArray();

	/// <inheritdoc cref="GrowArray{T}.ToArray"/>
	public T[] ToArray() => items[..Count];

	/// <inheritdoc cref="GrowArray{T}.AsArray"/>
	public T[] AsArray() => items;

	public IEnumerator<T> GetEnumerator()
	{
		foreach (var i in Count)
			yield return items[i];
	}
}
