namespace Nemonuri.Trees;

public static class AggregatingTheory4D
{
    public static TAggregation Aggregate
    <
        TAggregator4D, TChildrenProvider0, TChildrenProvider1, TMultiAxisAncestorConverter,
        TElement, TAggregation, TAncestor, TAncestorsAggregation
    >
    (
        TAggregator4D aggregator4D,
        TChildrenProvider0 childrenProvider0,
        TChildrenProvider1 childrenProvider1,
        TMultiAxisAncestorConverter multiAxisAncestorConverter,
        TElement element
    )
        where TAggregator4D : IAggregator4D<TElement, TAggregation, TAncestor, TAncestorsAggregation>
#if NET9_0_OR_GREATER
        , allows ref struct
#endif
        where TChildrenProvider0 : IChildrenProvider<TElement>
#if NET9_0_OR_GREATER
        , allows ref struct
#endif
        where TChildrenProvider1 : IChildrenProvider<TElement>
#if NET9_0_OR_GREATER
        , allows ref struct
#endif
        where TMultiAxisAncestorConverter : IMultiAxisAncestorConverter<TElement, TAncestor>
#if NET9_0_OR_GREATER
        , allows ref struct
#endif
#if NET9_0_OR_GREATER
        where TElement : allows ref struct
        where TAncestor : allows ref struct
        where TAncestorsAggregation : allows ref struct
#endif
    {
        Debug.Assert(aggregator4D is not null);
        Debug.Assert(childrenProvider0 is not null);
        Debug.Assert(childrenProvider1 is not null);
        Debug.Assert(multiAxisAncestorConverter is not null);
        Debug.Assert(element is not null);

        var ancestorsAggregation = aggregator4D.AggregateAncestor
        (
            aggregator4D.InitialAncestorsAggregation,
            multiAxisAncestorConverter.ConvertToAncestor(element, null, null)
        );
        Debug.Assert(ancestorsAggregation is not null);

        (var childrenAggregation0, var childrenAggregation1) = AggregateChildren
        <
            TAggregator4D, TChildrenProvider0, TChildrenProvider1, TMultiAxisAncestorConverter,
            TElement, TAggregation, TAncestor, TAncestorsAggregation
        >
        (
            aggregator4D, childrenProvider0, childrenProvider1, multiAxisAncestorConverter,
            ancestorsAggregation, element
        );
        Debug.Assert(childrenAggregation0 is not null);
        Debug.Assert(childrenAggregation1 is not null);

        var aggregation = aggregator4D.Aggregate
        (
            ancestorsAggregation, aggregator4D.InitialAggregation,
            childrenAggregation0, childrenAggregation1, element
        );

        Debug.Assert(aggregation is not null);

        return aggregation;
    }

    public static (TAggregation, TAggregation) AggregateChildren
    <
        TAggregator4D, TChildrenProvider0, TChildrenProvider1, TMultiAxisAncestorConverter,
        TElement, TAggregation, TAncestor, TAncestorsAggregation
    >
    (
        TAggregator4D aggregator4D,
        TChildrenProvider0 childrenProvider0,
        TChildrenProvider1 childrenProvider1,
        TMultiAxisAncestorConverter multiAxisAncestorConverter,
        TAncestorsAggregation ancestorsAggregation,
        TElement element
    )
        where TAggregator4D : IAggregator4D<TElement, TAggregation, TAncestor, TAncestorsAggregation>
#if NET9_0_OR_GREATER
        , allows ref struct
#endif
        where TChildrenProvider0 : IChildrenProvider<TElement>
#if NET9_0_OR_GREATER
        , allows ref struct
#endif
        where TChildrenProvider1 : IChildrenProvider<TElement>
#if NET9_0_OR_GREATER
        , allows ref struct
#endif
        where TMultiAxisAncestorConverter : IMultiAxisAncestorConverter<TElement, TAncestor>
#if NET9_0_OR_GREATER
        , allows ref struct
#endif
#if NET9_0_OR_GREATER
        where TElement : allows ref struct
        where TAncestor : allows ref struct
        where TAncestorsAggregation : allows ref struct
#endif
    {
        Debug.Assert(aggregator4D is not null);
        Debug.Assert(childrenProvider0 is not null);
        Debug.Assert(childrenProvider1 is not null);
        Debug.Assert(multiAxisAncestorConverter is not null);
        Debug.Assert(ancestorsAggregation is not null);
        Debug.Assert(element is not null);

        var childrenAggregation0 = aggregator4D.InitialAggregation;
        var childrenAggregation1 = aggregator4D.InitialAggregation;
        int childIndex;

        childIndex = 0;
        foreach (var child in childrenProvider0.GetChildren(element))
        {
            var ancestorsAggregationOfChild = aggregator4D.AggregateAncestor
            (
                ancestorsAggregation,
                multiAxisAncestorConverter.ConvertToAncestor(child, 0, childIndex)
            );
            Debug.Assert(ancestorsAggregationOfChild is not null);

            (var grandChildrenAggregation0, var grandChildrenAggregation1) = AggregateChildren
            <
                TAggregator4D, TChildrenProvider0, TChildrenProvider1, TMultiAxisAncestorConverter,
                TElement, TAggregation, TAncestor, TAncestorsAggregation
            >
            (
                aggregator4D, childrenProvider0, childrenProvider1, multiAxisAncestorConverter,
                ancestorsAggregationOfChild, child
            );
            Debug.Assert(grandChildrenAggregation0 is not null);
            Debug.Assert(grandChildrenAggregation1 is not null);

            childrenAggregation0 = aggregator4D.Aggregate
            (
                ancestorsAggregationOfChild, childrenAggregation0, grandChildrenAggregation0, grandChildrenAggregation1, child
            );
            Debug.Assert(childrenAggregation0 is not null);
        }

        childIndex = 0;
        foreach (var child in childrenProvider1.GetChildren(element))
        {
            var ancestorsAggregationOfChild = aggregator4D.AggregateAncestor
            (
                ancestorsAggregation,
                multiAxisAncestorConverter.ConvertToAncestor(child, 1, childIndex)
            );
            Debug.Assert(ancestorsAggregationOfChild is not null);

            (var grandChildrenAggregation0, var grandChildrenAggregation1) = AggregateChildren
            <
                TAggregator4D, TChildrenProvider0, TChildrenProvider1, TMultiAxisAncestorConverter,
                TElement, TAggregation, TAncestor, TAncestorsAggregation
            >
            (
                aggregator4D, childrenProvider0, childrenProvider1, multiAxisAncestorConverter,
                ancestorsAggregationOfChild, child
            );
            Debug.Assert(grandChildrenAggregation0 is not null);
            Debug.Assert(grandChildrenAggregation1 is not null);

            childrenAggregation1 = aggregator4D.Aggregate
            (
                ancestorsAggregationOfChild, childrenAggregation1, grandChildrenAggregation0, grandChildrenAggregation1, child
            );
            Debug.Assert(childrenAggregation1 is not null);
        }

        return (childrenAggregation0, childrenAggregation1);
    }

}