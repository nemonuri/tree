namespace Nemonuri.Trees.Abstractions;

public readonly struct TrivialChildrenProvider<TNode> : IChildrenProvider<TNode>
    where TNode : ISupportChildren<TNode>
{
    public readonly static IChildrenProvider<TNode> BoxedInstance = (TrivialChildrenProvider<TNode>)default;

    public TrivialChildrenProvider() { }

    public IEnumerable<TNode> GetChildren(TNode source)
    {
        Debug.Assert(source is not null);
        return source.Children;
    }
}
