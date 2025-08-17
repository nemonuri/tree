namespace Nemonuri.Trees;

public interface IChildrenProvider<TNode>
{ 
    IEnumerable<TNode> GetChildren(TNode source);
}

public interface IHasChildren<TNode>
{
    IEnumerable<TNode> Children { get; }
}