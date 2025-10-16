namespace Nemonuri.Graphs.Infrastructure;

public static class AggregatorContextTheory
{
    public static AggregatorContext<TChildren, TDescendants, TChild, TGraph>
    CreateInnerAggregatorContext<TChildren, TDescendants, TChild, TGraph>
    (
        ref TChildren childrenScopeContext,
        ref TDescendants descendantsScopeContext,
        ref TChild childScopeContext,
        ref TGraph graphScopeContext
    )
    {
        return new(ref childrenScopeContext, ref descendantsScopeContext, ref childScopeContext, ref graphScopeContext);
    }

    public static AggregatorContext<TChildren, TDescendants, ValueNull, TGraph>
    CreateOuterAggregatorContext<TChildren, TDescendants, TGraph>
    (
        ref TChildren childrenScopeContext,
        ref TDescendants descendantsScopeContext,
        ref TGraph graphScopeContext
    )
    {
        return new(ref childrenScopeContext, ref descendantsScopeContext, ref ValueNull.ValueNullRef, ref graphScopeContext);
    }

    public static void DeconstructOuterAggregatorContext<TChildren, TDescendants, TGraph>
    (
        scoped ref AggregatorContext<TChildren, TDescendants, ValueNull, TGraph> aggregatorContext,
        out TChildren childrenScopeContext,
        out TDescendants descendantsScopeContext,
        out TGraph graphScopeContext
    )
    {
        childrenScopeContext = aggregatorContext.ChildrenScopeContext;
        descendantsScopeContext = aggregatorContext.DescendantsScopeContext;
        graphScopeContext = aggregatorContext.GraphScopeContext;
    }

    public static void DeconstructInnerAggregatorContext<TChildren, TDescendants, TChild, TGraph>
    (
        scoped ref AggregatorContext<TChildren, TDescendants, TChild, TGraph> aggregatorContext,
        out TChildren childrenScopeContext,
        out TDescendants descendantsScopeContext,
        out TChild childScopeContext,
        out TGraph graphScopeContext
    )
    {
        childrenScopeContext = aggregatorContext.ChildrenScopeContext;
        descendantsScopeContext = aggregatorContext.DescendantsScopeContext;
        childScopeContext = aggregatorContext.ChildScopeContext;
        graphScopeContext = aggregatorContext.GraphScopeContext;
    }
}