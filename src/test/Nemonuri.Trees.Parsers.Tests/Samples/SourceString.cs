using System.Collections;

namespace Nemonuri.Trees.Parsers.Tests.Samples;

public class SourceString : IString<char, SourceString>
{
    private readonly string _internalString;

    public SourceString(string internalString)
    {
        _internalString = internalString;
    }

    public char this[int index] => _internalString[index];

    public int Count => _internalString.Length;

    public string InternalString => _internalString;

    public IEnumerator<char> GetEnumerator() => _internalString.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public SourceString Slice(int start, int length)
    {
        ReadOnlySpan<char> chars = _internalString;
        chars = chars.Slice(start, length);
        return new SourceString(new string(chars));
    }
}
