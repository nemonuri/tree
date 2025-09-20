namespace Nemonuri.Trees;

public static partial class AggregatingTheory
{
    public static TAggregation Aggregate
    <
        TAggregator, TChildrenProvider, TAncestorConverter,
        TElement, TAggregation, TAncestor, TAncestorsAggregation, TFlowContext
    >
    (
        TAggregator aggregator,
        TChildrenProvider childrenProvider,
        TAncestorConverter ancestorConverter,
        TElement element
    )
        where TAggregator :
            IAggregator3DWithFlowContext<TElement, TAggregation, TAncestor, TAncestorsAggregation, TFlowContext>
#if NET9_0_OR_GREATER
            , allows ref struct
#endif
        where TChildrenProvider : IChildrenProvider<TElement>
#if NET9_0_OR_GREATER
        , allows ref struct
#endif
        where TAncestorConverter : IAncestorConverter<TElement, TAncestor>
#if NET9_0_OR_GREATER
        , allows ref struct
#endif
#if NET9_0_OR_GREATER
        where TElement : allows ref struct
        where TAggregation : allows ref struct
        where TAncestor : allows ref struct
        where TAncestorsAggregation : allows ref struct
        where TFlowContext : allows ref struct
#endif
    {
        Debug.Assert(aggregator is not null);
        Debug.Assert(childrenProvider is not null);
        Debug.Assert(ancestorConverter is not null);
        Debug.Assert(element is not null);

        TFlowContext flowContext = aggregator.InitialFlowContext;

        TAncestorsAggregation ancestorsAggregation = aggregator.AggregateAncestor
        (
            aggregator.InitialAncestorsAggregation,
            ancestorConverter.ConvertToAncestor(element, null)
        );
        Debug.Assert(ancestorsAggregation is not null);

        TAggregation childrenAggregation = AggregateChildren
        <
            TAggregator, TChildrenProvider, TAncestorConverter,
            TElement, TAggregation, TAncestor, TAncestorsAggregation, TFlowContext
        >
        (
            aggregator, childrenProvider, ancestorConverter,
            ancestorsAggregation, element, ref flowContext
        );
        Debug.Assert(childrenAggregation is not null);

        TAggregation aggregation = aggregator.Aggregate
        (
            ancestorsAggregation, aggregator.InitialAggregation,
            childrenAggregation, element, ref flowContext
        );
        Debug.Assert(aggregation is not null);

        return aggregation;
    }

    public static TAggregation AggregateChildren
    <
        TAggregator, TChildrenProvider, TAncestorConverter,
        TElement, TAggregation, TAncestor, TAncestorsAggregation, TFlowContext
    >
    (
        TAggregator aggregator,
        TChildrenProvider childrenProvider,
        TAncestorConverter ancestorConverter,
        TAncestorsAggregation ancestorsAggregation,
        TElement element,
        scoped ref TFlowContext flowContext
    )
        where TAggregator :
            IAggregator3DWithFlowContext<TElement, TAggregation, TAncestor, TAncestorsAggregation, TFlowContext>
#if NET9_0_OR_GREATER
            , allows ref struct
#endif
        where TChildrenProvider : IChildrenProvider<TElement>
#if NET9_0_OR_GREATER
        , allows ref struct
#endif
        where TAncestorConverter : IAncestorConverter<TElement, TAncestor>
#if NET9_0_OR_GREATER
        , allows ref struct
#endif
#if NET9_0_OR_GREATER
        where TElement : allows ref struct
        where TAggregation : allows ref struct
        where TAncestor : allows ref struct
        where TAncestorsAggregation : allows ref struct
        where TFlowContext : allows ref struct
#endif
    {
        Debug.Assert(aggregator is not null);
        Debug.Assert(childrenProvider is not null);
        Debug.Assert(ancestorConverter is not null);
        Debug.Assert(ancestorsAggregation is not null);
        Debug.Assert(element is not null);

        var childrenAggregation = aggregator.InitialAggregation;

        int childIndex = 0;
        foreach (var child in childrenProvider.GetChildren(element))
        {
            var ancestorsAggregationOfChild = aggregator.AggregateAncestor
            (
                ancestorsAggregation,
                ancestorConverter.ConvertToAncestor(child, childIndex)
            );
            Debug.Assert(ancestorsAggregationOfChild is not null);

            var grandChildrenAggregation = AggregateChildren
            <
                TAggregator, TChildrenProvider, TAncestorConverter,
                TElement, TAggregation, TAncestor, TAncestorsAggregation, TFlowContext
            >
            (
                aggregator, childrenProvider, ancestorConverter,
                ancestorsAggregationOfChild, child, ref flowContext
            );
            Debug.Assert(grandChildrenAggregation is not null);

            childrenAggregation = aggregator.Aggregate
            (
                ancestorsAggregationOfChild, childrenAggregation, grandChildrenAggregation, child,
                ref flowContext
            );
            Debug.Assert(childrenAggregation is not null);

            childIndex++;
        }

        return childrenAggregation;
    }
}
