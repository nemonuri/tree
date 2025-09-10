
namespace Nemonuri.Trees.Parsers;

public static class SyntaxTreeTheory
{
    public static IBinderSyntaxTree<TChar> CreateLeaf<TChar>(SyntaxTreeInfo<TChar> value)
    {
        return new SyntaxLeaf<TChar>(value, default);
    }

    public static IBinderSyntaxTree<TChar> CreateBranch<TChar>
    (
        SyntaxTreeInfo<TChar> value,
        IEnumerable<IBinderSyntaxTree<TChar>> children
    )
    {
        return new SyntaxBranch<TChar>(value, children, default);
    }
}
