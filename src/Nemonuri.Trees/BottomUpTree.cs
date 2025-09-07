
namespace Nemonuri.Trees;

internal class BottomUpTree<TElement> : ITree<TElement>
{
    public TElement Value { get; }

    private readonly IEnumerable<ITree<TElement>> _originalChildren;
    private readonly IParentTreeBinder<TElement> _parentTreeBinder;

    private IEnumerable<ITree<TElement>>? _children;

    public BottomUpTree
    (
        TElement value,
        IEnumerable<ITree<TElement>>? children,
        IParentTreeBinder<TElement> parentTreeBinder
    )
    {
        Guard.IsNotNull(value);
        Guard.IsNotNull(parentTreeBinder);

        Value = value;
        _originalChildren = children ?? [];
        _parentTreeBinder = parentTreeBinder;
    }

    public IEnumerable<ITree<TElement>> Children =>
        _children ??= _originalChildren.Select(child => _parentTreeBinder.BindParent(child, this));

    public bool TryGetParent([NotNullWhen(true)] out ITree<TElement>? parent)
    {
        parent = default;
        return false;
    }
}
