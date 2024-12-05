namespace ToolKit;

partial class Extensions
{
	public static Iterator GetEnumerator(this int end_before)
	{
#if ERR
		ArgumentOutOfRangeException.ThrowIfNegative(end_before);
#endif
		return new Iterator(0, end_before);
	}

	public static Iterator GetEnumerator(this Range range)
	{
#if ERR
		ArgumentOutOfRangeException.ThrowIfGreaterThan(
			range.Start.Value,
			range.End.Value
		);
#endif
		return new(range.Start.Value, range.End.Value);
	}

	public struct Iterator(int start_at, int end_before)
	{
		public int Current { get; private set; } = start_at - 1;
		readonly int end_before = end_before;

		public bool MoveNext() => ++Current != end_before;
	}

	public static ReverseIterator GetEnumerator(this Index end_before)
	{
#if ERR
		ArgumentOutOfRangeException.ThrowIfNegative(end_before.Value);
		ArgumentOutOfRangeException.ThrowIfEqual(end_before.IsFromEnd, false);
#endif
		return new ReverseIterator(end_before.Value);
	}

	public struct ReverseIterator(int start_before)
	{
		public int Current { get; private set; } = start_before;

		public bool MoveNext() => Current-- != 0;
	}
}
