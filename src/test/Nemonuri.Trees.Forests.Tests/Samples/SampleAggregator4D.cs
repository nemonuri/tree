using System.Collections.Immutable;

namespace Nemonuri.Trees.Forests.Tests.Samples;

public class SampleAggregator4D :
    IAggregator4D<GrammarForest, ImmutableList<SyntaxForest>, AxisKind?, AxisKind?>
{
    private readonly string _sourceText;
    private OffSetAndLength _currentOffSetAndLength;

    public SampleAggregator4D(string sourceText, OffSetAndLength offSetAndLength)
    {
        _sourceText = sourceText;
        _currentOffSetAndLength = offSetAndLength;
    }

    public AxisKind? InitialAncestorsAggregation => default;

    public AxisKind? AggregateAncestor(AxisKind? ancestorsAggregation, AxisKind? ancestor) => ancestor;

    public ImmutableList<SyntaxForest> InitialAggregation => [];

    public ImmutableList<SyntaxForest> Aggregate
    (
        AxisKind? ancestorsAggregation,
        ImmutableList<SyntaxForest> siblingsAggregation,
        ImmutableList<SyntaxForest> childrenFromAxis0Aggregation,
        ImmutableList<SyntaxForest> childrenFromAxis1Aggregation,
        GrammarForest element
    )
    {
        if (ancestorsAggregation is AxisKind.Possibility && childrenFromAxis1Aggregation.Count > 10)
        {
            return siblingsAggregation;
        }

        SyntaxForestValue? syntaxForestValue = childrenFromAxis0Aggregation switch
        {
            [] => element.Value switch
            {
                { IsStringGrammar: true } v =>
                    v.AsStringGrammar.Match(_sourceText, _currentOffSetAndLength) is var i && i >= 0 ?
                        new SyntaxForestValue(element, UnionedMatchInfo.String(new(new(_sourceText, _currentOffSetAndLength), i))) : null,
                _ => null
            },
            _ => element.Value switch
            {
                { IsSyntaxTreeListGrammar: true } v =>
                    v.AsSyntaxTreeListGrammar.Match(childrenFromAxis0Aggregation) ?
                        new SyntaxForestValue(element, UnionedMatchInfo.SyntaxTreeList(new(childrenFromAxis0Aggregation))) : null,
                _ => null
            }
        };

        SyntaxForest syntaxForest;
        if (syntaxForestValue is null)
        {
            if (childrenFromAxis1Aggregation is []) { return siblingsAggregation; }

            syntaxForest = new SyntaxForest
            (
                childrenFromAxis1Aggregation[0].Value,
                [childrenFromAxis1Aggregation[0].UnboundChildrenList[0], childrenFromAxis1Aggregation.RemoveAt(0)],
                null, null
            );
        }
        else
        {
            syntaxForest = new SyntaxForest
            (
                syntaxForestValue,
                [childrenFromAxis0Aggregation, childrenFromAxis1Aggregation],
                null, null
            );

            if (syntaxForestValue.MatchInfo.IsString)
            {
                StringGrammarUnitMatchInfo v = syntaxForestValue.MatchInfo.AsString;
                _currentOffSetAndLength = new OffSetAndLength(_currentOffSetAndLength.Offset + v.Result, _currentOffSetAndLength.Length - v.Result);
            }
        }

        return siblingsAggregation.Add(syntaxForest);
    }
}
