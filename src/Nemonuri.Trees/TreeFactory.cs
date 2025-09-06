

using Nemonuri.Trees.Abstractions;

namespace Nemonuri.Trees;

public class TreeFactory<TElement> : ITreeFactory<TElement>
{
    public static readonly TreeFactory<TElement> Instance = new();

    public TreeFactory() { }

    public ITree<TElement> Create(TElement root, IChildrenProvider<TElement> childrenProvider, ITree<TElement>? parent)
    {
        return new Tree<TElement>(root, childrenProvider, this, parent);
    }
}