namespace Nemonuri.Trees;

public static class DynamicAggregatingTheory
{
    public static TPostAggregationCollection Aggregate
    <
        TDynamicAggragator, TChildrenProvider,
        TPreElement, TPreAggregation, TPostElement, TPostAggregation, TPostAggregationCollection
    >
    (
        TDynamicAggragator dynamicAggragator,
        TChildrenProvider childrenProvider,
        TPreElement element
    )
        where TDynamicAggragator : IDynamicAggragator<TPreElement, TPreAggregation, TPostElement, TPostAggregation, TPostAggregationCollection>
#if NET9_0_OR_GREATER
        , allows ref struct
#endif
        where TChildrenProvider : IChildrenProvider<TPreElement>
#if NET9_0_OR_GREATER
        , allows ref struct
#endif
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
        Debug.Assert(dynamicAggragator is not null);
        Debug.Assert(childrenProvider is not null);
        Debug.Assert(element is not null);

        TPreAggregation preAggregation = dynamicAggragator.PreAggregate
        (
            dynamicAggragator.InitialPostAggregationCollection,
            dynamicAggragator.DefaultPostAggregation, default,
            dynamicAggragator.InitialPreAggregation, element, default
        );

        TPostAggregationCollection postAggregationCollection = dynamicAggragator.InitialPostAggregationCollection;
        TPostAggregationCollection postAggregationCollectionFromChildren =
            AggregateChildren
            <
                TDynamicAggragator, TChildrenProvider,
                TPreElement, TPreAggregation, TPostElement, TPostAggregation, TPostAggregationCollection
            >
            (
                dynamicAggragator, childrenProvider,
                dynamicAggragator.InitialPostAggregationCollection,
                dynamicAggragator.DefaultPostAggregation,
                default,
                preAggregation,
                element,
                default
            );

        int postElementIndex = 0;

        foreach (var postElement in preAggregation)
        {
            if
            (
                !dynamicAggragator.TryAggregate
                (
                    preAggregation,
                    dynamicAggragator.DefaultPostAggregation, default,
                    dynamicAggragator.DefaultPostAggregation, default,
                    postElement, 0,
                    out var postAggregation
                )
            )
            {
                goto Continue;
            }

            if
            (
                !dynamicAggragator.TryAggregateAsCollection
                (
                    postAggregationCollection, postAggregation,
                    out var v1
                )
            )
            {
                goto Continue;
            }

            postAggregationCollection = v1;

        Continue:
            postElementIndex++;
        }

        return postAggregationCollection;

    }

    public static TPostAggregationCollection AggregateChildren
    <
        TDynamicAggragator, TChildrenProvider,
        TPreElement, TPreAggregation, TPostElement, TPostAggregation, TPostAggregationCollection
    >
    (
        TDynamicAggragator dynamicAggragator,
        TChildrenProvider childrenProvider,

        TPostAggregationCollection postAggregationCollection,
        TPostAggregation postAggregation,
        int? postAggregationIndex,
        TPreAggregation preAggregation,
        TPreElement preElement,
        int? preElementIndex
    )
        where TDynamicAggragator : IDynamicAggragator<TPreElement, TPreAggregation, TPostElement, TPostAggregation, TPostAggregationCollection>
#if NET9_0_OR_GREATER
        , allows ref struct
#endif
        where TChildrenProvider : IChildrenProvider<TPreElement>
#if NET9_0_OR_GREATER
        , allows ref struct
#endif
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

    }

    public static TPostAggregationCollection Aggregate
    <
        TDynamicAggragator, TChildrenProvider,
        TPreElement, TPreAggregation, TPostElement, TPostAggregation, TPostAggregationCollection
    >
    (
        TDynamicAggragator dynamicAggragator,
        TChildrenProvider childrenProvider,

        TPostAggregationCollection siblings,
        TPostAggregationCollection children,
        TPreElement preElement
    )
        where TDynamicAggragator : IDynamicAggragator<TPreElement, TPreAggregation, TPostElement, TPostAggregation, TPostAggregationCollection>
#if NET9_0_OR_GREATER
        , allows ref struct
#endif
        where TChildrenProvider : IChildrenProvider<TPreElement>
#if NET9_0_OR_GREATER
        , allows ref struct
#endif
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
        
    }
}