using System.Threading;

namespace pathmage.ToolKit.Debug;

public sealed record Logger(Action<string> Write, Action WriteLine) : ILogger
{
	public static ILogger Singleton
	{
		get => _singleton;
		set
		{
			lock (Lock)
				_singleton = value;
		}
	}
	static ILogger _singleton = new Logger(Console.Write, Console.WriteLine);
	public static readonly Lock Lock = new();
}
