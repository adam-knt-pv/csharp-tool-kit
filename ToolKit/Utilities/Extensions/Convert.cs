namespace ToolKit;

partial class Extensions
{
	/// <summary>
	/// Casts item to T.
	/// </summary>
	/// <param name="item"></param>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	public static T As<T>(this object? item) => (T)item!;

	/// <summary>
	/// Casts multiple items to T.
	/// </summary>
	/// <param name="items"></param>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	public static T[] As<T>(this object?[] items)
	{
		var result = new T[items.Length];

		items.CopyTo(result, 0);

		return result;
	}

	/// <summary>
	/// Converts item to readable text.
	/// </summary>
	/// <param name="item"></param>
	/// <returns></returns>
	public static string ToText(this object? item) =>
		item switch
		{
			null => "<null>",
			string { Length: 0 } => "<empty>",
			Type type => type.ToText(),
			_ => item.ToString() ?? "<ToString->null>",
		};

	/// <summary>
	/// Converts type to readable text.
	/// </summary>
	/// <param name="type"></param>
	/// <returns></returns>
	public static string ToText(this Type type)
	{
		var type_generics = type.GenericTypeArguments;
		var text_generics = new string[type_generics.Length];

		for (var i = 0; i < type_generics.Length; i++)
			text_generics[i] = type_generics[i].ToText();

		if (type_generics.Length > 0)
			return $"{type.Name[..^2]}<{string.Join(", ", text_generics)}>";

		if (type.IsGenericType && type_generics.Length == 0)
			return $"{type.Name[..^2]}<{new string(',', type.GetGenericArguments().Length - 1)}>";

		return type.Name;
	}
}
