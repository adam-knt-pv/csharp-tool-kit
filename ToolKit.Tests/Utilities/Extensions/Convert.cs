namespace pathmage.ToolKit.Tests.Utilities.Extensions;

public class Convert
{
	public static void As()
	{
		var obj = (object)"Hello World";

		Console.WriteLine(pathmage.ToolKit.Extensions.As<string?>(obj)?.Length);
	}

	public static void ItemsAs()
	{
		var items = new object[] { "11", "22", "33" };

		foreach (var item in pathmage.ToolKit.Extensions.As<string>(items))
			Console.WriteLine(item.Length);
	}

	public static void ToText()
	{
		var obj = (object?)null;
		Console.WriteLine(pathmage.ToolKit.Extensions.ToText(obj));

		obj = "";
		Console.WriteLine(pathmage.ToolKit.Extensions.ToText(obj));

		obj = new ToStringNull();
		Console.WriteLine(pathmage.ToolKit.Extensions.ToText(obj));

		obj = typeof(Action<,>);
		Console.WriteLine(pathmage.ToolKit.Extensions.ToText(obj));
	}

	public static void TypeToText()
	{
		var type = typeof(object);
		Console.WriteLine(pathmage.ToolKit.Extensions.ToText(type));

		type = typeof(Action<,>);
		Console.WriteLine(pathmage.ToolKit.Extensions.ToText(type));

		type = typeof(Action<int, Action<object, string>>);
		Console.WriteLine(pathmage.ToolKit.Extensions.ToText(type));

		type = typeof(int[,][]);
		Console.WriteLine(pathmage.ToolKit.Extensions.ToText(type));
	}

	class ToStringNull
	{
		public override string? ToString() => null;
	}
}
