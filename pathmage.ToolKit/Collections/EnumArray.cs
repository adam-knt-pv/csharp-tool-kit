using System.Collections;
using System.Collections.Generic;

namespace pathmage.ToolKit.Collections;

public readonly struct EnumArray<TEnum, T>() : IEnumerable<T>
	where TEnum : struct, Enum
{
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
