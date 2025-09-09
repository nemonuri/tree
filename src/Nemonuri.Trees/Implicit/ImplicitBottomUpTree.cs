namespace Nemonuri.Trees.Implicit;

public class ImplicitBottomUpRoseTree<TValue> :
    IBottomUpRoseTree<TValue, ImplicitBottomUpRoseTree<TValue>>
{
    public TValue Value { get; }
    private readonly IEnumerable<ImplicitBottomUpRoseTree<TValue>> _unboundChildren;
    private IEnumerable<ImplicitBottomUpRoseTree<TValue>>? _childrenCache;
    private readonly ImplicitBottomUpRoseTree<TValue>? _parent;

    public ImplicitBottomUpRoseTree
    (
        TValue value,
        IEnumerable<ImplicitBottomUpRoseTree<TValue>> unboundChildren,
        IEnumerable<ImplicitBottomUpRoseTree<TValue>>? childrenCache,
        ImplicitBottomUpRoseTree<TValue>? parent
    )
    {
        Value = value;
        _unboundChildren = unboundChildren;
        _childrenCache = childrenCache;
        _parent = parent;
    }

    public ImplicitBottomUpRoseTree
    (
        TValue value,
        IEnumerable<ImplicitBottomUpRoseTree<TValue>> unboundChildren
    ) :
    this(value, unboundChildren, null, null)
    { }

    public IEnumerable<ImplicitBottomUpRoseTree<TValue>> UnboundChildren => _unboundChildren;

    public ImplicitBottomUpRoseTree<TValue> BindParent(ImplicitBottomUpRoseTree<TValue> parent)
    {
        return new ImplicitBottomUpRoseTree<TValue>(Value, _unboundChildren, _childrenCache, parent);
    }

    public IEnumerable<ImplicitBottomUpRoseTree<TValue>> Children => _childrenCache ??=
        _unboundChildren.Select(child => child.BindParent(this));

    public bool HasParent => _parent is not null;
    public ImplicitBottomUpRoseTree<TValue> GetParent() => _parent ?? ThrowHelper.ThrowArgumentNullException<ImplicitBottomUpRoseTree<TValue>>();
    
    public static implicit operator ImplicitBottomUpRoseTree<TValue>(TValue v) => new(v, []);
    public static implicit operator ImplicitBottomUpRoseTree<TValue>((TValue Value, ImplicitBottomUpRoseTree<TValue>[] Children) v) =>
        new(v.Value, v.Children);
}