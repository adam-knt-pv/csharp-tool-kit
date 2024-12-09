using System.Text;

namespace pathmage.ToolKit;

partial class Extensions
{
	public static char ToLower(this char item) => char.ToLower(item);

	public static char ToUpper(this char item) => char.ToUpper(item);

	public static bool IsLower(this char item) => char.IsLower(item);

	public static bool IsUpper(this char item) => char.IsUpper(item);

	public static bool IsDigit(this char item) => char.IsDigit(item);

	public static bool IsLetter(this char item) => char.IsLetter(item);

	public static bool IsSpecial(this char item) => !char.IsLetterOrDigit(item);

	public static bool IsWhiteSpace(this char item) => char.IsWhiteSpace(item);

	public static string TrimAll(this string item, params char[] chars)
	{
		foreach (var i in chars.Length)
			item = item.Replace(chars[i], '\0');

		return item;
	}

	public static string TrimAllExcept(this string item, params char[] chars)
	{
		var result = new StringBuilder(item.Length);

		foreach (var i in item.Length)
		{
			foreach (var ii in chars.Length)
			{
				if (item[i] == chars[ii])
				{
					result.Append(item[i]);
					goto NextChar;
				}
			}

			continue;
			NextChar:
			;
		}

		return result.ToString();
	}

	public static string TrimAllExcept(this string item, params char[][] chars)
	{
		var result = new StringBuilder(item.Length);

		foreach (var i in item.Length)
		{
			foreach (var ii in chars.Length)
			{
				foreach (var iii in chars[ii].Length)
				{
					if (item[i] == chars[ii][iii])
					{
						result.Append(item[i]);
						goto NextChar;
					}
				}
			}

			continue;
			NextChar:
			;
		}

		return result.ToString();
	}
}
