
namespace Nemonuri.Trees;

internal class GeneralBottomUpRoseTree<TValue> : IGeneralBottomUpRoseTree<TValue>
{
    public TValue Value { get; }
    private readonly IGeneralBottomUpRoseTree<TValue>? _parent;
    private readonly IEnumerable<IGeneralBottomUpRoseTree<TValue>> _unboundChildren;
    private IEnumerable<IGeneralBottomUpRoseTree<TValue>>? _childrenCache;

    public GeneralBottomUpRoseTree
    (
        TValue value,
        IEnumerable<IGeneralBottomUpRoseTree<TValue>> unboundChildren,
        IEnumerable<IGeneralBottomUpRoseTree<TValue>>? childrenCache,
        IGeneralBottomUpRoseTree<TValue>? parent
    )
    {
        Value = value;
        _parent = parent;
        _childrenCache = childrenCache;
        _unboundChildren = unboundChildren;
    }

    public bool HasParent => _parent is not null;

    public IGeneralBottomUpRoseTree<TValue> GetParent() => _parent ?? ThrowHelper.ThrowArgumentNullException<IGeneralBottomUpRoseTree<TValue>>();

    public IGeneralBottomUpRoseTree<TValue> BindParent(IGeneralBottomUpRoseTree<TValue> parent)
    {
        return new GeneralBottomUpRoseTree<TValue>(Value, _unboundChildren, _childrenCache, parent);
    }

    public IEnumerable<IGeneralBottomUpRoseTree<TValue>> UnboundChildren => _unboundChildren;

    public IEnumerable<IGeneralBottomUpRoseTree<TValue>> Children => _childrenCache ??=
        _unboundChildren.Select(child => child.BindParent(this));


    IGeneralBinderRoseTree<TValue> ISupportParent<IGeneralBinderRoseTree<TValue>>.GetParent() => GetParent();
    IGeneralBinderTree ISupportParent<IGeneralBinderTree>.GetParent() => GetParent();
    IEnumerable<IGeneralBinderRoseTree<TValue>> ISupportChildren<IGeneralBinderRoseTree<TValue>>.Children => Children;
    IEnumerable<IGeneralRoseTree<TValue>> ISupportChildren<IGeneralRoseTree<TValue>>.Children => Children;
    IEnumerable<IGeneralBinderTree> ISupportChildren<IGeneralBinderTree>.Children => Children;
    IEnumerable<IGeneralTree> ISupportChildren<IGeneralTree>.Children => Children;
}