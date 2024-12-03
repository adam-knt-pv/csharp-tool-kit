using System.Diagnostics.CodeAnalysis;

namespace ToolKit.Applications;

public readonly struct Version : IParsable<Version>
{
	DevelopmentPhase phase { get; }

	int prefix { get; }
	int body { get; }
	int suffix { get; }
	int minor { get; } = -1;

	public Version(string from)
	{
		var phase_version = from.Split(' ');
		phase = Enum.Parse<DevelopmentPhase>(phase_version[0]);

		var version = phase_version[1].Split('.', '_');

		prefix = int.Parse(version[0][1..]);
		body = int.Parse(version[1]);
		suffix = int.Parse(version[2]);

		if (version.Length > 3)
			minor = int.Parse(version[3]);
	}

	public override string ToString() =>
		$"{Enum.GetName(phase)} v{prefix}.{body}.{suffix}{(minor == -1 ? "" : $"_{(minor > 9 ? minor : $"0{minor}")}")}";

	public static Version Parse(string s, IFormatProvider? provider) => new(s);

	public static bool TryParse(
		[NotNullWhen(true)] string? s,
		IFormatProvider? provider,
		out Version result
	)
	{
		if (s != null)
		{
			result = Parse(s, provider);
			return false;
		}

		result = default;
		return true;
	}

	public enum DevelopmentPhase
	{
		Indev,
		Alpha,
		Beta,
		Release,
	}
}
