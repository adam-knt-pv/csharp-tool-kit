namespace pathmage.ToolKit.Tests.Collections;

public class Vec
{
	public static void Capacity()
	{
		print(2 / 2);
		var array = GrowArray<int>.With(10);

		array.Append(10);
		array.Append(11);
		array.Append(12);
		array.Append(13, 14, 15, 16, 17, 18);

		array.Pop();

		if (array.TryGet(8, out var item))
			print(nameof(array.TryGet), item);

		print(nameof(array.GetRandom), array.GetRandom());

		foreach (var i in array)
		{
			print(i);
		}
	}

	public static void From()
	{
		var array = GrowArray<int>.From(10, 11, 12);

		array += 13;
		array.Append(14);
		array.Append(15);
		array += [16, 17, 18];

		array--;

		if (array.TryGet(8, out var item))
			print(nameof(array.TryGet), item);

		print(nameof(array.GetRandom), array.GetRandom());

		foreach (var i in array)
		{
			print(i);
		}
	}

	public static void Copy()
	{
		var array = GrowArray<int>.Copy([10, 11, 12]);

		array.Append(13);
		array.Append(14);
		array.Append(15);
		array.Append(16, 17, 18);

		array.Pop();

		if (array.TryGet(8, out var item))
			print(nameof(array.TryGet), item);

		print(nameof(array.GetRandom), array.GetRandom());

		foreach (var i in array)
		{
			print(i);
		}
	}

	public static void Tests()
	{
		var array = GrowArray<int?>.Copy([1], 10);

		array.Append((int?)null);
		array.Append(14);
		array.Append(15);
		array.Append(16, 17, 18);
		//
		// array.RemoveLast();
		//
		// if (array.TryGet(8, out var item))
		// 	print(nameof(array.TryGet), item);
		//
		// print(nameof(array.GetRandom), array.GetRandom());
		//
		foreach (var i in array)
		{
			print(i);
		}

		// if (array.TryGet(array.LastIndex, out var item))
		// 	print(nameof(array.TryGet), item);

		var to_array = array.ToArray();

		var as_array = array.AsArray();
	}
}
