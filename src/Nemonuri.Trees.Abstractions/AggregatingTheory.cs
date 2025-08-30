namespace Nemonuri.Trees;

/// <summary>
/// General theory of aggregating tree structured objects.
/// </summary>
public static class AggregatingTheory
{
    /// <inheritdoc cref="Aggregate{_,_,_,_,_,_,_}" />
    public static TAggregation Aggregate
    <TElement, TAggregation, TAncestor, TAncestorsAggregation>
    (
        IAggregator3D<TElement, TAggregation, TAncestor, TAncestorsAggregation> aggregator3D,
        IChildrenProvider<TElement> childrenProvider,
        IAncestorConverter<TElement, TAggregation, TAncestor> ancestorConverter,
        TElement element
    )
#if NET9_0_OR_GREATER
        where TElement : allows ref struct
        where TAggregation : allows ref struct
        where TAncestor : allows ref struct
        where TAncestorsAggregation : allows ref struct
#endif
    { 
        return Aggregate
        <
            IAggregator3D<TElement, TAggregation, TAncestor, TAncestorsAggregation>,
            IChildrenProvider<TElement>,
            IAncestorConverter<TElement, TAggregation, TAncestor>,

            TElement, TAggregation, TAncestor, TAncestorsAggregation
        >
        (
            aggregator3D,
            childrenProvider,
            ancestorConverter,
            element
        );
    }

    /// <summary>
    /// Aggregate the specified object as tree root.
    /// </summary>
    /// <inheritdoc cref="AggregateChildren{_,_,_,_,_,_,_}" 
    ///     path="//*[(self::remarks) or (self::typeparam) or (self::param)]"/>
    /// <returns>The aggregated value.</returns>
    public static TAggregation Aggregate
    <
        TAggregator3D, TChildrenProvider, TAncestorConverter,
        TElement, TAggregation, TAncestor, TAncestorsAggregation
    >
    (
        TAggregator3D aggregator3D,
        TChildrenProvider childrenProvider,
        TAncestorConverter ancestorConverter,
        TElement element
    )
        where TAggregator3D : IAggregator3D<TElement, TAggregation, TAncestor, TAncestorsAggregation>
#if NET9_0_OR_GREATER
        , allows ref struct
#endif
        where TChildrenProvider : IChildrenProvider<TElement>
#if NET9_0_OR_GREATER
        , allows ref struct
#endif
        where TAncestorConverter : IAncestorConverter<TElement, TAggregation, TAncestor>
#if NET9_0_OR_GREATER
        , allows ref struct
#endif
#if NET9_0_OR_GREATER
        where TElement : allows ref struct
        where TAggregation : allows ref struct
        where TAncestor : allows ref struct
        where TAncestorsAggregation : allows ref struct
#endif
    {
        Debug.Assert(aggregator3D is not null);
        Debug.Assert(childrenProvider is not null);
        Debug.Assert(ancestorConverter is not null);
        Debug.Assert(element is not null);

        var ancestorsAggregation = aggregator3D.AggregateAncestor
        (
            aggregator3D.InitialAncestorsAggregation,
            ancestorConverter.ConvertToAncestor(element, null)
        );
        Debug.Assert(ancestorsAggregation is not null);

        var childrenAggregation = AggregateChildren
        <
            TAggregator3D, TChildrenProvider, TAncestorConverter,
            TElement, TAggregation, TAncestor, TAncestorsAggregation
        >
        (
            aggregator3D, childrenProvider, ancestorConverter,
            ancestorsAggregation, element
        );
        Debug.Assert(childrenAggregation is not null);

        var aggregation = aggregator3D.Aggregate
        (
            ancestorsAggregation, aggregator3D.InitialAggregation,
            childrenAggregation, element
        );
        Debug.Assert(aggregation is not null);

        return aggregation;
    }


