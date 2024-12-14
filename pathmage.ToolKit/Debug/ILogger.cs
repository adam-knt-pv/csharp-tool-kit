using System.Collections;

namespace pathmage.ToolKit.Debug;

public sealed record Logger(Action<string> Write, Action WriteLine) : ILogger;

public interface ILogger
{
	Action<string> Write { get; }
	Action WriteLine { get; }

	/// <summary>
	/// Writes one or more items to the current output as readable text with a space between each argument.
	/// </summary>
	/// <param name="items"></param>
	static void print(params object?[] items)
	{
		var text_items = new string[items.Length];

		foreach (var i in items.Length)
			text_items[i] = items[i].ToText();

		var result = string.Join(' ', text_items);

		lock (Project.LoggerLock)
		{
			Project.Logger.Write(result);
			Project.Logger.WriteLine();
		}
	}

	/// <summary>
	/// <inheritdoc cref="print"/>
	/// </summary>
	/// <param name="items"></param>
	static void printt(params object?[] items)
	{
		var text_items = new string[items.Length];

		for (var i = 0; i < items.Length; i++)
			text_items[i] = items[i].ToText();

		string result = items is []
			? "---"
			: $"--- {string.Join(' ', text_items)} ---";

		lock (Project.LoggerLock)
		{
			Project.Logger.Write(result);
			Project.Logger.WriteLine();
		}
	}

	static void printl(params object?[] items)
	{
		lock (Project.LoggerLock)
		{
			for (int i = 0; i < items.Length; i++)
			{
				if (items[i] is IEnumerable enumerable and not string)
				{
					Project.Logger.Write($"[{i}]: {enumerable.GetType().ToText()}");
					Project.Logger.WriteLine();

					printItems(enumerable, 1);
					continue;
				}

				Project.Logger.Write($"[{i}]: {items[i].ToText()}");
				Project.Logger.WriteLine();
			}
		}

		static void printItems(IEnumerable items, int indent)
		{
			var i = 0;
			foreach (var item in items)
			{
				var text_indent = new string(' ', indent * 2);

				if (item is IEnumerable enumerable and not string)
				{
					Project.Logger.Write(
						$"{text_indent}[{i++}]: {enumerable.GetType().ToText()}"
					);
					Project.Logger.WriteLine();
					continue;
				}

				Project.Logger.Write($"{text_indent}[{i++}]: {item.ToText()}");
				Project.Logger.WriteLine();
			}
		}
	}
}
