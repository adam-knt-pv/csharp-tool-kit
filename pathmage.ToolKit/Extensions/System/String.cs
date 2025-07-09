using System.Linq;
using System.Text;

namespace AdamKnight.ToolKit.Extensions;

partial class Extensions
{
	public static string TrimAll(this string str, params char[] chars)
	{
		var output = new StringBuilder();

		foreach (var c in str)
		{
			if (!chars.Contains(c))
				output.Append(c);
		}

		return output.ToString();
	}

	public static string TrimAllExcept(this string str, params char[] chars)
	{
		var output = new StringBuilder();

		foreach (var c in str)
		{
			if (chars.Contains(c))
				output.Append(c);
		}

		return output.ToString();
	}
}
