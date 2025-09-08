
namespace Nemonuri.Trees.Parsers;

public class ProductParser<TChar, TInfo> : IParser<TChar, TInfo>
{
    private readonly IEnumerable<IParser<TChar, TInfo>> _parserSequence;

    public ProductParser(IEnumerable<IParser<TChar, TInfo>> parserSequence)
    {
        Guard.IsNotNull(parserSequence);
        _parserSequence = parserSequence;
    }

    public IEnumerable<ITree<IInformedString<TChar, TInfo>>> Parse(IString<TChar> @string, int offset)
    {
        if (_parserSequence.FirstOrDefault() is not { } parser) { yield break; }
        if (!(0 <= offset && offset < @string.Count)) { yield break; }

        var forest = parser.Parse(@string, offset);
        foreach (var tree in forest)
        {
            int advancedCount = tree.Value.String.Count;
        }

    }
}

#if false
public static class ParserTheory
{
    public static IEnumerable<IEnumerable<ITree<IInformedString<TChar, TInfo>>>>
    ProductParse<TChar, TInfo>
    (
        IEnumerable<IParser<TChar, TInfo>> parserSequence, IString<TChar> @string, int offset
    )
    {
        if (parserSequence.FirstOrDefault() is not { } parser) { yield break; }
        if (!(0 <= offset && offset < @string.Count)) { yield break; }

        var forest = parser.Parse(@string, offset);
        foreach (var tree in forest)
        {
            var nextParserSequence = parserSequence.Skip(1);
            int advancedCount = tree.Value.String.Count;

            var forestSequence = ProductParse(nextParserSequence, @string, offset + advancedCount);
            foreach (var treeSequence in forestSequence)
            {
                yield return treeSequence.Prepend(tree);
            }
        }
    }
}
#endif