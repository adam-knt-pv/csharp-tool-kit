using System.Reflection;
using System.Text.Json;

namespace pathmage.ToolKit.Globals;

public interface Constants
{
	interface File
	{
		const char ItemSeparator = '¬';

		static readonly JsonSerializerOptions JsonDefaultOptions = new()
		{
			WriteIndented = true,
			IncludeFields = true,
			AllowTrailingCommas = true,
			IndentSize = Text.IndentSize,
		};
	}

	interface Text
	{
		const int IndentSize = 3;

		static readonly char[] WhiteSpaceChars = [' ', '\t'];

		static readonly char[] FileForbiddenChars =
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
			'&',
		];

		static readonly char[] PunctuationChars =
		[
			'.',
			',',
			'?',
			'!',
			':',
			';',
			'\'',
			'"',
			'(',
			')',
			'-',
			'[',
			']',
			'/',
			'\\',
		];

		static readonly char[] NumberChars =
		[
			'0',
			'1',
			'2',
			'3',
			'4',
			'5',
			'6',
			'7',
			'8',
			'9',
		];

		static readonly char[] LetterChars =
		[
			'a',
			'b',
			'c',
			'd',
			'e',
			'f',
			'g',
			'h',
			'i',
			'j',
			'k',
			'l',
			'm',
			'n',
			'o',
			'p',
			'q',
			'r',
			's',
			't',
			'u',
			'v',
			'w',
			'x',
			'y',
			'z',
			'A',
			'B',
			'C',
			'D',
			'E',
			'F',
			'G',
			'H',
			'I',
			'J',
			'K',
			'L',
			'M',
			'N',
			'O',
			'P',
			'Q',
			'R',
			'S',
			'T',
			'U',
			'V',
			'W',
			'X',
			'Y',
			'Z',
		];

		static readonly char[] LetterLowerChars =
		[
			'a',
			'b',
			'c',
			'd',
			'e',
			'f',
			'g',
			'h',
			'i',
			'j',
			'k',
			'l',
			'm',
			'n',
			'o',
			'p',
			'q',
			'r',
			's',
			't',
			'u',
			'v',
			'w',
			'x',
			'y',
			'z',
		];

		static readonly char[] LetterUpperChars =
		[
			'A',
			'B',
			'C',
			'D',
			'E',
			'F',
			'G',
			'H',
			'I',
			'J',
			'K',
			'L',
			'M',
			'N',
			'O',
			'P',
			'Q',
			'R',
			'S',
			'T',
			'U',
			'V',
			'W',
			'X',
			'Y',
			'Z',
		];

		/// <summary>
		/// Array of all strings that mean true.
		/// </summary>
		static readonly string[] TrueStrings = ["true", "t", "yes", "y", "1"];

		/// <summary>
		/// Array of all strings that mean false.
		/// </summary>
		static readonly string[] FalseStrings = ["false", "f", "no", "n", "0"];
	}

	interface Reflection
	{
		const BindingFlags BindingFlagsAllMembers =
			BindingFlags.Instance
			| BindingFlags.Static
			| BindingFlags.Public
			| BindingFlags.NonPublic;
	}
}
