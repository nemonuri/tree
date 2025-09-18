namespace Nemonuri.Trees;

public interface IDynamicAggragator
<TElement, TAggregation, TAncestor, TAncestorsAggregation, TAggregationCollection>
#if NET9_0_OR_GREATER
    where TElement : allows ref struct
    where TAggregation : allows ref struct
    where TAncestor : allows ref struct
    where TAncestorsAggregation : allows ref struct
#endif
    where TAggregationCollection : IEnumerable<TAggregation>
#if NET9_0_OR_GREATER
    , allows ref struct
#endif
{
    TAncestorsAggregation InitialAncestorsAggregation { get; }

    TAncestorsAggregation AggregateAncestor
    (
        TAncestorsAggregation ancestorsAggregation,
        TAncestor ancestor
    );

    bool TryAggregate
    (
        TAncestorsAggregation ancestorsAggregation,
        TAggregation siblingsAggregation,
        TAggregation childrenAggregation,
        TElement element,
        [NotNullWhen(true)] out TAggregation? aggregation
    );

    IEnumerable<TElement> GetAlternateElements(TAggregation siblingsAggregation, TElement element);

    TAggregationCollection InitialAggregationCollection { get; }

    bool TryAggregateAsCollection
    (
        TAggregationCollection prevAggregationCollection,
        TAggregation aggregation,
        [NotNullWhen(true)] out TAggregationCollection? aggregationCollection
    );
    
}

public interface IDynamicAncestorConverter
<TElement, TAncestor>
#if NET9_0_OR_GREATER
    where TElement : allows ref struct
    where TAncestor : allows ref struct
#endif
{ 
    TAncestor ConvertToAncestor
    (
        TElement element,
        int alternateElementIndex,
        int alternateSiblingsAggregationIndex,
        int alternateChildrenAggregationIndex,
        int? elementAsChildIndex
    );
}