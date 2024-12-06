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
}
