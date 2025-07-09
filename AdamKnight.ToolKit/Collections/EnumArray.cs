using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using AdamKnight.ToolKit.Utilities;

namespace AdamKnight.ToolKit.Collections;

public readonly struct EnumArray<TEnum, T>() : IEnumerable<T>
	where TEnum : struct, Enum
{
	[JsonInclude]
	T[] values { get; } = new T[Enum<TEnum>.Length];

	public T this[int idx]
	{
		get => values[idx];
		set => values[idx] = value;
	}

	public T this[TEnum e]
	{
		get => this[e.ToInt32()];
		set => this[e.ToInt32()] = value;
	}

	public EnumArray(params T[] values)
		: this()
	{
		this.values = values;
	}

	public EnumArray(params KeyValuePair<TEnum, T>[] pairs)
		: this(new T[Enum<TEnum>.Length])
	{
		foreach (var i in Enum<TEnum>.Length)
		{
			this[pairs[i].Key] = pairs[i].Value;
		}
	}

	public void Add(TEnum e, T value)
	{
		this[e] = value;
	}

	public T GetRandom() => this[Random.Shared.Next(Enum<TEnum>.Length)];

	public T GetRandom(Random random) => this[random.Next(Enum<TEnum>.Length)];

	public T[] AsArray() => values;

	public IEnumerator<T> GetEnumerator()
	{
		foreach (var i in Enum<TEnum>.Length)
		{
			yield return values[i];
		}
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}
