
namespace Nemonuri.Trees.Implicit;

public class ImplicitBottomUpRoseTree<TValue> : IGeneralBottomUpRoseTree<TValue>
{
    public TValue Value { get; }
    private readonly IEnumerable<IGeneralBottomUpRoseTree<TValue>> _unboundChildren;
    private IEnumerable<IGeneralBottomUpRoseTree<TValue>>? _childrenCache;
    private readonly IGeneralBottomUpRoseTree<TValue>? _parent;

    public ImplicitBottomUpRoseTree
    (
        TValue value,
        IEnumerable<IGeneralBottomUpRoseTree<TValue>>? unboundChildren,
        IEnumerable<IGeneralBottomUpRoseTree<TValue>>? childrenCache,
        IGeneralBottomUpRoseTree<TValue>? parent
    )
    {
        Value = value;
        _unboundChildren = unboundChildren ?? [];
        _childrenCache = childrenCache;
        _parent = parent;
    }

    public ImplicitBottomUpRoseTree
    (
        TValue value,
        IEnumerable<IGeneralBottomUpRoseTree<TValue>> unboundChildren
    ) :
    this(value, unboundChildren, null, null)
    { }

    public IEnumerable<IGeneralBottomUpRoseTree<TValue>> UnboundChildren => _unboundChildren;

    public IGeneralBottomUpRoseTree<TValue> BindParent(IGeneralBottomUpRoseTree<TValue> parent)
    {
        return new ImplicitBottomUpRoseTree<TValue>(Value, _unboundChildren, _childrenCache, parent);
    }

    public IEnumerable<IGeneralBottomUpRoseTree<TValue>> Children => _childrenCache ??=
        _unboundChildren.Select(child => child.BindParent(this));

    public bool HasParent => _parent is not null;

    public IGeneralBottomUpRoseTree<TValue> GetParent()
    {
        Guard.IsNotNull(_parent);
        return _parent;
    }

    public static implicit operator ImplicitBottomUpRoseTree<TValue>(TValue v) => new(v, []);
    public static implicit operator ImplicitBottomUpRoseTree<TValue>((TValue Value, ImplicitBottomUpRoseTree<TValue>[] Children) v) =>
        new(v.Value, v.Children);

    IEnumerable<IGeneralBinderRoseTree<TValue>> ISupportChildren<IGeneralBinderRoseTree<TValue>>.Children => Children;
    IGeneralBinderRoseTree<TValue> ISupportParent<IGeneralBinderRoseTree<TValue>>.GetParent() => GetParent();
    IEnumerable<IGeneralRoseTree<TValue>> ISupportChildren<IGeneralRoseTree<TValue>>.Children => Children;
    IEnumerable<IGeneralBinderTree> ISupportChildren<IGeneralBinderTree>.Children => Children;
    IGeneralBinderTree ISupportParent<IGeneralBinderTree>.GetParent() => GetParent();
    IEnumerable<IGeneralTree> ISupportChildren<IGeneralTree>.Children => Children;
}