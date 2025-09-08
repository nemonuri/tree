namespace Nemonuri.Trees.Abstractions;

public readonly struct TrivialChildrenProvider<TNode> : IChildrenProvider<TNode>
    where TNode : ISupportChildren<TNode>
#if NET9_0_OR_GREATER
    , allows ref struct
#endif
{
    public readonly static IChildrenProvider<TNode> BoxedInstance = (TrivialChildrenProvider<TNode>)default;

    public TrivialChildrenProvider() { }

    public IEnumerable<TNode> GetChildren(TNode source)
    {
        Debug.Assert(source is not null);
        return source.Children;
    }
}
