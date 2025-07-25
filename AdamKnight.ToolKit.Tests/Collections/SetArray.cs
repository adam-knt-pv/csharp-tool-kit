﻿using AdamKnight.ToolKit.Collections;

namespace AdamKnight.ToolKit.Tests.Collections;

public class SetArray
{
	public static void From()
	{
		var set = SetArray<int>.NewFrom(1, 2, 3);

		set.Remove(1);
		set.TryRemove(1);
		set.TryRemove(2);

		foreach (var i in set)
		{
			print(i);
		}
	}
}
