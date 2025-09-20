using CommunityToolkit.Diagnostics;

namespace Nemonuri.Trees.Forests;

public class ForestAggregator
<
    TElement, TAggregation,
    TElementFactory, TAggregationCollection,
    TElementComplex,
    TAncestor, TAncestorsAggregation,
    TFlowContext
> :
    IAggregator3DWithFlowContext<TElementComplex, TAggregationCollection, TAncestor, TAncestorsAggregation, TFlowContext>
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
    private readonly IForestAggregatorBase
    <
        TElement, TAggregation,
        TElementFactory, TAggregationCollection,
        TElementComplex,
        TAncestor, TAncestorsAggregation,
        TFlowContext
    > _forestAggregatorBase;

    public ForestAggregator(IForestAggregatorBase<TElement, TAggregation, TElementFactory, TAggregationCollection, TElementComplex, TAncestor, TAncestorsAggregation, TFlowContext> forestAggregatorBase)
    {
        Guard.IsNotNull(forestAggregatorBase);
        _forestAggregatorBase = forestAggregatorBase;
    }

    public TFlowContext InitialFlowContext => _forestAggregatorBase.InitialFlowContext;

    public TAncestorsAggregation InitialAncestorsAggregation => _forestAggregatorBase.InitialAncestorsAggregation;

    public TAncestorsAggregation AggregateAncestor(TAncestorsAggregation ancestorsAggregation, TAncestor ancestor) =>
        _forestAggregatorBase.AggregateAncestor(ancestorsAggregation, ancestor);

    public TAggregationCollection EmptyAggregationCollection => _forestAggregatorBase.EmptyAggregationCollection;

    public TAggregationCollection InitialAggregation => _forestAggregatorBase.InitialAggregationCollection;

    public TAggregationCollection Aggregate
    (
        TAncestorsAggregation ancestorsAggregation,
        TAggregationCollection siblingsAggregationCollection,
        TAggregationCollection childrenAggregationCollection,
        TElementComplex elementComplex,
        scoped ref TFlowContext flowContext
    )
    {
        int elementFactoryIndex = 0;
        int elementIndex = 0;
        int siblingsAggregationIndex = 0;
        int childrenAggregationIndex = 0;
        TAggregationCollection aggregationCollection = EmptyAggregationCollection;


        foreach (var elementFactory in GetElementFactories(ancestorsAggregation, elementComplex))
        {
            foreach (var siblingsAggregation in siblingsAggregationCollection)
            {
                foreach (var childrenAggregation in childrenAggregationCollection)
                {
                    var elements = GetElements
                    (
                        ancestorsAggregation, siblingsAggregationCollection, childrenAggregationCollection,
                        elementComplex, ref flowContext,

                        elementFactory, elementFactoryIndex,
                        siblingsAggregation, siblingsAggregationIndex,
                        childrenAggregation, childrenAggregationIndex
                    );
                    foreach (var element in elements)
                    {
                        if
                        (
                            !TryAggregate
                            (
                                ancestorsAggregation, siblingsAggregationCollection, childrenAggregationCollection,
                                elementComplex, ref flowContext,

                                elementFactory, elementFactoryIndex,
                                siblingsAggregation, siblingsAggregationIndex,
                                childrenAggregation, childrenAggregationIndex,
                                element, elementIndex,
                                out var aggregation
                            )
                        )
                        { goto Continue; }

                        if
                        (
                            !TryAggregateCollection
                            (
                                ancestorsAggregation, siblingsAggregationCollection, childrenAggregationCollection,
                                elementComplex, ref flowContext,

                                aggregationCollection, aggregation,
                                out var ac1
                            )
                        )
                        { goto Continue; }

                        aggregationCollection = ac1;

                    Continue:
                        elementIndex++;
                    }

                    childrenAggregationIndex++;
                }

                siblingsAggregationIndex++;
            }

            elementFactoryIndex++;
        }

        return aggregationCollection;
    }

    private IEnumerable<TElementFactory> GetElementFactories
    (
        TAncestorsAggregation ancestorsAggregation,
        TElementComplex elementComplex
    )
    {
        return _forestAggregatorBase.GetElementFactories(ancestorsAggregation, elementComplex);
    }

    private IEnumerable<TElement> GetElements
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
    )
    {
        return _forestAggregatorBase.GetElements
        (
            ancestorsAggregation,
            siblingsAggregationCollection, childrenAggregationCollection,
            elementComplex, in flowContext,

            elementFactory, elementFactoryIndex,
            siblingsAggregation, siblingsAggregationIndex,
            childrenAggregation, childrenAggregationIndex
        );
    }

    private bool TryAggregate
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
    )
    {
        return _forestAggregatorBase.TryAggregate
        (
            ancestorsAggregation,
            siblingsAggregationCollection, childrenAggregationCollection,
            elementComplex, ref flowContext,

            elementFactory, elementFactoryIndex,
            siblingsAggregation, siblingsAggregationIndex,
            childrenAggregation, childrenAggregationIndex,
            element, elementIndex,
            out aggregation
        );
    }

    private bool TryAggregateCollection
    (
        TAncestorsAggregation ancestorsAggregation,
        TAggregationCollection siblingsAggregationCollection,
        TAggregationCollection childrenAggregationCollection,
        TElementComplex elementComplex,
        scoped ref readonly TFlowContext flowContext,

        TAggregationCollection aggregationCollection,
        TAggregation aggregation,
        [NotNullWhen(true)] out TAggregationCollection? nextAggregationCollection
    )
    {
        return _forestAggregatorBase.TryAggregateCollection
        (
            ancestorsAggregation,
            siblingsAggregationCollection, childrenAggregationCollection,
            elementComplex, in flowContext,

            aggregationCollection, aggregation,
            out nextAggregationCollection
        );
    }

    
}
