#if REF_TREE

namespace Nemonuri.Trees;

public readonly ref struct RefTree<TElement, TAggregation, TAncestor, TAncestorsAggregation> :
    ITree<TElement, TAggregation, TAncestor, TAncestorsAggregation>
#if NET9_0_OR_GREATER
    where TElement : allows ref struct
    where TAggregation : allows ref struct
    where TAncestor : allows ref struct
    where TAncestorsAggregation : allows ref struct
#endif
{
    private readonly TElement _root;
    private readonly ITreeWalker<TElement, TAggregation, TAncestor, TAncestorsAggregation> _treeWalker;

    public RefTree(TElement root, ITreeWalker<TElement, TAggregation, TAncestor, TAncestorsAggregation> treeWalker)
    {
        _root = root;
        _treeWalker = treeWalker;
    }

    /// <inheritdoc cref="ITree{_,_,_,_}.Root" />
    public TElement Root => _root;

    /// <inheritdoc cref="ITree{_,_,_,_}.TreeWalker" />
    public ITreeWalker<TElement, TAggregation, TAncestor, TAncestorsAggregation> TreeWalker => _treeWalker;
}

#endif