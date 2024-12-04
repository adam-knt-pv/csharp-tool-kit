namespace ToolKit.Tests.Utilities.Extensions;

public class Iteration
{
	public static void Int()
	{
		foreach (var i in 0)
			print(i);

		foreach (var i in 1)
			print(i);

		foreach (var i in 10)
			print(i);
	}

	public static void Range()
	{
		foreach (var i in ..0)
			print(i);

		foreach (var i in ..1)
			print(i);

		foreach (var i in ..10)
			print(i);
	}

	public static void Index()
	{
		foreach (var i in ^0)
			print(i);

		foreach (var i in ^1)
			print(i);

		foreach (var i in ^10)
			print(i);
	}
}
