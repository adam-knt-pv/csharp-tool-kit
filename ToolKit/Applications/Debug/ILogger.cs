using System.Collections;
using ToolKit.Applications;

namespace ToolKit.Applications.Debug;

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

		for (var i = 0; i < items.Length; i++)
			text_items[i] = items[i].ToText();

		var result = string.Join(' ', text_items);

		lock (Application.Instance.LoggerLock)
		{
			Application.Instance.Logger.Write(result);
			Application.Instance.Logger.WriteLine();
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

		lock (Application.Instance.LoggerLock)
		{
			Application.Instance.Logger.Write(result);
			Application.Instance.Logger.WriteLine();
		}
	}

	static void printl(params object?[] items)
	{
		lock (Application.Instance.LoggerLock)
		{
			for (int i = 0; i < items.Length; i++)
			{
				if (items[i] is IEnumerable enumerable and not string)
				{
					Application.Instance.Logger.Write(
						$"[{i}]: {enumerable.GetType().ToText()}"
					);
					Application.Instance.Logger.WriteLine();

					printItems(enumerable, 1);
					continue;
				}

				Application.Instance.Logger.Write($"[{i}]: {items[i].ToText()}");
				Application.Instance.Logger.WriteLine();
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
					Application.Instance.Logger.Write(
						$"{text_indent}[{i++}]: {enumerable.GetType().ToText()}"
					);
					Application.Instance.Logger.WriteLine();
					continue;
				}

				Application.Instance.Logger.Write(
					$"{text_indent}[{i++}]: {item.ToText()}"
				);
				Application.Instance.Logger.WriteLine();
			}
		}
	}
}

public sealed record Logger(Action<string> Write, Action WriteLine) : ILogger;
