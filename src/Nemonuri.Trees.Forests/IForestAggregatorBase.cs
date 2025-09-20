namespace Nemonuri.Trees.Forests;

public interface IForestAggregatorBase
<
    TElement, TAggregation,
    TElementFactory, TAggregationCollection,
    TElementComplex,
    TAncestor, TAncestorsAggregation,
    TFlowContext
>
#if NET9_0_OR_GREATER
    where TElement : allows ref struct
    where TAggregation : allows ref struct
    where TElementFactory : allows ref struct
    where TAncestor : allows ref struct
    where TAncestorsAggregation : allows ref struct
    where TFlowContext : allows ref struct
#endif
    where TAggregationCollection : IEnumerable<TAggregation>
#if NET9_0_OR_GREATER
        , allows ref struct
#endif
{
    TFlowContext InitialFlowContext { get; }

    TAncestorsAggregation InitialAncestorsAggregation { get; }

    TAncestorsAggregation AggregateAncestor(TAncestorsAggregation ancestorsAggregation, TAncestor ancestor);

    TAggregationCollection EmptyAggregationCollection { get; }

    TAggregationCollection InitialAggregationCollection { get; }

    IEnumerable<TElementFactory> GetElementFactories
    (
        TAncestorsAggregation ancestorsAggregation,
        TElementComplex elementComplex
    );

    IEnumerable<TElement> GetElements
    (
        TAncestorsAggregation ancestorsAggregation,
        TAggregationCollection siblingsAggregationCollection,
        TAggregationCollection childrenAggregationCollection,
        TElementComplex elementComplex,
        scoped ref readonly TFlowContext flowContext,

        TElementFactory elementFactory,
        int elementFactoryIndex,
        TAggregation siblingsAggregation,
        int siblingsAggregationIndex,
        TAggregation childrenAggregation,
        int childrenAggregationIndex
    );

    bool TryAggregate
    (
        TAncestorsAggregation ancestorsAggregation,
        TAggregationCollection siblingsAggregationCollection,
        TAggregationCollection childrenAggregationCollection,
        TElementComplex elementComplex,
        scoped ref TFlowContext flowContext,

        TElementFactory elementFactory,
        int elementFactoryIndex,
        TAggregation siblingsAggregation,
        int siblingsAggregationIndex,
        TAggregation childrenAggregation,
        int childrenAggregationIndex,
        TElement element,
        int elementIndex,
        [NotNullWhen(true)] out TAggregation? aggregation
    );

    bool TryAggregateCollection
    (
        TAncestorsAggregation ancestorsAggregation,
        TAggregationCollection siblingsAggregationCollection,
        TAggregationCollection childrenAggregationCollection,
        TElementComplex elementComplex,
        scoped ref readonly TFlowContext flowContext,

        TAggregationCollection aggregationCollection,
        TAggregation aggregation,
        [NotNullWhen(true)] out TAggregationCollection? nextAggregationCollection
    );
}
