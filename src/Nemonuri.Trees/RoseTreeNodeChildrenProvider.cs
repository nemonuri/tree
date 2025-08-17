namespace Nemonuri.Trees;

public class RoseTreeNodeChildrenProvider<T> : IChildrenProvider<RoseTreeNode<T>>
{
    private readonly TrivialChildrenProvider<RoseTreeNode<T>> _internalSource;

    public RoseTreeNodeChildrenProvider()
    {
        _internalSource = new();
    }

    public IEnumerable<RoseTreeNode<T>> GetChildren(RoseTreeNode<T> source) =>
        _internalSource.GetChildren(source);
}
