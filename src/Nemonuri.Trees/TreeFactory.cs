
namespace Nemonuri.Trees;

public class TreeFactory<TElement> : ITreeFactory<TElement>
{
    public TreeFactory() { }

    public ITree<TElement> Create(TElement root, IChildrenProvider<TElement> childrenProvider)
    {
        return new Tree<TElement>(root, childrenProvider);
    }
}