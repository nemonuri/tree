
namespace Nemonuri.Trees.Parsers;

public static class SyntaxTreeTheory
{
    public static ISyntaxTree<TChar> CreateLeaf<TChar>(SyntaxTreeInfo<TChar> value)
    {
        return new SyntaxLeaf<TChar>(value, default);
    }

    public static ISyntaxTree<TChar> CreateBranch<TChar>
    (
        SyntaxTreeInfo<TChar> value,
        IEnumerable<ISyntaxTree<TChar>> children
    )
    {
        return new SyntaxBranch<TChar>(value, children, default);
    }
}
