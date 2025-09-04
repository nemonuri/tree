#if false

namespace Nemonuri.Trees.Paths;

public readonly record struct IndexedElement<TElement>
{
    public TElement? Element { get; }
    public int? Index { get; }

    public IndexedElement(TElement? element, int? index)
    {
        Element = element;
        Index = index;
    }

    public IndexedElement(TElement? element) : this(element, default)
    { }

    public void Deconstruct(out TElement? element, out int? index)
    {
        element = Element;
        index = Index;
    }

    public static implicit operator IndexedElement<TElement>((TElement? Element, int? Index) v) => new(v.Element, v.Index);
}

#endif