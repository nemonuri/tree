namespace Nemonuri.Trees;

public interface IDynamicAggragator
<TPreElement, TPreAggregation, TPostElement, TPostAggregation, TPostAggregationCollection>
#if NET9_0_OR_GREATER
    where TPreElement : allows ref struct
    where TPostElement : allows ref struct
    where TPostAggregation : allows ref struct
#endif
    where TPreAggregation : IEnumerable<TPostElement>
#if NET9_0_OR_GREATER
    , allows ref struct
#endif
    where TPostAggregationCollection : IEnumerable<TPostAggregation>
#if NET9_0_OR_GREATER
    , allows ref struct
#endif
{
    TPreAggregation InitialPreAggregation { get; }

    TPreAggregation PreAggregate
    (
        TPostAggregationCollection postAggregationCollection,
        TPostAggregation postAggregation,
        int? postAggregationIndex,
        TPreAggregation preAggregation,
        TPreElement preElement,
        int? preElementIndex
    );

    bool TryAggregate
    (
        TPreAggregation preAggregation,
        TPostAggregation siblingsAggregation,
        int? siblingsAggregationIndex,
        TPostAggregationCollection childrenAggregationCollection,
        TPostAggregation childrenAggregation,
        int? childrenAggregationIndex,
        TPostElement postElement,
        int postElementIndex,
        [NotNullWhen(true)] out TPostAggregation? aggregation
    );

    TPostAggregation DefaultPostAggregation { get; }

    TPostAggregationCollection InitialPostAggregationCollection { get; }

    bool TryAggregateAsCollection
    (
        TPostAggregationCollection currentPostAggregationCollection,
        TPostAggregation aggregation,
        [NotNullWhen(true)] out TPostAggregationCollection? postAggregationCollection
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