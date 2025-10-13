using System.Collections.Immutable;
using Nemonuri.Grammars.Infrastructure.TestDatas;
using Nemonuri.Graphs.Infrastructure;

namespace Nemonuri.Grammars.Infrastructure.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var data = TestDataTheory.Data1;
        SampleNfaPremise<int, ScanExtraRecord, AggregationSequence, AggregationSequenceUnion> nfaPremise = TestDataTheory.CreateGrammar1();
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

        Assert.True(true);
    }
}
