namespace ToolKit;

[AttributeUsage(AttributeTargets.Field)]
public class ParsableAttribute<TParsable> : Attribute
	where TParsable : IParsable<TParsable>;

public sealed class ParsableArrayAttribute<TParsable>
	: ParsableAttribute<TParsable>
	where TParsable : IParsable<TParsable>;
