using System.Collections;
using System.Runtime.CompilerServices;
using System.Text.Json;

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

		lock (Plugin.LoggerLock)
		{
			Plugin.Logger.Write(result);
			Plugin.Logger.WriteLine();
		}
	}

	/// <summary>
	/// Writes one or more items to the current output as readable text with a tab between each argument.
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

		lock (Plugin.LoggerLock)
		{
			Plugin.Logger.Write(result);
			Plugin.Logger.WriteLine();
		}
	}

	static void printl(params object?[] items)
	{
		lock (Plugin.LoggerLock)
		{
			for (int i = 0; i < items.Length; i++)
			{
				if (items[i] is IEnumerable enumerable and not string)
				{
					Plugin.Logger.Write($"[{i}]: {enumerable.GetType().ToText()}");
					Plugin.Logger.WriteLine();

					printItems(enumerable, 1);
					continue;
				}

				Plugin.Logger.Write($"[{i}]: {items[i].ToText()}");
				Plugin.Logger.WriteLine();
			}
		}

		static void printItems(IEnumerable items, int indent)
		{
			var i = 0;
			foreach (var item in items)
			{
				var text_indent = new string(
					' ',
					indent * Constants.Text.IndentSize
				);

				if (item is IEnumerable enumerable and not string)
				{
					Plugin.Logger.Write(
						$"{text_indent}[{i++}]: {enumerable.GetType().ToText()}"
					);
					Plugin.Logger.WriteLine();
					continue;
				}

				Plugin.Logger.Write($"{text_indent}[{i++}]: {item.ToText()}");
				Plugin.Logger.WriteLine();
			}
		}
	}

	static void printv(object item)
	{
		var variables = JsonSerializer.Serialize(
			item,
			Constants.File.JsonDefaultOptions
		);

		lock (Plugin.LoggerLock)
		{
			Plugin.Logger.Write(variables);
			Plugin.Logger.WriteLine();
		}
	}

	static void prints(
		object? item,
		[CallerArgumentExpression(nameof(item))] string? item_id = null
	)
	{
		lock (Plugin.LoggerLock)
		{
			Plugin.Logger.Write($"{item_id} = {item}");
			Plugin.Logger.WriteLine();
		}
	}
}
