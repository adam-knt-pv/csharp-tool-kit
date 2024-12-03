namespace ToolKit;

[AttributeUsage(AttributeTargets.Field)]
public sealed class ParsableAttribute<TParsable> : Attribute
	where TParsable : IParsable<TParsable>;
