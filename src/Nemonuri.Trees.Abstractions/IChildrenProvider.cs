namespace Nemonuri.Trees;

/// <summary>
/// Defines a method to get children nodes from a node of a specified type.
/// </summary>
/// <typeparam name="TNode">The type of nodes</typeparam>
public interface IChildrenProvider<TNode>
#if NET9_0_OR_GREATER
    where TNode : allows ref struct
#endif
{
    /// <summary>
    /// Gets enumerable children nodes from a specified node.
    /// </summary>
    /// <param name="source">The node whose children nodes to get.</param>
    /// <returns>The enumerable children nodes of <paramref name="source"/>.</returns>
    IEnumerable<TNode> GetChildren(TNode source);
}
