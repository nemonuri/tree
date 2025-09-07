namespace Nemonuri.Trees;

/// <summary>
/// Exposes a method to get the parent node.
/// </summary>
/// <typeparam name="TNode">The type of the parent node.</typeparam>
public interface ISupportParent<TNode>
#if NET9_0_OR_GREATER
    where TNode : allows ref struct
#endif
{
    bool TryGetParent([NotNullWhen(true)] out TNode? parent);
}
