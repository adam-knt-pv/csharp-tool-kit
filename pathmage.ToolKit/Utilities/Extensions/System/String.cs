using System.Linq;
using System.Text;

namespace pathmage.ToolKit;

partial class Extensions
{
	public static string TrimAll(this string text, params char[] chars)
	{
		var result = new StringBuilder();

		foreach (var c in text)
		{
			if (!chars.Contains(c))
				result.Append(c);
		}

		return result.ToString();
	}

	public static string TrimAllExcept(this string text, params char[] chars)
	{
		var result = new StringBuilder();

		foreach (var c in text)
		{
			if (chars.Contains(c))
				result.Append(c);
		}

		return result.ToString();
	}
}