    /// <inheritdoc cref="AggregateChildren{_,_,_,_,_,_,_}" />
    public static TAggregation AggregateChildren
    <TElement, TAggregation, TAncestor, TAncestorsAggregation>
    (
        IAggregator3D<TElement, TAggregation, TAncestor, TAncestorsAggregation> aggregator3D,
        IChildrenProvider<TElement> childrenProvider,
        IAncestorConverter<TElement, TAggregation, TAncestor> ancestorConverter,
        TAncestorsAggregation ancestorsAggregation,
        TElement element
    )
#if NET9_0_OR_GREATER
        where TElement : allows ref struct
        where TAggregation : allows ref struct
        where TAncestor : allows ref struct
        where TAncestorsAggregation : allows ref struct
#endif
    {
        return AggregateChildren
        <
            IAggregator3D<TElement, TAggregation, TAncestor, TAncestorsAggregation>,
            IChildrenProvider<TElement>,
            IAncestorConverter<TElement, TAggregation, TAncestor>,

            TElement, TAggregation, TAncestor, TAncestorsAggregation
        >
        (
            aggregator3D,
            childrenProvider,
            ancestorConverter,
            ancestorsAggregation,
            element
        );
    }

    /// <summary>
    /// Aggregate children of the specified object.
    /// </summary>
    /// <remarks>Operation order is depth-first.</remarks>
    /// <typeparam name="TAggregator3D">
    /// The concrete type of <see cref="IAggregator3D{_,_,_,_}"/>
    /// </typeparam>
    /// <typeparam name="TChildrenProvider">
    /// The concrete type of <see cref="IChildrenProvider{_}"/>
    /// </typeparam>
    /// <typeparam name="TAncestorConverter">
    /// The concrete type of <see cref="IAncestorConverter{_,_,_}"/>
    /// </typeparam>
    /// <inheritdoc cref="IAggregator3D{_,_,_,_}" path="/typeparam"/>
    /// <param name="aggregator3D"></param>
    /// <param name="childrenProvider"></param>
    /// <param name="ancestorConverter"></param>
    /// <param name="ancestorsAggregation"></param>
    /// <param name="element"></param>
    /// <returns>The aggregated value from children of <paramref name="element"/></returns>
    public static TAggregation AggregateChildren
    <
        TAggregator3D, TChildrenProvider, TAncestorConverter,
        TElement, TAggregation, TAncestor, TAncestorsAggregation
    >
    (
        TAggregator3D aggregator3D,
        TChildrenProvider childrenProvider,
        TAncestorConverter ancestorConverter,
        TAncestorsAggregation ancestorsAggregation,
        TElement element
    )
        where TAggregator3D : IAggregator3D<TElement, TAggregation, TAncestor, TAncestorsAggregation>
#if NET9_0_OR_GREATER
        , allows ref struct
#endif
        where TChildrenProvider : IChildrenProvider<TElement>
#if NET9_0_OR_GREATER
        , allows ref struct
#endif
        where TAncestorConverter : IAncestorConverter<TElement, TAggregation, TAncestor>
#if NET9_0_OR_GREATER
        , allows ref struct
#endif
#if NET9_0_OR_GREATER
        where TElement : allows ref struct
        where TAggregation : allows ref struct
        where TAncestor : allows ref struct
        where TAncestorsAggregation : allows ref struct
#endif
    {
        Debug.Assert(aggregator3D is not null);
        Debug.Assert(childrenProvider is not null);
        Debug.Assert(ancestorConverter is not null);
        Debug.Assert(ancestorsAggregation is not null);
        Debug.Assert(element is not null);

        var childrenAggregation = aggregator3D.InitialAggregation;

        int childIndex = 0;
        foreach (var child in childrenProvider.GetChildren(element))
        {
            var ancestorsAggregationOfChild = aggregator3D.AggregateAncestor
            (
                ancestorsAggregation,
                ancestorConverter.ConvertToAncestor(child, childIndex)
            );
            Debug.Assert(ancestorsAggregationOfChild is not null);

            var grandChildrenAggregation = AggregateChildren
            <
                TAggregator3D, TChildrenProvider, TAncestorConverter,
                TElement, TAggregation, TAncestor, TAncestorsAggregation
            >
            (
                aggregator3D, childrenProvider, ancestorConverter,
                ancestorsAggregationOfChild, child
            );
            Debug.Assert(grandChildrenAggregation is not null);

            childrenAggregation = aggregator3D.Aggregate
            (
                ancestorsAggregationOfChild, childrenAggregation, grandChildrenAggregation, child
            );
            Debug.Assert(childrenAggregation is not null);

            childIndex++;
        }

        return childrenAggregation;
    }
}