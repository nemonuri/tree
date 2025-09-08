namespace Nemonuri.Trees;

/// <summary>
/// Represents a tree structured object.
/// </summary>
/// <inheritdoc cref="ITreeAggregator{_,_,_,_}" path="/typeparam" />
public interface ITree<TElement, TSelf> :
    ISupportChildren<TSelf>,
    ISupportParent<TSelf>
    where TSelf : ITree<TElement, TSelf>
#if NET9_0_OR_GREATER
    , allows ref struct
    where TElement : allows ref struct
#endif
{
    // <summary>
    // Gets the mechanism to get children of the tree.
    // </summary>
    //IChildrenProvider<TElement> ChildrenProvider { get; }

    //ITreeFactory<TElement> TreeFactory { get; }

    /// <summary>
    /// Gets the value of the tree.
    /// </summary>
    TElement Value { get; }
}

public interface ITree<TElement> : ITree<TElement, ITree<TElement>>
#if NET9_0_OR_GREATER
    where TElement : allows ref struct
#endif
{ }