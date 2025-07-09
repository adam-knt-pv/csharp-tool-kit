using System.Threading;
using AdamKnight.ToolKit.Debug;

namespace AdamKnight.ToolKit.Globals;

public interface Plugin
{
	static ILogger Logger
	{
		get => _logger;
		set
		{
			lock (LoggerLock)
				_logger = value;
		}
	}
	private static ILogger _logger = new Logger(
		Console.Write,
		Console.WriteLine
	);
	static readonly Lock LoggerLock = new();
}
