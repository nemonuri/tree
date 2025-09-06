namespace Nemonuri.Trees.Parsers;

public readonly struct AdHocParser<TChar, TInfo> : IParser<TChar, TInfo>
{
    private readonly Func<IString<TChar>, int, IEnumerable<IReadOnlyList<ISyntaxTree<TChar, TInfo>>>> _parseImplementation;

    public AdHocParser(Func<IString<TChar>, int, IEnumerable<IReadOnlyList<ISyntaxTree<TChar, TInfo>>>> parseImplementation)
    {
        Guard.IsNotNull(parseImplementation);
        _parseImplementation = parseImplementation;
    }

    public IEnumerable<IReadOnlyList<ISyntaxTree<TChar, TInfo>>> Parse(IString<TChar> @string, int offset)
    {
        return _parseImplementation(@string, offset);
    }
}
