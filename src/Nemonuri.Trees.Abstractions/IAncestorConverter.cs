namespace Nemonuri.Trees;

/// <summary>
/// Defines a method to convert the specified aggregating context to an ancestor element.
/// </summary>
/// <inheritdoc cref="IAggregator3D{_,_,_,_}" path="/typeparam" />
public interface IAncestorConverter
<TElement, TAncestor>
#if NET9_0_OR_GREATER
    where TElement : allows ref struct
    where TAncestor : allows ref struct
#endif
{
    /// <summary>
    /// Convert the specified aggregating context to an ancestor element.
    /// </summary>
    /// <param name="element">The element in the aggregating context.</param>
    /// <param name="elementIndex">
    /// <see langword="null"/> if <paramref name="element"/> is root;
    /// otherwise, the index of <paramref name="element"/> in the aggregating context.
    /// </param>
    /// <returns>An ancestor element that is equivalent to the specified aggregating context.</returns>
    TAncestor ConvertToAncestor(TElement element, int? elementIndex);
}

public interface IMultiAxisAncestorConverter<TElement, TAncestor>
#if NET9_0_OR_GREATER
    where TElement : allows ref struct
    where TAncestor : allows ref struct
#endif
{ 
    TAncestor ConvertToAncestor(TElement element, int? axisIndex, int? elementIndex);
}