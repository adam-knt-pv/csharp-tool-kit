using ToolKit.Applications;
using ToolKit.Applications.Debug;

namespace ToolKit.Tests.Debug;

public class Logger
{
	public static void ILogger()
	{
		print("Hello!");

		print(null, "", typeof(Action<>));

		printt();

		printt("New segment or smth");
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
		var app = new Application<_>("x", new())
		{
			Logger = new LoggerWrapper(Console.WriteLine),
		};

		print("Hello!");
	}

	enum _;
}
