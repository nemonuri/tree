
namespace Nemonuri.Trees.Tests;

public static class RoseTreeNodeTestTheory
{
    public static RoseTreeNode<T> CreateFromNodeValueAndChildrenValues<T>
    (
        T? nodeValue,
        IEnumerable<T>? childrenValues
    )
    { 
        return new(nodeValue, [.. childrenValues?.Select(a => new RoseTreeNode<T>(a))??[]]);
    }
}