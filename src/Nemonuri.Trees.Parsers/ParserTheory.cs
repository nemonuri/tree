namespace Nemonuri.Trees.Parsers;

public static class ParserTheory
{
    public static IParser<TChar> Create<TChar>
    (
        Func<IString<TChar>, int, IParser<TChar>, IEnumerable<ISyntaxTree<TChar>>> parseImplementation
    )
    {
        return new AdHocParser<TChar>(parseImplementation);
    }
}