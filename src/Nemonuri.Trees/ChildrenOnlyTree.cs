

namespace Nemonuri.Trees;

public class ChildrenOnlyTree<TElement> : ITree<TElement>
{
    public ChildrenOnlyTree(TElement value, IEnumerable<ITree<TElement>> children)
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

    public static implicit operator ChildrenOnlyTree<TElement>(TElement v) => new(v, []);
    public static implicit operator ChildrenOnlyTree<TElement>((TElement Value, ChildrenOnlyTree<TElement>[] Children) v) =>
        new(v.Value, v.Children);
}