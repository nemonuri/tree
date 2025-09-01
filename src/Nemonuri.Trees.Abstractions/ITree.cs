namespace Nemonuri.Trees;

/// <summary>
/// Represents a tree structured object.
/// </summary>
/// <inheritdoc cref="ITreeAggregator{_,_,_,_}" path="/typeparam" />
public interface ITree<TElement>
#if NET9_0_OR_GREATER
        where TElement : allows ref struct
#endif
{
    /// <summary>
    /// Gets the root of the tree.
    /// </summary>
    TElement Root { get; }

    /// <summary>
    /// Gets the mechanism to get children of the tree.
    /// </summary>
    IChildrenProvider<TElement> ChildrenProvider { get; }
}