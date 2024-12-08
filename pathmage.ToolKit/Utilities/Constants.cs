using System.Reflection;

namespace pathmage.ToolKit;

public static class Constants
{
	public static class Reflection
	{
		public const BindingFlags BindingFlagsAllMembers =
			BindingFlags.Instance
			| BindingFlags.Static
			| BindingFlags.Public
			| BindingFlags.NonPublic;
	}
}
