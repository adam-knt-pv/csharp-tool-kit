namespace pathmage.ToolKit;

partial class Extensions
{
	/// <summary>
	/// Casts item to T.
	/// </summary>
	/// <param name="obj"></param>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	public static T As<T>(this object? obj) => (T)obj!;

	/// <summary>
	/// Casts multiple items to T.
	/// </summary>
	/// <param name="objects"></param>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	public static T[] As<T>(this object?[] objects)
	{
		var output = new T[objects.Length];

		objects.CopyTo(output, 0);

		return output;
	}

	/// <summary>
	/// Converts item to readable text.
	/// </summary>
	/// <param name="obj"></param>
	/// <returns></returns>
	public static string ToText(this object? obj) =>
		obj switch
		{
			null => "<null>",
			string { Length: 0 } => "<empty>",
			Type type => type.ToText(),
			_ => obj.ToString() ?? "<ToString->null>",
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
		{
			text_generics[i] = type_generics[i].ToText();
		}

		if (type_generics.Length > 0)
			return $"{type.Name[..^2]}<{string.Join(", ", text_generics)}>";

		if (type.IsGenericType && type_generics.Length == 0)
			return $"{type.Name[..^2]}<{new string(',', type.GetGenericArguments().Length - 1)}>";

		return type.Name;
	}
}
