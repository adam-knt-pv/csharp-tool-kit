using System.Text;

namespace AdamKnight.ToolKit.Debug;

public sealed class LoggerWrapper : ILogger
{
	readonly StringBuilder output = new(100);

	public Action<string> Write { get; }
	public Action WriteLine { get; }

	public LoggerWrapper(Action<string> write_line)
	{
		Write = item => output.Append(item);

		WriteLine = () =>
		{
			write_line(output.ToString());
			output.Clear();
		};
	}
}
