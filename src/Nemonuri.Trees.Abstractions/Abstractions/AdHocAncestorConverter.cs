namespace Nemonuri.Trees.Abstractions;

/// <summary>
/// An ad-hoc implementation of <see cref="IAncestorConverter{_,_}"/>
/// </summary>
/// <inheritdoc cref="IAncestorConverter{_,_}" path="/typeparam" />
public readonly struct AdHocAncestorConverter<TElement, TAncestor> :
    IAncestorConverter<TElement, TAncestor>
#if NET9_0_OR_GREATER
    where TElement : allows ref struct
    where TAncestor : allows ref struct
#endif
{
    private readonly Func<TElement, int?, TAncestor> _convertToAncestorImplementation;

    /// <summary>
    /// Initializes a new instance of the <see cref="AdHocAncestorConverter{_,_}"/> structure with the specified implementations.
    /// </summary>
    /// <param name="convertToAncestorImplementation">
    /// The implementation of <see cref="IAncestorConverter{_,_}.ConvertToAncestor(_, int?)"/>
    /// </param>
    public AdHocAncestorConverter(Func<TElement, int?, TAncestor> convertToAncestorImplementation)
    {
        Debug.Assert(convertToAncestorImplementation is not null);

        _convertToAncestorImplementation = convertToAncestorImplementation;
    }

    /// <inheritdoc cref="IAncestorConverter{_,_}.ConvertToAncestor(_, int?)" />
    public TAncestor ConvertToAncestor(TElement element, int? elementIndex) =>
        _convertToAncestorImplementation(element, elementIndex);
}