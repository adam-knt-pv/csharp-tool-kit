using System.Buffers;
using System.Diagnostics.CodeAnalysis;

namespace ToolKit.Collections;

public struct Pool<T>
{
	T[] items;

	/// <inheritdoc cref="Vec{T}.Count"/>
	public int Count { get; private set; }

	Vec<int> pooled;

	/// <inheritdoc cref="Vec{T}.LastIndex"/>
	public int LastIndex => Count - 1;

	public T this[int at]
	{
		get => items[at];
		set => items[at] = value;
	}

	public static Pool<T> With(int capacity)
	{
#if ERR
		ArgumentOutOfRangeException.ThrowIfNegative(capacity);
#endif
		return new()
		{
			items = new T[capacity],
			Count = 0,
			pooled = Vec<int>.From(new int[capacity > 4 ? capacity / 2 : 10], 0),
		};
	}

	public static Pool<T> From(params T[] array) =>
		new()
		{
			items = array,
			Count = array.Length,
			pooled = Vec<int>.From(
				new int[array.Length > 4 ? array.Length / 2 : 10],
				0
			),
		};

	public static Pool<T> From(T[] array, int count)
	{
#if ERR
		ArgumentOutOfRangeException.ThrowIfNegative(count);
#endif
		return new()
		{
			items = array,
			Count = count,
			pooled = Vec<int>.From(new int[count > 4 ? count / 2 : 10], 0),
		};
	}

	public static Pool<T> From(T[] array, int count, Vec<int> pooled)
	{
#if ERR
		ArgumentOutOfRangeException.ThrowIfNegative(count);
#endif
		return new()
		{
			items = array,
			Count = count,
			pooled = pooled,
		};
	}

	public static Pool<T> Copy(T[] array, int add_capacity = 0)
	{
#if ERR
		ArgumentOutOfRangeException.ThrowIfNegative(add_capacity);
#endif
		var result = new Pool<T>
		{
			items = new T[array.Length + add_capacity],
			Count = array.Length,
			pooled = Vec<int>.From(
				new int[array.Length > 4 ? array.Length / 2 : 10],
				0
			),
		};

		array.CopyTo(result.items, 0);

		return result;
	}

	public static Pool<T> operator +(Pool<T> left, T right)
	{
		left.Add(right);
		return left;
	}

	/// <summary>
	/// Adds the given item to this collection.
	/// </summary>
	/// <param name="item"></param>
	public int Add(T item)
	{
		int i;

		if (pooled.Count == 0)
		{
			i = Count++;

			if (i == items.Length)
				Array.Resize(ref items, Count << 2);

			items[i] = item;
			return i;
		}

		i = pooled[pooled.LastIndex];
		items[i] = item;
		pooled.Pop();
		return i;
	}

	public static Pool<T> operator +(Pool<T> left, T[] right)
	{
		left.Add(right);
		return left;
	}

	/// <summary>
	/// Adds one or more items to this collection.
	/// </summary>
	/// <param name="items"></param>
	public void Add(params ReadOnlySpan<T> items)
	{
		foreach (var item in items)
			Add(item);
	}

	/// <summary>
	/// Removes the item at the given index.
	/// </summary>
	/// <param name="at"></param>
	public void Remove(int at)
	{
#if ERR
		ArgumentOutOfRangeException.ThrowIfNegative(at);
		ArgumentOutOfRangeException.ThrowIfGreaterThan(at, LastIndex);
#endif
		pooled.Append(at);
	}

	public static implicit operator T[](Pool<T> array) => array.ToArray();

	/// <inheritdoc cref="Vec{T}.ToArray"/>
	public T[] ToArray() => items[..Count];

	/// <inheritdoc cref="Vec{T}.AsArray"/>
	public T[] AsArray() => items;

	public Enumerator GetEnumerator()
	{
		var result = new Enumerator()
		{
			Items = items,
			Indexes = ArrayPool<int>.Shared.Rent(Count - pooled.Count),
			IndexesCount = Count - pooled.Count,
		};

		int ii = 0;
		foreach (var i in Count)
		{
			foreach (var index in pooled)
			{
				if (index == i)
					goto Next;
			}

			result.Indexes[ii++] = i;

			Next:
			;
		}

		return result;
	}

	public struct Enumerator()
	{
		int i = -1;
		internal T[] Items;
		internal int[] Indexes;
		internal int IndexesCount;

		public T Current => Items[Indexes[i]];

		public bool MoveNext()
		{
			if (++i != IndexesCount)
				return true;

			ArrayPool<int>.Shared.Return(Indexes);
			return false;
		}
	}
}
