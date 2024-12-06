using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace pathmage.ToolKit;

partial class Extensions
{
	public static bool TryGetAttribute<T>(
		this Type type,
		[NotNullWhen(true)] out Attribute? attribute
	) => type.TryGetAttribute(typeof(T), out attribute);

	public static bool TryGetAttribute(
		this Type type,
		Type attribute_type,
		[NotNullWhen(true)] out Attribute? attribute
	)
	{
		attribute = type.GetCustomAttribute(attribute_type);
		return attribute != null;
	}

	public static Set<Type> GetTypesWithInterface<T>(this Assembly assembly) =>
		assembly.GetTypesWithInterface(typeof(T));

	public static Set<Type> GetTypesWithInterface(
		this Assembly assembly,
		Type interface_type
	)
	{
		var result = Set<Type>.With(100);

		foreach (var type in assembly.GetTypes())
		{
			if (type.IsInterface)
				continue;

			foreach (var type_interface in type.GetInterfaces())
			{
				if (type_interface == interface_type)
				{
					result.Append(type);
					break;
				}
			}
		}

		return result;
	}
}
