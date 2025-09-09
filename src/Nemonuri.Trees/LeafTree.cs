#if false

namespace Nemonuri.Trees;

internal class LeafTree<TValue> : IGeneralRoseTree<TValue>
{
    public TValue Value { get; }

    public LeafTree(TValue value)
    {
        Guard.IsNotNull(value);
        Value = value;
    }

    public IEnumerable<IGeneralRoseTree<TValue>> Children => [];

    IEnumerable<IGeneralTree> ITree<IGeneralTree>.Children => Children;
}

#endif