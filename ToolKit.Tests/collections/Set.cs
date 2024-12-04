namespace ToolKit.Tests.Collections;

public class Set
{
	public static void From()
	{
		var set = Set<int>.From(1, 2, 3);

		set -= 1;
		set.TryRemove(1);
		set.TryRemove(2);

		foreach (var i in set)
		{
			print(i);
		}
	}
}
