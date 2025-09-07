namespace Nemonuri.Trees;

/// <summary>
/// Exposes own children nodes.
/// </summary>
/// <typeparam name="TNode">The type of children nodes.</typeparam>
public interface ISupportChildren<out TNode>
#if NET9_0_OR_GREATER
    where TNode : allows ref struct
#endif
{
    /// <summary>
    /// Gets enumerable children nodes.
    /// </summary>
    IEnumerable<TNode> Children { get; }
}
