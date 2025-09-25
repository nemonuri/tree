using System.Diagnostics.CodeAnalysis;
using Nemonuri.Trees.Abstractions;

namespace Nemonuri.Trees.Forests.Tests.Samples3;

public class GrammarForest
{
    public ISyntaxForestGrammar Grammar { get; }
}

#if false
public static class ParserTheory
{
    public static SyntaxForest Consume(GrammarForest grammarForest, ConsumableSource source)
    {
        DynamicAggregatingTheory.Aggregate()
    }
}
#endif

public class GrammarForestNavigator : IDynamicNavigator<GrammarForest, NullValue, SyntaxForestBuilder>
{
    public bool TryGetFirstChildOfNextChildren(GrammarForest node, NullValue flowAggregation, SyntaxForestBuilder childrenUnion, [NotNullWhen(true)] out GrammarForest? firstChildNode)
    {
        throw new NotImplementedException();
    }

    public bool TryGetNextSibling(GrammarForest node, NullValue flowAggregation, SyntaxForestBuilder siblingSequence, [NotNullWhen(true)] out GrammarForest? nextSiblingNode)
    {
        throw new NotImplementedException();
    }
}

#if false
public class SyntaxForestAggregator : IAggregator4D<GrammarForest, SyntaxForestBuilder, NullValue, NullValue>
{
    public SyntaxForestAggregator(ConsumableSourceInvarient consumableSourceInvarient)
    {
        ConsumableSourceInvarient = consumableSourceInvarient;
    }

    public ConsumableSourceInvarient ConsumableSourceInvarient { get; }

    public NullValue InitialFlowAggregation => default;

    public NullValue AggregateFlow(NullValue flowAggregation, NullValue flow) => default;

    public SyntaxForestBuilder InitialAggregation => SyntaxForestBuilder.Empty;

    public SyntaxForestBuilder AggregateElement
    (
        NullValue flowAggregation,
        SyntaxForestBuilder siblingSequenceUnion,
        SyntaxForestBuilder siblingSequence,
        SyntaxForestBuilder childMatrix,
        GrammarForest grammarForest
    )
    {
        if
        (
            !(siblingSequenceUnion.TryGetSequenceUnion(out var ensuredSiblingSequenceUnion) &&
            siblingSequence.TryGetSequence(out var ensuredSiblingSequence) &&
            childMatrix.TryGetSequenceUnion(out var ensuredChildMatrix))
        )
        {
            return SyntaxForestBuilder.Poison;
        }

        var producedChildrenUnion =
        ensuredSiblingSequence.ConsumableSourceVarientSet
            .Select(v => new ConsumableSource(ConsumableSourceInvarient, v))
            .Select(cs => (Success: grammarForest.Grammar.TryProduce(cs, out var product), Product: product))
            .Where(tuple => tuple.Success == true && tuple.Product is not null)
            .Select(tuple => tuple.Product)
            .Aggregate
            (
                Enumerable.Empty<SyntaxForestSequence>(),
                static (a, e) => a.Concat(e!),
                static a => new SyntaxForestSequenceUnion([.. a])
            );

        var newChildMatrixBuilder = InitialAggregation;

        foreach (var children in producedChildrenUnion)
        {
            if (grammarForest.Grammar.IsMatch(children))
            {
                newChildMatrixBuilder = AggregateSequence(newChildMatrixBuilder, children);
            }
        }
        foreach (var children in ensuredChildMatrix)
        {
            if (grammarForest.Grammar.IsMatch(children))
            {
                newChildMatrixBuilder = AggregateSequence(newChildMatrixBuilder, children);
            }
        }

        if (!newChildMatrixBuilder.TryGetSequenceUnion(out var newChildMatrix))
        {
            return SyntaxForestBuilder.Poison;
        }

        SyntaxForest resultSyntaxForest = new SyntaxForest(grammarForest.Grammar, newChildMatrix);
    }

    public SyntaxForestBuilder AggregateSequence(SyntaxForestBuilder sequenceUnion, SyntaxForestBuilder sequence)
    {
        throw new NotImplementedException();
    }
}
#endif

public class AAAA<T> : IFlowConverter<GrammarForest, T>
{
    public T ConvertToFlow(GrammarForest node)
    {
        throw new NotImplementedException();
    }

    public T ConvertToFlow(GrammarForest parent, GrammarForest node, int ordinalInSequenceUnion, int ordinalInSequence)
    {
        throw new NotImplementedException();
    }
}
