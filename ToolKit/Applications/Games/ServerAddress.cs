using System.Diagnostics.CodeAnalysis;

namespace ToolKit.GameDevelopment;

public readonly struct ServerAddress : IParsable<ServerAddress>
{
	const char separator = '#';

	const int min_port = 1024;
	const int default_port = 5121;
	const int max_port = 65535;

	/// <summary>
	/// Either a fully qualified domain name or an IP address in IPv4 or IPv6 format.
	/// </summary>
	public string Address { get; }
	public int Port { get; }

	public ServerAddress(string from)
	{
		var address = from.Split(separator);
		Address = address[0];

		if (address.Length > 1 && int.TryParse(address[1], out var port))
			Port = port;

		if (Port is < min_port or > max_port)
			Port = default_port;
	}

	public override string ToString() => $"{Address}{separator}{Port}";

	public static implicit operator ServerAddress(string from) => new(from);

	public static implicit operator string(ServerAddress address) =>
		address.ToString();

	public static ServerAddress Parse(string s, IFormatProvider? provider) =>
		new(s);

	public static bool TryParse(
		[NotNullWhen(true)] string? s,
		IFormatProvider? provider,
		out ServerAddress result
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
}
