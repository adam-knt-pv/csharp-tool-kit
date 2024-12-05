namespace pathmage.ToolKit;

class Constants
{
	public static class StringSet
	{
		/// <summary>
		/// Array of all strings that mean true.
		/// </summary>
		public static readonly string[] True = ["true", "t", "yes", "y", "1"];

		/// <summary>
		/// Array of all strings that mean false.
		/// </summary>
		public static readonly string[] False = ["false", "f", "no", "n", "0"];

		/// <summary>
		/// Array of all strings that mean true and false.
		/// </summary>
		public static readonly string[] TrueFalse;

		static StringSet()
		{
			TrueFalse = new string[True.Length + False.Length];
			True.CopyTo(TrueFalse, 0);
			False.CopyTo(TrueFalse, True.Length);
		}
	}
}
