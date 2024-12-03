using ToolKit.Applications.Debug;

namespace ToolKit.Applications;

static class Application
{
	public static IApplication Instance { get; set; } = null!;

	static Application()
	{
		if (Instance == null!)
			Instance = new Application<_>("Unnamed", new());
	}

	enum _;
}

public interface IApplication
{
	ILogger Logger { get; }
	object LoggerLock { get; }
}

public class Application<TDirectories> : IApplication
	where TDirectories : struct, Enum
{
	public readonly string Name;
	public readonly Version Version;

	public ILogger Logger
	{
		get => _logger;
		set
		{
			lock (LoggerLock)
				_logger = value;
		}
	}
	ILogger _logger = new Logger(Console.Write, Console.WriteLine);
	public object LoggerLock { get; } = new();

	public Application(string name, Version version)
	{
		Name = name;
		Version = version;

		Application.Instance = this;
	}
}
