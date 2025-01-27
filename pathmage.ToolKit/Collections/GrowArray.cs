using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace pathmage.ToolKit.Collections;

public struct GrowArray<T> : IEquatable<GrowArray<T>>
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

	public static GrowArray<T> New(int length)
	{
#if ERR
		ArgumentOutOfRangeException.ThrowIfNegative(length);
#endif
		return new() { values = new T[length], Count = 0 };
	}

	public static GrowArray<T> NewFrom(params T[] values) =>
		new() { values = values, Count = values.Length };

	public static GrowArray<T> NewFrom(T[] values, int count)
	{
#if ERR
		ArgumentOutOfRangeException.ThrowIfNegative(count);
#endif
		return new() { values = values, Count = count };
	}

	public static GrowArray<T> NewFromOf(T[] values, int add_length = 0)
	{
#if ERR
		ArgumentOutOfRangeException.ThrowIfNegative(add_length);
#endif
		var output = new GrowArray<T>
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

	public bool Equals(GrowArray<T> other) =>
		(values, Count) == (other.values, other.Count);

	public override bool Equals(object? obj) =>
		obj is GrowArray<T> other && Equals(other);

	public static bool operator ==(GrowArray<T> left, GrowArray<T> right) =>
		left.Equals(right);

	public static bool operator !=(GrowArray<T> left, GrowArray<T> right) =>
		!left.Equals(right);

	public override int GetHashCode() => HashCode.Combine(values, Count);
}
