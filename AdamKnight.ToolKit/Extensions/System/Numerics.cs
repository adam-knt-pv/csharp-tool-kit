using System.Security.Cryptography;
using System.Text;

namespace AdamKnight.ToolKit.Extensions;

partial class Extensions
{
	public static byte ToByte(this string str)
	{
		byte output = 0;

		foreach (var b in SHA256.HashData(Encoding.UTF8.GetBytes(str)))
		{
			output ^= b;
		}

		return output;
	}

	public static short ToInt16(this string str) =>
		BitConverter.ToInt16(SHA256.HashData(Encoding.UTF8.GetBytes(str)));

	public static int ToInt32(this string str) =>
		BitConverter.ToInt32(SHA256.HashData(Encoding.UTF8.GetBytes(str)));

	/// <summary>
	/// Converts a 64-bit integer into two 32-bit integers, representing the higher and lower parts of the original value.
	/// </summary>
	/// <param name="n">
	/// The 64-bit integer to split. The higher 32 bits are extracted as the left value, and the lower 32 bits as the right value.
	/// </param>
	/// <returns>
	/// A tuple containing two 32-bit integers:
	/// <list type="bullet">
	/// <item>Left: 0 or positive value if <paramref name="n"/> is positive, negative value otherwise.</item>
	/// </list>
	/// </returns>
	public static (int Left, int Right) ToInt32(this long n) =>
		((int)(n >> 32), (int)(n & 0xFFFFFFFF));

	public static long ToInt64(this string str) =>
		BitConverter.ToInt64(SHA256.HashData(Encoding.UTF8.GetBytes(str)));
}
