
namespace Nemonuri.Trees;

public readonly struct TreeIndexProjector<TElement> : IAncestorConverter<TElement, int?>
{
    public static readonly IAncestorConverter<TElement, int?> BoxedInstance = (TreeIndexProjector<TElement>)default;

    public TreeIndexProjector() { }

    public int? ConvertToAncestor(TElement element, int? elementIndex) => elementIndex;
}
