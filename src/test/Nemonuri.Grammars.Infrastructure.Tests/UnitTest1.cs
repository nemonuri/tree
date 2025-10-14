using System.Collections.Immutable;
using Nemonuri.Grammars.Infrastructure.TestDatas;
using Nemonuri.Graphs.Infrastructure;

namespace Nemonuri.Grammars.Infrastructure.Tests;

public class UnitTest1
{
    [Theory]
    [MemberData(nameof(Entry1))]
    public void Test1(Int32GrammarLabel grammarLabel)
    {
        var data = TestDataTheory.Data1;
        SampleNfaPremise<int, ScanExtraRecord, AggregationSequence, AggregationSequenceUnion> nfaPremise = TestDataTheory.Int32GrammarMap[grammarLabel];
        ValueNull context = default;
        AggregationSequence extraMutableDepthContext = new([]);
        SequenceIdealContext<int, NodeId> idealContext = new
        (
            ImmutableDictionary.Create<NodeId, int>(),
            new(data)
        );

        AggregationSequenceUnion post =
        NfaTheory.AggregateHomogeneousSuccessors
        <
            SampleNfaPremise<int, ScanExtraRecord, AggregationSequence, AggregationSequenceUnion>,
            ValueNull, ValueNull, AggregationSequence, ValueNull, ValueNull, AggregationSequenceUnion,
            NodeId, NodeIdArrow, NodeIdArrow, NodeIdOutArrowSet,
            int, IReadOnlyList<int>, SequenceLattice<int>, SequenceIdealContext<int, NodeId>, ScanExtraRecord
        >
        (
            nfaPremise, ref context, extraMutableDepthContext, idealContext, new("N0")

        );

        var sliceDisplays = post.InternalAggregationSequenceUnion.Select(a => a.GetSliceDisplay(data)).ToArray();

        Assert.True(true);
    }

    public static TheoryData<Int32GrammarLabel> Entry1 => new()
    {
        Int32GrammarLabel.Grammar1,
        Int32GrammarLabel.Grammar2,
        Int32GrammarLabel.Grammar3
    };
}
