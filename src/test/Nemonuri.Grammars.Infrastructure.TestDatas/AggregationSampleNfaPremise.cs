using Nemonuri.Graphs.Infrastructure;

namespace Nemonuri.Grammars.Infrastructure.TestDatas;

public class AggregationSampleNfaPremise<T> : SampleNfaPremise<T, ScanExtraRecord, AggregationSequence, AggregationSequenceUnion>
{
    public AggregationSampleNfaPremise
    (
        IReadOnlyDictionary<NodeArrowId, NodeArrowIdToNodeIdMapItem> nodeMap,
        IReadOnlyDictionary<NodeArrowId, NodeArrowIdToScanPremiseMapItem<T, ScanExtraRecord>> scanMap,
        IInitialSourceGivenAggregator<AggregationSequence, SequenceLatticeSnapshot<T, ScanExtraRecord>> sequenceAggregator,
        IInitialSourceGivenAggregator<AggregationSequenceUnion, AggregationSequenceUnion> sequenceUnionAggregator,
        Func<AggregationSequence, AggregationSequence> sequenceCloner,
        Func<IReadOnlyList<T>, AggregationSequence, AggregationSequenceUnion> sequenceToSequenceUnionCaster
    ) : 
        base(nodeMap, scanMap, sequenceAggregator, sequenceUnionAggregator, sequenceCloner, sequenceToSequenceUnionCaster)
    {
    }
}


