using System.Collections;

namespace Nemonuri.Trees.Parsers;

internal class SyntaxForest<TChar> : ISyntaxForest<TChar>
{
    private readonly IEnumerable<IBinderSyntaxTree<TChar>> _syntaxTrees;

    public SyntaxForest(IEnumerable<IBinderSyntaxTree<TChar>> syntaxTrees)
    {
        Guard.IsNotNull(syntaxTrees);
        _syntaxTrees = syntaxTrees;
    }

    public IEnumerator<IBinderSyntaxTree<TChar>> GetEnumerator() => _syntaxTrees.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
