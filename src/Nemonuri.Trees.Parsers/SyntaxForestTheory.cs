namespace Nemonuri.Trees.Parsers;

public static class SyntaxForestTheory
{
    public static ISyntaxForest<TChar> ToSyntaxTree<TChar>
    (
        this IEnumerable<IBinderSyntaxTree<TChar>> syntaxTrees
    )
    {
        return new SyntaxForest<TChar>(syntaxTrees);
    }
}
