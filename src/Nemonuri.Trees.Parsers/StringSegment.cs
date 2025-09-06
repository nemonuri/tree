using System.Collections;

namespace Nemonuri.Trees.Parsers;

public class StringSegment<TChar> : IString<TChar>
{
    public StringSegment(IString<TChar> internalString, int offset, int count)
    {
        Guard.IsNotNull(internalString);
        Guard.IsInRange(offset, 0, internalString.Count + 1);
        Guard.IsGreaterThanOrEqualTo(count, 0);
        Guard.IsLessThanOrEqualTo(offset + count, Count);

        InternalString = internalString;
        Offset = offset;
        Count = count;
    }

    public IString<TChar> InternalString { get; }
    public int Offset { get; }
    public int Count { get; }

    public TChar this[int index]
    {
        get
        {
            Guard.IsInRange(index, 0, Count);

            Debug.Assert(InternalString is not null);
            return InternalString[Offset + index];
        }
    }

    public IEnumerator<TChar> GetEnumerator()
    {
        for (int i = 0; i < Count; i++)
        {
            yield return this[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
