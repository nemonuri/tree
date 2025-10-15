namespace Nemonuri.Graphs.Infrastructure;

public interface IAggregatorContextPremise<TDescendants, TChildren, TChild, TGraph>
{
    TGraph CreateInitialGraphScopeContext();

    TDescendants CreateInitialDescendantsScopeContext(scoped ref TGraph graph);

    TDescendants CloneDescendantsScopeContext(scoped ref readonly TDescendants descendants, scoped ref TGraph graph);

    TChildren CreateChildrenScopeContext(scoped ref readonly TDescendants descendants, scoped ref TGraph graph);

    TChild CreateChildScopeContext(scoped ref readonly TDescendants descendants, scoped ref readonly TChildren children, scoped ref TGraph graph);

    AggregatorContext<TDescendants, TChildren, TChild, TGraph> CreateInnerAggregatorContext(ref TDescendants descendantsScopeContext, ref TChildren childrenScopeContext, ref TChild childScopeContext, ref TGraph graphScopeContext);

    AggregatorContext<TDescendants, TChildren, ValueNull, TGraph> CreateOuterAggregatorContext(ref TDescendants descendantsScopeContext, ref TChildren childrenScopeContext, ref TGraph graphScopeContext);

    void DeconstructOuterAggregatorContext(scoped ref AggregatorContext<TDescendants, TChildren, ValueNull, TGraph> aggregatorContext, out TDescendants descendantsScopeContext, out TChildren childrenScopeContext, out TGraph graphScopeContext);

    void DeconstructInnerAggregatorContext(scoped ref AggregatorContext<TDescendants, TChildren, TChild, TGraph> aggregatorContext, out TChildren childrenScopeContext, out TChild childScopeContext, out TGraph graphScopeContext);
}