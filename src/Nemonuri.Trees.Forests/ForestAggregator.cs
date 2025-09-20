using System.Diagnostics.CodeAnalysis;

namespace Nemonuri.Trees.Forests;

public class ForestAggregator
<
    TElement, TAggregation,
    TElementFactory, TAggregationCollection,
    TAncestor, TAncestorsAggregation,
    TFlowContext
> :
    IAggregator3DWithFlowContext<TElementFactory, TAggregationCollection, TAncestor, TAncestorsAggregation, TFlowContext>
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
    private readonly Func<TFlowContext> _initialFlowContextImplementation;
    private readonly Func<TAncestorsAggregation> _initialAncestorsAggregationImplementation;
    private readonly Func<TAncestorsAggregation, TAncestor, TAncestorsAggregation> _aggregateAncestorImplementation;
    private readonly Func<TAggregationCollection> _emptyAggregationCollectionImplementation;

    public TFlowContext InitialFlowContext => _initialFlowContextImplementation.Invoke();

    public TAncestorsAggregation InitialAncestorsAggregation => _initialAncestorsAggregationImplementation.Invoke();

    public TAncestorsAggregation AggregateAncestor(TAncestorsAggregation ancestorsAggregation, TAncestor ancestor) =>
        _aggregateAncestorImplementation(ancestorsAggregation, ancestor);

    public TAggregationCollection InitialAggregation => _emptyAggregationCollectionImplementation.Invoke();

    public TAggregationCollection Aggregate
    (
        TAncestorsAggregation ancestorsAggregation,
        TAggregationCollection siblingsAggregationCollection,
        TAggregationCollection childrenAggregationCollection,
        TElementFactory elementFactory,
        scoped ref TFlowContext flowContext
    )
    {
        int elementIndex = 0;
        int siblingsAggregationIndex = 0;
        int childrenAggregationIndex = 0;
        TAggregationCollection aggregationCollection = InitialAggregation;


        foreach (var siblingsAggregation in siblingsAggregationCollection)
        {
            foreach (var childrenAggregation in childrenAggregationCollection)
            {
                foreach (var element in GetElements(elementFactory, ref flowContext))
                { 
                    if
                    (
                        !TryAggregate
                        (
                            ancestorsAggregation, siblingsAggregationCollection, childrenAggregationCollection,
                            elementFactory, ref flowContext,

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
                            elementFactory, ref flowContext,

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

        return aggregationCollection;
    }

    private IEnumerable<TElement> GetElements
    (
        TAncestorsAggregation ancestorsAggregation,
        TAggregationCollection siblingsAggregationCollection,
        TAggregationCollection childrenAggregationCollection,
        TElementFactory elementFactory,
        scoped ref readonly TFlowContext flowContext,

        TAggregation siblingsAggregation,
        int siblingsAggregationIndex,
        TAggregation childrenAggregation,
        int childrenAggregationIndex
    )
    {
        throw new NotImplementedException();
    }

    private bool TryAggregate
    (
        TAncestorsAggregation ancestorsAggregation,
        TAggregationCollection siblingsAggregationCollection,
        TAggregationCollection childrenAggregationCollection,
        TElementFactory elementFactory,
        scoped ref TFlowContext flowContext,

        TAggregation siblingsAggregation,
        int siblingsAggregationIndex,
        TAggregation childrenAggregation,
        int childrenAggregationIndex,
        TElement element,
        int elementIndex,
        [NotNullWhen(true)] out TAggregation? aggregation
    )
    {
        throw new NotImplementedException();
    }

    private bool TryAggregateCollection
    (
        TAncestorsAggregation ancestorsAggregation,
        TAggregationCollection siblingsAggregationCollection,
        TAggregationCollection childrenAggregationCollection,
        TElementFactory elementFactory,
        scoped ref readonly TFlowContext flowContext,

        TAggregationCollection aggregationCollection,
        TAggregation aggregation,
        [NotNullWhen(true)] out TAggregationCollection? nextAggregationCollection
    )
    { 
        throw new NotImplementedException();
    }

    
}
