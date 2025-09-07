

using Nemonuri.Trees.Abstractions;

namespace Nemonuri.Trees;

public class TopDownTreeFactory<TElement> : ITopDownTreeFactory<TElement>
{
    public static readonly TopDownTreeFactory<TElement> Instance = new();

    public TopDownTreeFactory() { }

    public ITree<TElement> Create(TElement value, IChildrenProvider<TElement> childrenProvider, ITopDownTreeFactory<TElement> childToTreeFactory, ITree<TElement>? parent)
    {
        return TreeTheory.CreateTopDown(value, childrenProvider, childToTreeFactory, parent);
    }
}