using System.Text.Json;

namespace pathmage.ToolKit.Globals;

public interface DataHelper
{
	public static JsonSerializerOptions JsonOptions { get; } =
		new()
		{
			WriteIndented = true,
			IncludeFields = true,
			AllowTrailingCommas = true,
			IndentSize = Constants.Text.IndentSize,
		};

	public static JsonSerializerOptions JsonReadableOptions { get; } =
		new()
		{
			WriteIndented = true,
			IncludeFields = true,
			AllowTrailingCommas = true,
			IndentSize = Constants.Text.IndentSize,
		};

	public static string ToString<T>(T value) =>
		JsonSerializer.Serialize(value, JsonOptions);

	public static T FromString<T>(string str) =>
		JsonSerializer.Deserialize<T>(str, JsonOptions)!;

	public static string ToReadableString<T>(T value) =>
		JsonSerializer.Serialize(value, JsonOptions);

	public static T FromReadableString<T>(string str) =>
		JsonSerializer.Deserialize<T>(str, JsonOptions)!;
}
