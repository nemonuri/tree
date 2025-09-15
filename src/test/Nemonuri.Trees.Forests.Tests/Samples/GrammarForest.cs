using CommunityToolkit.Diagnostics;
using Nemonuri.Trees.Abstractions;
using System.Collections.Immutable;

namespace Nemonuri.Trees.Forests.Tests.Samples;

public class GrammarForest :
    IMultiAxisTree<GrammarForest>,
    ISupportMultiAxisUnboundChildren<GrammarForest>,
    IBoundableTree<GrammarForest, GrammarForest>,
    IBinderTree<GrammarForest>,
    IRoseTree<UnionedGrammarUnit, GrammarForest>
{
    private readonly UnionedGrammarUnit _grammarUnit;
    private readonly IReadOnlyList<IEnumerable<GrammarForest>> _unboundChildrenList;
    private IReadOnlyList<IEnumerable<GrammarForest>>? _childrenListCache;
    private readonly GrammarForest? _parent;

    public GrammarForest
    (
        UnionedGrammarUnit grammarUnit,
        IReadOnlyList<IEnumerable<GrammarForest>> unboundChildrenList,
        IReadOnlyList<IEnumerable<GrammarForest>>? childrenListCache,
        GrammarForest? parent
    )
    {
        _grammarUnit = grammarUnit;
        _unboundChildrenList = unboundChildrenList;
        _childrenListCache = childrenListCache;
        _parent = parent;
    }

    public GrammarForest BindParent(GrammarForest parent)
    {
        return new(_grammarUnit, _unboundChildrenList, _childrenListCache, _parent);
    }

    public IEnumerable<GrammarForest> Children => GetChildrenFromAxis(0);

    public bool HasParent => _parent is not null;

    public GrammarForest GetParent()
    {
        Guard.IsNotNull(_parent);
        return _parent;
    }

    public UnionedGrammarUnit Value => _grammarUnit;

    public int AxisCount => GrammarForestTheory.AxisCount();

    public IEnumerable<GrammarForest> GetChildrenFromAxis(int axisIndex) =>
    (
        _childrenListCache ??=
        [
            .._unboundChildrenList.Select(cl => cl.Select(child => child.BindParent(this)))
        ]
    )[axisIndex];

    public IReadOnlyList<IEnumerable<GrammarForest>> UnboundChildrenList => _unboundChildrenList;
}

public static class GrammarMatchingTheory
{
    public static Optional<SyntaxForest> Match(GrammarForest grammarForest, string sourceText, TextSpan offSetAndLength)
    {
        var aggregation = AggregatingTheory.Aggregate
        <
            SampleAggregator4D, AdHocChildrenProvider<GrammarForest>, AdHocChildrenProvider<GrammarForest>, AxisKindAncestorConverter,
            GrammarForest, ImmutableList<SyntaxForest>, AxisKind?, AxisKind?
        >
        (
            aggregator4D: new SampleAggregator4D(sourceText, offSetAndLength),
            childrenProvider0: new AdHocChildrenProvider<GrammarForest>(static f => f.GetChildrenFromAxis(0)),
            childrenProvider1: new AdHocChildrenProvider<GrammarForest>(static f => f.GetChildrenFromAxis(1)),
            multiAxisAncestorConverter: default,
            element: grammarForest
        );

#pragma warning disable CS8509
        return aggregation switch
        {
            [] => Optional<SyntaxForest>.None,
            [var v] => Optional<SyntaxForest>.Some(v)
        };
#pragma warning restore CS8509
    }
}

public readonly struct AxisKindAncestorConverter() : IMultiAxisAncestorConverter<GrammarForest, AxisKind?>
{
    public AxisKind? ConvertToAncestor(GrammarForest element, int? axisIndex, int? elementIndex) => axisIndex is { } v ? (AxisKind)v : null;
}
