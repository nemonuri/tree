namespace Nemonuri.Graphs.Infrastructure;

public interface IAggregatorContextPremise<TChildren, TDescendants, TChild, TGraph>
{
    TGraph CreateInitialGraphScopeContext();

    TDescendants CreateInitialDescendantsScopeContext(scoped ref TGraph graph);

    TDescendants CreateNextDescendantsScopeContext(scoped ref readonly TDescendants descendants, scoped ref readonly TChildren children, scoped ref TGraph graph);

    TChildren CreateInitialChildrenScopeContext(scoped ref readonly TDescendants descendants, scoped ref TGraph graph);

    TChild CreateInitialChildScopeContext(scoped ref readonly TChildren children, scoped ref readonly TDescendants descendants, scoped ref TGraph graph);

    void DisposeChildScopeContext(scoped ref TChild child, scoped ref TGraph graph);

    void DisposeChildrenScopeContext(scoped ref TChildren children, scoped ref TGraph graph);

    void DisposeDescendantsScopeContext(scoped ref TDescendants descendants, scoped ref TGraph graph);

    void DisposeGraphScopeContext(scoped ref TGraph graph);

    AggregatorContext<TChildren, TDescendants, TChild, TGraph> CreateInnerAggregatorContext
    (
        ref TChildren childrenScopeContext,
        ref TDescendants descendantsScopeContext,
        ref TChild childScopeContext,
        ref TGraph graphScopeContext
    );

    AggregatorContext<TChildren, TDescendants, ValueNull, TGraph> CreateOuterAggregatorContext
    (
        ref TChildren childrenScopeContext,
        ref TDescendants descendantsScopeContext,
        ref TGraph graphScopeContext
    );

    void DeconstructOuterAggregatorContext
    (
        scoped ref AggregatorContext<TChildren, TDescendants, ValueNull, TGraph> aggregatorContext,
        out TChildren childrenScopeContext,
        out TDescendants descendantsScopeContext,
        out TGraph graphScopeContext
    );

    void DeconstructInnerAggregatorContext
    (
        scoped ref AggregatorContext<TChildren, TDescendants, TChild, TGraph> aggregatorContext,
        out TChildren childrenScopeContext,
        out TDescendants descendantsScopeContext,
        out TChild childScopeContext,
        out TGraph graphScopeContext
    );
}