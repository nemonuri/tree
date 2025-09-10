namespace Nemonuri.Trees.Parsers;

public static class ParserTheory
{
    public static IParserNode<TChar> Create<TChar>
    (
        Func<IString<TChar>, int, IParserNode<TChar>, IEnumerable<IBinderSyntaxTree<TChar>>> parseImplementation
    )
    {
        return new AdHocParser<TChar>(parseImplementation);
    }
}