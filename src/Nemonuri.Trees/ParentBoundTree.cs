

namespace Nemonuri.Trees;

internal class ParentBoundTree<TElement> :
    ITree<TElement>, ISupportInternalSource<ITree<TElement>>
{
    private readonly ITree<TElement>? _parent;
    private readonly ITree<TElement> _source;

    public ParentBoundTree(ITree<TElement> source, ITree<TElement>? parent)
    {
        Guard.IsNotNull(source);

        _parent = parent;
        _source = source;
    }

    public TElement Value => _source.Value;

    public IEnumerable<ITree<TElement>> Children => _source.Children;

    public bool TryGetParent([NotNullWhen(true)] out ITree<TElement>? parent)
    {
        parent = _parent;
        return parent is not null;
    }

    ITree<TElement>? ISupportInternalSource<ITree<TElement>>.GetInternalSource() =>
        _source;
}
