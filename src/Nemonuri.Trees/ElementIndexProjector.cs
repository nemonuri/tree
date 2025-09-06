#if false

namespace Nemonuri.Trees;

public readonly struct ElementIndexProjector<TElement> : IAncestorConverter<TElement, int?>
{
    public static readonly IAncestorConverter<TElement, int?> BoxedInstance = (ElementIndexProjector<TElement>)default;

    public ElementIndexProjector() { }

    public int? ConvertToAncestor(TElement element, int? elementIndex) => elementIndex;
}

#endif