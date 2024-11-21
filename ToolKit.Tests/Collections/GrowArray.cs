namespace ToolKit.Tests.Collections;

public class GrowArray
{
	public static void Capacity()
	{
		var array = new GrowArray<int>(10);

		array.Add(10);
		array.Add(11);
		array.Add(12);
		array.AddRange(13, 14, 15, 16, 17, 18);

		array.RemoveLast();

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
		var array = new GrowArray<int>([10, 11, 12]);

		array.Add(13);
		array.Add(14);
		array.Add(15);
		array.AddRange(16, 17, 18);

		array.RemoveLast();

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
		var array = new GrowArray<int>([10, 11, 12], 0);

		array.Add(13);
		array.Add(14);
		array.Add(15);
		array.AddRange(16, 17, 18);

		array.RemoveLast();

		if (array.TryGet(8, out var item))
			print(nameof(array.TryGet), item);

		print(nameof(array.GetRandom), array.GetRandom());

		foreach (var i in array)
		{
			print(i);
		}
	}

	public static void Testspace()
	{
		var array = new GrowArray<int?>([1], 10);

		array.Add(null);
		array.Add(14);
		array.Add(15);
		array.AddRange(16, 17, 18);
		//
		// array.RemoveLast();
		//
		// if (array.TryGet(8, out var item))
		// 	print(nameof(array.TryGet), item);
		//
		// print(nameof(array.GetRandom), array.GetRandom());
		//
		// foreach (var i in array)
		// {
		// 	print(i);
		// }

		// if (array.TryGet(array.LastIndex, out var item))
		// 	print(nameof(array.TryGet), item);

		var to_array = array.ToArray();

		var as_array = array.AsArray();
	}
}
