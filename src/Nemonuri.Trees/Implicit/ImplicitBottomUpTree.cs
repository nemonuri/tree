

namespace Nemonuri.Trees.Implicit;

public class ImplicitBottomUpTree<TElement> : ITree<TElement>
{
    public ImplicitBottomUpTree(TElement value, IEnumerable<ITree<TElement>> children)
    {
        Value = value;
        Children = children;
    }

    public TElement Value { get; }

    public IEnumerable<ITree<TElement>> Children { get; }

    public bool TryGetParent([NotNullWhen(true)] out ITree<TElement>? parent)
    {
        parent = default;
        return false;
    }

    public static implicit operator ImplicitBottomUpTree<TElement>(TElement v) => new(v, []);
    public static implicit operator ImplicitBottomUpTree<TElement>((TElement Value, ImplicitBottomUpTree<TElement>[] Children) v) =>
        new(v.Value, v.Children);
}