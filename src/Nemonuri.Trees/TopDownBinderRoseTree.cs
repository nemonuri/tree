namespace Nemonuri.Trees;

internal class TopDownBinderRoseTree<TValue> : IGeneralBinderRoseTree<TValue>
{
    public TValue Value { get; }
    private readonly IChildrenProvider<TValue> _childrenProvider;
    private readonly IGeneralBinderRoseTree<TValue>? _parent;

    public IEnumerable<IGeneralBinderRoseTree<TValue>>? _childrenCache = null;

    public TopDownBinderRoseTree
    (
        TValue value,
        IChildrenProvider<TValue> childrenProvider,
        IGeneralBinderRoseTree<TValue>? parent
    )
    {
        Guard.IsNotNull(value);
        Guard.IsNotNull(childrenProvider);

        Value = value;
        _childrenProvider = childrenProvider;
        _parent = parent;
    }

    public bool HasParent => _parent is not null;
    public IGeneralBinderRoseTree<TValue> GetParent() => _parent ?? ThrowHelper.ThrowArgumentNullException<IGeneralBinderRoseTree<TValue>>();

    public IEnumerable<IGeneralBinderRoseTree<TValue>> Children => _childrenCache ??=
        _childrenProvider.GetChildren(Value).Select(child => new TopDownBinderRoseTree<TValue>(child, _childrenProvider, this));

    IEnumerable<IGeneralRoseTree<TValue>> ISupportChildren<IGeneralRoseTree<TValue>>.Children => Children;
    IEnumerable<IGeneralBinderTree> ISupportChildren<IGeneralBinderTree>.Children => Children;
    IEnumerable<IGeneralTree> ISupportChildren<IGeneralTree>.Children => Children;
    IGeneralBinderTree ISupportParent<IGeneralBinderTree>.GetParent() => GetParent();
}

