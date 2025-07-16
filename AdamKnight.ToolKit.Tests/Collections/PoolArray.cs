using AdamKnight.ToolKit.Collections;

namespace AdamKnight.ToolKit.Tests.Collections;

public class PoolArray
{
	public static void From()
	{
		var pool = PoolArray<(int, int)>.NewFrom((0, 0), (0, 0), (0, 0));

		// pool.Add(4, 5, 6);

		pool.GetRef(0).Item1++;

		// pool.Remove(0);
		// pool.Remove(2);

		// print('i', pool.Add(34));
		// print('i', pool.Add(4));

		foreach (var i in pool)
			print(i);
	}
}
