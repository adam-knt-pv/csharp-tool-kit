using System.Threading;
using pathmage.ToolKit.Debug;

namespace pathmage.ToolKit.Globals;

public static class Project
{
	public static ILogger Logger
	{
		get => _logger;
		set
		{
			lock (LoggerLock)
				_logger = value;
		}
	}
	static ILogger _logger = new Logger(Console.Write, Console.WriteLine);
	public static readonly Lock LoggerLock = new();
}
