namespace pathmage.ToolKit;

[AttributeUsage(AttributeTargets.Field)]
public class FileFieldAttribute<T> : Attribute
	where T : IParsable<T>;

[AttributeUsage(AttributeTargets.Field)]
public sealed class FileArrayAttribute<T> : Attribute
	where T : IParsable<T>;
