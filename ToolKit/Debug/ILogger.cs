using System.Collections;

namespace pathmage.ToolKit.Debug;

public interface ILogger
{
	Action<string> Write { get; }
	Action WriteLine { get; }

	/// <summary>
	/// Writes one or more items to the current output as readable text with a space between each argument.
	/// </summary>
	/// <param name="items"></param>
	static void print(params ReadOnlySpan<object?> items)
	{
		var text_items = new string[items.Length];

		foreach (var i in items.Length)
			text_items[i] = items[i].ToText();

		var result = string.Join(' ', text_items);

		lock (Logger.Lock)
		{
			Logger.Singleton.Write(result);
			Logger.Singleton.WriteLine();
		}
	}

	/// <summary>
	/// <inheritdoc cref="print"/>
	/// </summary>
	/// <param name="items"></param>
	static void printt(params ReadOnlySpan<object?> items)
	{
		var text_items = new string[items.Length];

		for (var i = 0; i < items.Length; i++)
			text_items[i] = items[i].ToText();

		string result = items is []
			? "---"
			: $"--- {string.Join(' ', text_items)} ---";

		lock (Logger.Lock)
		{
			Logger.Singleton.Write(result);
			Logger.Singleton.WriteLine();
		}
	}

	static void printl(params Span<object?> items)
	{
		lock (Logger.Lock)
		{
			for (int i = 0; i < items.Length; i++)
			{
				if (items[i] is IEnumerable enumerable and not string)
				{
					Logger.Singleton.Write(
						$"[{i}]: {enumerable.GetType().ToText()}"
					);
					Logger.Singleton.WriteLine();

					printItems(enumerable, 1);
					continue;
				}

				Logger.Singleton.Write($"[{i}]: {items[i].ToText()}");
				Logger.Singleton.WriteLine();
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
					Logger.Singleton.Write(
						$"{text_indent}[{i++}]: {enumerable.GetType().ToText()}"
					);
					Logger.Singleton.WriteLine();
					continue;
				}

				Logger.Singleton.Write($"{text_indent}[{i++}]: {item.ToText()}");
				Logger.Singleton.WriteLine();
			}
		}
	}
}
