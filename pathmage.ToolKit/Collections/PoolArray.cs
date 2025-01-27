using System.Buffers;
using System.Text.Json.Serialization;

namespace pathmage.ToolKit.Collections;

public struct PoolArray<T>
{
	[JsonInclude]
	T[] values;

	[JsonInclude]
	public int Count { get; private set; }

	[JsonInclude]
	GrowArray<int> pooled;

	[JsonIgnore]
	public int Length => values.Length;

	[JsonIgnore]
	public int LastIndex => Count - 1;

	[JsonIgnore]
	public bool IsEmpty => LastIndex == -1;

	public T this[int at]
	{
		get => values[at];
		set => values[at] = value;
	}

	public static PoolArray<T> New(int length)
	{
#if ERR
		ArgumentOutOfRangeException.ThrowIfNegative(length);
#endif
		return new()
		{
			values = new T[length],
			Count = 0,
			pooled = GrowArray<int>.NewFrom(
				new int[length > 4 ? length / 2 : 10],
				0
			),
		};
	}

	public static PoolArray<T> NewFrom(params T[] values) =>
		new()
		{
			values = values,
			Count = values.Length,
			pooled = GrowArray<int>.NewFrom(
				new int[values.Length > 4 ? values.Length / 2 : 10],
				0
			),
		};

	public static PoolArray<T> NewFrom(T[] values, int count)
	{
#if ERR
		ArgumentOutOfRangeException.ThrowIfNegative(count);
#endif
		return new()
		{
			values = values,
			Count = count,
			pooled = GrowArray<int>.NewFrom(
				new int[count > 4 ? count / 2 : 10],
				0
			),
		};
	}

	public static PoolArray<T> NewFrom(
		T[] values,
		int count,
		GrowArray<int> pooled
	)
	{
#if ERR
		ArgumentOutOfRangeException.ThrowIfNegative(count);
#endif
		return new()
		{
			values = values,
			Count = count,
			pooled = pooled,
		};
	}

	public static PoolArray<T> NewCopyFrom(T[] values, int add_length = 0)
	{
#if ERR
		ArgumentOutOfRangeException.ThrowIfNegative(add_length);
#endif
		var result = new PoolArray<T>
		{
			values = new T[values.Length + add_length],
			Count = values.Length,
			pooled = GrowArray<int>.NewFrom(
				new int[values.Length > 4 ? values.Length / 2 : 10],
				0
			),
		};

		values.CopyTo(result.values, 0);

		return result;
	}

	public int Add(T item)
	{
		int i;

		if (pooled.Count == 0)
		{
			i = Count++;

			if (i == values.Length)
				Array.Resize(ref values, Count << 2);

			values[i] = item;
			return i;
		}

		i = pooled[pooled.LastIndex];
		values[i] = item;
		pooled.Pop();
		return i;
	}

	public void Add(params T[] items)
	{
		foreach (var item in items)
			Add(item);
	}

	public void Remove(int at)
	{
#if ERR
		ArgumentOutOfRangeException.ThrowIfNegative(at);
		ArgumentOutOfRangeException.ThrowIfGreaterThan(at, LastIndex);
#endif
		pooled.Append(at);
	}

	/// <inheritdoc cref="GrowArray{T}.ToArray"/>
	public T[] ToArray() => values[..Count];

	/// <inheritdoc cref="GrowArray{T}.AsArray"/>
	public T[] AsArray() => values;

	public Enumerator GetEnumerator()
	{
		var result = new Enumerator()
		{
			Items = values,
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
