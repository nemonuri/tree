namespace Nemonuri.Trees;

public interface IChildrenOnlyTree<TElement, out TSelf> : ISupportChildren<TSelf>
    where TSelf : IChildrenOnlyTree<TElement, TSelf>
#if NET9_0_OR_GREATER
    where TElement : allows ref struct
#endif
{ 
    /// <summary>
    /// Gets the value of the tree.
    /// </summary>
    TElement Value { get; }
}