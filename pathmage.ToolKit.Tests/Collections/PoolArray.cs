namespace pathmage.ToolKit.Tests.Collections;

public class PoolArray
{
	public static void From()
	{
		var pool = PoolArray<int>.NewFrom(1, 2, 3);

		pool.Add(4, 5, 6);

		// pool.Remove(0);
		// pool.Remove(2);

		// print('i', pool.Add(34));
		// print('i', pool.Add(4));

		foreach (var i in pool)
			print(i);
	}
}
