using System.Text.Json;

namespace pathmage.ToolKit.Helpers;

public interface DataHelper
{
	public static string Save<T>(T value) =>
		JsonSerializer.Serialize(value, Constants.Misc.JsonOptions);

	public static string Save<T>(T value, JsonSerializerOptions options) =>
		JsonSerializer.Serialize(value, options);

	public static T Load<T>(string value) =>
		JsonSerializer.Deserialize<T>(value, Constants.Misc.JsonOptions)!;

	public static T Load<T>(string value, JsonSerializerOptions options) =>
		JsonSerializer.Deserialize<T>(value, options)!;
}
