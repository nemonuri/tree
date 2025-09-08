
namespace Nemonuri.Trees.Parsers;

public static class SyntaxTreeTheory
{
    public static ISyntaxTree<TChar> CreateLeaf<TChar>(SyntaxTreeInfo<TChar> value)
    {
        return new SyntaxLeaf<TChar>(value);
    }
}
