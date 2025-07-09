using System.Text.Json;

namespace AdamKnight.ToolKit.Globals;

public interface Data
{
	const char ItemSeparator = '¬';

	static JsonSerializerOptions JsonOptions { get; } =
		new()
		{
			WriteIndented = true,
			IncludeFields = true,
			AllowTrailingCommas = true,
			IndentSize = Constants.Text.IndentSize,
		};

	static JsonSerializerOptions JsonReadableOptions { get; } =
		new()
		{
			WriteIndented = true,
			IncludeFields = true,
			AllowTrailingCommas = true,
			IndentSize = Constants.Text.IndentSize,
		};

	static string ToString<T>(T value) =>
		JsonSerializer.Serialize(value, JsonOptions);

	static T FromString<T>(string str) =>
		JsonSerializer.Deserialize<T>(str, JsonOptions)!;

	static string ToReadableString<T>(T value) =>
		JsonSerializer.Serialize(value, JsonOptions);

	static T FromReadableString<T>(string str) =>
		JsonSerializer.Deserialize<T>(str, JsonOptions)!;
}
