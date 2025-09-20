using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using CommunityToolkit.Diagnostics;
using Nemonuri.Trees.Abstractions;

namespace Nemonuri.Trees.Forests.Tests.Samples2;

public class GrammarAggregatorBase : IForestAggregatorBase
<
    GrammarTree, ImmutableList<SyntaxTree>, GrammarFactory, SyntaxForest, GrammarComplex,
    NullValue, NullValue, GrammarAggregatorFlow
>
{
    public GrammarAggregatorBase(string sourceText)
    {
        Guard.IsNotNull(sourceText);
        InitialFlowContext = new(sourceText, new TextSpan(0, sourceText.Length));
    }

    public GrammarAggregatorFlow InitialFlowContext { get; }

    public NullValue InitialAncestorsAggregation => default;

    public NullValue AggregateAncestor(NullValue ancestorsAggregation, NullValue ancestor)
    {
        return default;
    }

    public SyntaxForest EmptyAggregationCollection { get; } = new([]);

    public SyntaxForest InitialAggregationCollection { get; } = new([[]]);

    public IEnumerable<GrammarFactory> GetElementFactories
    (
        NullValue ancestorsAggregation,
        GrammarComplex elementComplex
    )
    {
        throw new NotImplementedException();
    }

    public IEnumerable<GrammarTree> GetElements
    (
        NullValue ancestorsAggregation,
        SyntaxForest siblingsAggregationCollection,
        SyntaxForest childrenAggregationCollection,
        GrammarComplex elementComplex,
        scoped ref readonly GrammarAggregatorFlow flowContext,

        GrammarFactory elementFactory,
        int elementFactoryIndex,
        ImmutableList<SyntaxTree> siblingsAggregation,
        int siblingsAggregationIndex,
        ImmutableList<SyntaxTree> childrenAggregation,
        int childrenAggregationIndex
    )
    {
        if (elementFactory is SumGrammarFactory gf1)
        {
            return gf1.GrammarTrees;
        }
        else
        {
            throw new SwitchExpressionException(elementFactory);
        }
    }

    public bool TryAggregate
    (
        NullValue ancestorsAggregation,
        SyntaxForest siblingsAggregationCollection,
        SyntaxForest childrenAggregationCollection,
        GrammarComplex elementComplex,
        scoped ref GrammarAggregatorFlow flowContext,

        GrammarFactory elementFactory,
        int elementFactoryIndex,
        ImmutableList<SyntaxTree> siblingsAggregation,
        int siblingsAggregationIndex,
        ImmutableList<SyntaxTree> childrenAggregation,
        int childrenAggregationIndex,
        GrammarTree element,
        int elementIndex,
        [NotNullWhen(true)] out ImmutableList<SyntaxTree>? aggregation
    )
    {
        throw new NotImplementedException();
    }

    public bool TryAggregateCollection
    (
        NullValue ancestorsAggregation,
        SyntaxForest siblingsAggregationCollection,
        SyntaxForest childrenAggregationCollection,
        GrammarComplex elementComplex,
        scoped ref readonly GrammarAggregatorFlow flowContext,

        SyntaxForest aggregationCollection,
        ImmutableList<SyntaxTree> aggregation,
        [NotNullWhen(true)] out SyntaxForest? nextAggregationCollection
    )
    {
        throw new NotImplementedException();
    }
}



