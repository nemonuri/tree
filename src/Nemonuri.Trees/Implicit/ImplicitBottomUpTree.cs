
namespace Nemonuri.Trees.Implicit;

public class ImplicitBottomUpTree<TElement> : ITree<TElement>
{
    private readonly IEnumerable<ITree<TElement>> _originalChildren;
    private IEnumerable<ITree<TElement>>? _children;

    public ImplicitBottomUpTree(TElement value, IEnumerable<ITree<TElement>> children)
    {
        Value = value;
        _originalChildren = children;
    }

    public TElement Value { get; }

    public IEnumerable<ITree<TElement>> Children =>
        _children ??= _originalChildren.Select(child => ParentTreeBinder<TElement>.Instance.BindParent(child, this));

    public bool TryGetParent([NotNullWhen(true)] out ITree<TElement>? parent)
    {
        parent = default;
        return false;
    }

    public static implicit operator ImplicitBottomUpTree<TElement>(TElement v) => new(v, []);
    public static implicit operator ImplicitBottomUpTree<TElement>((TElement Value, ImplicitBottomUpTree<TElement>[] Children) v) =>
        new(v.Value, v.Children);
}