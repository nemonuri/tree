using System.Collections;

namespace Nemonuri.Trees.Parsers;

internal class SyntaxForest<TChar> : ISyntaxForest<TChar>
{
    private readonly IEnumerable<ISyntaxTree<TChar>> _syntaxTrees;

    public SyntaxForest(IEnumerable<ISyntaxTree<TChar>> syntaxTrees)
    {
        Guard.IsNotNull(syntaxTrees);
        _syntaxTrees = syntaxTrees;
    }

    public IEnumerator<ISyntaxTree<TChar>> GetEnumerator() => _syntaxTrees.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
