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
}
