using System.Reflection;

namespace pathmage.ToolKit;

public interface Constants
{
	interface Reflection
	{
		const BindingFlags BindingFlagsAllMembers =
			BindingFlags.Instance
			| BindingFlags.Static
			| BindingFlags.Public
			| BindingFlags.NonPublic;
	}

	interface Text
	{
		public static readonly char[] WhiteSpaceChars = [' ', '\t'];

		public static readonly char[] FileForbiddenChars =
		[
			'/',
			'\\',
			'?',
			'%',
			'*',
			':',
			'|',
			'"',
			'<',
			'>',
			'.',
			',',
			';',
			'=',
			' ',
			'\t',
		];

		/// <summary>
		/// Array of all strings that mean true.
		/// </summary>
		public static readonly string[] TrueStrings =
		[
			"true",
			"t",
			"yes",
			"y",
			"1",
		];

		/// <summary>
		/// Array of all strings that mean false.
		/// </summary>
		public static readonly string[] FalseStrings =
		[
			"false",
			"f",
			"no",
			"n",
			"0",
		];
	}
}
