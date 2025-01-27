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
	/// <param name="objects"></param>
	static void print(params object?[] objects)
	{
		var text_items = new string[objects.Length];

		foreach (var i in objects.Length)
			text_items[i] = objects[i].ToText();

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
	/// <param name="objects"></param>
	static void printt(params object?[] objects)
	{
		var text_items = new string[objects.Length];

		for (var i = 0; i < objects.Length; i++)
			text_items[i] = objects[i].ToText();

		string result = objects is []
			? "---"
			: $"--- {string.Join(' ', text_items)} ---";

		lock (Plugin.LoggerLock)
		{
			Plugin.Logger.Write(result);
			Plugin.Logger.WriteLine();
		}
	}

	static void printl(params object?[] objects)
	{
		lock (Plugin.LoggerLock)
		{
			for (int i = 0; i < objects.Length; i++)
			{
				if (objects[i] is IEnumerable enumerable and not string)
				{
					Plugin.Logger.Write($"[{i}]: {enumerable.GetType().ToText()}");
					Plugin.Logger.WriteLine();

					printItems(enumerable, 1);
					continue;
				}

				Plugin.Logger.Write($"[{i}]: {objects[i].ToText()}");
				Plugin.Logger.WriteLine();
			}
		}

		static void printItems(IEnumerable objects, int indent)
		{
			var i = 0;
			foreach (var item in objects)
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

	static void printv(object obj)
	{
		var variables = DataHelper.Save(obj);

		lock (Plugin.LoggerLock)
		{
			Plugin.Logger.Write(variables);
			Plugin.Logger.WriteLine();
		}
	}

	static void prints(
		object? obj,
		[CallerArgumentExpression(nameof(obj))] string? item_id = null
	)
	{
		lock (Plugin.LoggerLock)
		{
			Plugin.Logger.Write($"{item_id} = {obj}");
			Plugin.Logger.WriteLine();
		}
	}
}
