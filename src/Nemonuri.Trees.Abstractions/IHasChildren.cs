namespace Nemonuri.Trees;

public interface IHasChildren<TNode>
{
    IEnumerable<TNode> Children { get; }
}
