using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace pathmage.ToolKit.Collections;

public struct SetArray<T>
{
	[JsonInclude]
	T[] values;

	[JsonInclude]
	public int Count { get; private set; }

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

	public static SetArray<T> New(int length)
	{
#if ERR
		ArgumentOutOfRangeException.ThrowIfNegative(length);
#endif
		return new() { values = new T[length], Count = 0 };
	}

	public static SetArray<T> NewFrom(params T[] values) =>
		new() { values = values, Count = values.Length };

	public static SetArray<T> NewFrom(T[] values, int count)
	{
#if ERR
		ArgumentOutOfRangeException.ThrowIfNegative(count);
#endif
		return new() { values = values, Count = count };
	}

	public static SetArray<T> NewFromCopyOf(T[] values, int add_length = 0)
	{
#if ERR
		ArgumentOutOfRangeException.ThrowIfNegative(add_length);
#endif
		var output = new SetArray<T>
		{
			values = new T[values.Length + add_length],
			Count = values.Length,
		};

		values.CopyTo(output.values, 0);

		return output;
	}

	public bool TryGet(int idx, [NotNullWhen(true)] out T value)
	{
		if (idx > 0 && idx < Count)
		{
			value = values[idx]!;
			return true;
		}

		value = default!;
		return false;
	}

	public bool TryRemove(int idx)
	{
		if (TryGet(idx, out _))
		{
			Remove(idx);
			return true;
		}

		return false;
	}

	public void Append(T value)
	{
		var new_idx = Count++;

		if (new_idx == values.Length)
			Array.Resize(ref values, Count << 2);

		values[new_idx] = value;
	}

	public void Append(params T[] values)
	{
		var new_idx = Count;
		Count += values.Length;

		if (this.values.Length < Count)
		{
			var new_size = new_idx;

			while (new_size < Count)
				new_size <<= 2;

			Array.Resize(ref this.values, new_size);
		}

		values.CopyTo(this.values, new_idx);
	}

	public void Remove(int idx)
	{
#if ERR
		ArgumentOutOfRangeException.ThrowIfNegative(idx);
		ArgumentOutOfRangeException.ThrowIfGreaterThan(idx, LastIndex);
#endif
		values[idx] = values[LastIndex];
		Pop();
	}

	public void Pop()
	{
		Count--;
#if ERR
		ArgumentOutOfRangeException.ThrowIfNegative(Count);
#endif
	}

	public T GetRandom() => this[Random.Shared.Next(Count)];

	public T GetRandom(Random random) => this[random.Next(Count)];

	public T[] ToArray() => values[..Count];

	public T[] AsArray() => values;

	public IEnumerator<T> GetEnumerator()
	{
		foreach (var i in Count)
			yield return values[i];
	}
}
