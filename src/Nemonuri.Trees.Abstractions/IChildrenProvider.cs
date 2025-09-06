namespace Nemonuri.Trees;

#if false
/// <summary>
/// Defines a method to get children nodes from a node of a specified type.
/// </summary>
/// <typeparam name="TSource">The type of source elements</typeparam>
public interface IChildrenProvider<TSource, TResult>

    where TSource : allows ref struct
    where TResult : allows ref struct

{
    /// <summary>
    /// Gets enumerable children nodes from a specified source.
    /// </summary>
    /// <param name="source">The node whose children nodes to get.</param>
    /// <returns>The enumerable children nodes of <paramref name="source"/>.</returns>
    IEnumerable<TResult> GetChildren(TSource source);
}
#endif

/// <summary>
/// Defines a method to get children nodes from a node of a specified type.
/// </summary>
/// <typeparam name="TElement">The type of nodes</typeparam>
public interface IChildrenProvider<TElement>
#if NET9_0_OR_GREATER
    where TElement : allows ref struct
#endif
{ 
    /// <summary>
    /// Gets enumerable children nodes from a specified source.
    /// </summary>
    /// <param name="source">The node whose children nodes to get.</param>
    /// <returns>The enumerable children nodes of <paramref name="source"/>.</returns>
    IEnumerable<TElement> GetChildren(TElement source);   
}
