namespace pathmage.ToolKit;

partial class Extensions
{
	public static char ToLower(this char c) => char.ToLower(c);

	public static char ToUpper(this char c) => char.ToUpper(c);

	public static bool IsLower(this char c) => char.IsLower(c);

	public static bool IsUpper(this char c) => char.IsUpper(c);

	public static bool IsDigit(this char c) => char.IsDigit(c);

	public static bool IsLetter(this char c) => char.IsLetter(c);

	public static bool IsSpecial(this char c) => !char.IsLetterOrDigit(c);

	public static bool IsWhiteSpace(this char c) => char.IsWhiteSpace(c);
}
