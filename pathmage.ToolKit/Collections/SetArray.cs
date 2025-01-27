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

	public T this[int at]
	{
		get => values[at];
		set => values[at] = value;
	}

	public static SetArray<T> With(int length)
	{
#if ERR
		ArgumentOutOfRangeException.ThrowIfNegative(length);
#endif
		return new() { values = new T[length], Count = 0 };
	}

	public static SetArray<T> From(params T[] array) =>
		new() { values = array, Count = array.Length };

	public static SetArray<T> From(T[] array, int count)
	{
#if ERR
		ArgumentOutOfRangeException.ThrowIfNegative(count);
#endif
		return new() { values = array, Count = count };
	}

	public static SetArray<T> Copy(T[] array, int add_capacity = 0)
	{
#if ERR
		ArgumentOutOfRangeException.ThrowIfNegative(add_capacity);
#endif
		var result = new SetArray<T>
		{
			values = new T[array.Length + add_capacity],
			Count = array.Length,
		};

		array.CopyTo(result.values, 0);

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

	public bool TryGet(int at, [NotNullWhen(true)] out T item)
	{
		if (at > 0 && at < Count)
		{
			item = values[at]!;
			return true;
		}
		item = default!;
		return false;
	}

	public void Append(T item)
	{
		int i = Count++;

		if (i == values.Length)
			Array.Resize(ref values, Count << 2);

		values[i] = item;
	}

	public void Append(params T[] items)
	{
		int i = Count;
		Count += items.Length;

		if (this.values.Length < Count)
		{
			int new_size = i;

			while (new_size < Count)
				new_size <<= 2;

			Array.Resize(ref this.values, new_size);
		}

		items.CopyTo(this.values, i);
	}

	public void Remove(int at)
	{
#if ERR
		ArgumentOutOfRangeException.ThrowIfNegative(at);
		ArgumentOutOfRangeException.ThrowIfGreaterThan(at, LastIndex);
#endif
		values[at] = values[LastIndex];
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

	public T GetRandom(Random from) => this[from.Next(Count)];

	public T[] ToArray() => values[..Count];

	public T[] AsArray() => values;

	public IEnumerator<T> GetEnumerator()
	{
		foreach (var i in Count)
			yield return values[i];
	}
}
