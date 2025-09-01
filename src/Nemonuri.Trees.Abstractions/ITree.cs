namespace Nemonuri.Trees;

/// <summary>
/// Represents a tree structured object.
/// </summary>
/// <inheritdoc cref="ITreeWalker{_,_,_,_}" path="/typeparam" />
public interface ITree<TElement, TAggregation, TAncestor, TAncestorsAggregation>
#if NET9_0_OR_GREATER
        where TElement : allows ref struct
        where TAggregation : allows ref struct
        where TAncestor : allows ref struct
        where TAncestorsAggregation : allows ref struct
#endif
{
    /// <summary>
    /// Gets the root of the tree.
    /// </summary>
    TElement Root { get; }

    /// <summary>
    /// Gets the mechanism to aggregate the tree.
    /// </summary>
    ITreeWalker<TElement, TAggregation, TAncestor, TAncestorsAggregation> TreeWalker { get; }
}