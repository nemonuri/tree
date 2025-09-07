
namespace Nemonuri.Trees;

internal class LeafTree<TElement> : ITree<TElement>
{
    public TElement Value { get; }

    public LeafTree(TElement value)
    {
        Guard.IsNotNull(value);
        Value = value;
    }

    public IEnumerable<ITree<TElement>> Children => [];

    public bool TryGetParent([NotNullWhen(true)] out ITree<TElement>? parent)
    {
        parent = default;
        return false;
    }
}