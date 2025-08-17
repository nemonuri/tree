namespace Nemonuri.Trees;

public class AdHocChildrenProvider<TNode> : IChildrenProvider<TNode>
{
    public Func<TNode, IEnumerable<TNode>> ChildrenProvider { get; }

    public AdHocChildrenProvider(Func<TNode, IEnumerable<TNode>> childrenProvider)
    {
        Debug.Assert(childrenProvider is not null);
        ChildrenProvider = childrenProvider;
    }

    public IEnumerable<TNode> GetChildren(TNode source) =>
        ChildrenProvider(source);
}