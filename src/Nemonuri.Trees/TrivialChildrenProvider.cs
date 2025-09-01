namespace Nemonuri.Trees;

public class TrivialChildrenProvider<TNode> : IChildrenProvider<TNode>
    where TNode : ISupportChildren<TNode>
{
    public TrivialChildrenProvider() { }

    public IEnumerable<TNode> GetChildren(TNode source)
    {
        Debug.Assert(source is not null);
        return source.Children;
    }
}
