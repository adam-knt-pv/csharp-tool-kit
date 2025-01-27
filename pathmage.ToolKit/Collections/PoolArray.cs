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
	GrowArray<int> free_idxes;

	[JsonIgnore]
	public int Length => values.Length;

	[JsonIgnore]
	public int LastIndex => Count - 1;

	[JsonIgnore]
	public bool IsEmpty => LastIndex == -1;

	public T this[int idx]
	{
		get => values[idx];
		set => values[idx] = value;
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
			free_idxes = GrowArray<int>.NewFrom(
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
			free_idxes = GrowArray<int>.NewFrom(
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
			free_idxes = GrowArray<int>.NewFrom(
				new int[count > 4 ? count / 2 : 10],
				0
			),
		};
	}

	public static PoolArray<T> NewFrom(
		T[] values,
		int count,
		GrowArray<int> free_idxes
	)
	{
#if ERR
		ArgumentOutOfRangeException.ThrowIfNegative(count);
#endif
		return new()
		{
			values = values,
			Count = count,
			free_idxes = free_idxes,
		};
	}

	public static PoolArray<T> NewFromCopyOf(T[] values, int add_length = 0)
	{
#if ERR
		ArgumentOutOfRangeException.ThrowIfNegative(add_length);
#endif
		var output = new PoolArray<T>
		{
			values = new T[values.Length + add_length],
			Count = values.Length,
			free_idxes = GrowArray<int>.NewFrom(
				new int[values.Length > 4 ? values.Length / 2 : 10],
				0
			),
		};

		values.CopyTo(output.values, 0);

		return output;
	}

	public int Add(T value)
	{
		var new_idx = 0;

		if (free_idxes.Count == 0)
		{
			new_idx = Count++;

			if (new_idx == values.Length)
				Array.Resize(ref values, Count << 2);

			values[new_idx] = value;
			return new_idx;
		}

		new_idx = free_idxes[free_idxes.LastIndex];
		values[new_idx] = value;

		free_idxes.Pop();
		return new_idx;
	}

	public void Add(params T[] values)
	{
		foreach (var item in values)
			Add(item);
	}

	public void Remove(int idx)
	{
#if ERR
		ArgumentOutOfRangeException.ThrowIfNegative(idx);
		ArgumentOutOfRangeException.ThrowIfGreaterThan(idx, LastIndex);
#endif
		free_idxes.Append(idx);
	}

	public T[] ToArray() => values[..Count];

	public T[] AsArray() => values;

	public Enumerator GetEnumerator()
	{
		var output = new Enumerator()
		{
			Items = values,
			Indexes = ArrayPool<int>.Shared.Rent(Count - free_idxes.Count),
			IndexesCount = Count - free_idxes.Count,
		};

		var ii = 0;
		foreach (var i in Count)
		{
			foreach (var index in free_idxes)
			{
				if (index == i)
					goto Next;
			}

			output.Indexes[ii++] = i;

			Next:
			;
		}

		return output;
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
