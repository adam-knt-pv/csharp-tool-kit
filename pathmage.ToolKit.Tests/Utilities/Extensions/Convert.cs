namespace pathmage.ToolKit.Tests.Utilities.Extensions;

public class Convert
{
	public static void As()
	{
		var obj = (object)"Hello World";

		Console.WriteLine(obj.As<string?>()?.Length);
	}

	public static void ItemsAs()
	{
		var items = new object[] { "11", "22", "33" };

		foreach (var item in items.As<string>())
			Console.WriteLine(item.Length);
	}

	public static void ToText()
	{
		var obj = (object?)null;
		Console.WriteLine(obj.ToText());

		obj = "";
		Console.WriteLine(obj.ToText());

		obj = new ToStringNull();
		Console.WriteLine(obj.ToText());

		obj = typeof(Action<,>);
		Console.WriteLine(obj.ToText());
	}

	public static void TypeToText()
	{
		var type = typeof(object);
		Console.WriteLine(type.ToText());

		type = typeof(Action<,>);
		Console.WriteLine(type.ToText());

		type = typeof(Action<int, Action<object, string>>);
		Console.WriteLine(type.ToText());

		type = typeof(int[,][]);
		Console.WriteLine(type.ToText());
	}

	class ToStringNull
	{
		public override string? ToString() => null;
	}
}
