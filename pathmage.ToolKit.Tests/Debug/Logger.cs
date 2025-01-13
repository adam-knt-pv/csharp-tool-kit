using pathmage.ToolKit.Debug;

namespace pathmage.ToolKit.Tests.Debug;

public class Logger
{
	public static void ILogger()
	{
		print("Hello!");

		print(null, "", typeof(Action<>));

		printt();

		printt("New segment or smth");

		printl(1, 2, 3);

		printv(1);

		printv(Vec<int>.With(10));

		prints(1 + 2);
		prints(2);

		var a = "abc";
		prints(a);
	}

	public static void PrintList()
	{
		var items = new object?[]
		{
			new object[] { 1, 2, 3 },
			new object[][]
			{
				[11, 22, 33, 44],
				['a', 'b', 'c'],
				[new object[] { 1, 2, 3 }, new object[] { 4, 5, 6 }],
			},
			4,
			5,
			6,
			typeof(Action<>),
			null,
			new object[,]
			{
				{ 1, 2 },
				{ 3, 4 },
			},
		};

		printl(items);
	}

	public static void LoggerWrapperTest()
	{
		Plugin.Logger = new LoggerWrapper(Console.WriteLine);

		print("Hello!");
	}
}
