#if false

namespace Nemonuri.Trees;

public interface IChildrenOnlyTree<TElement, out TSelf> : ISupportChildren<TSelf>
    where TSelf : IChildrenOnlyTree<TElement, TSelf>
    where TElement : allows ref struct

{
    /// <summary>
    /// Gets the value of the tree.
    /// </summary>
    TElement Value { get; }
}

#endif