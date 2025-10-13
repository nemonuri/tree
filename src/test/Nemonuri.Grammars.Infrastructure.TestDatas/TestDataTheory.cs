using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using Nemonuri.Graphs.Infrastructure;

namespace Nemonuri.Grammars.Infrastructure.TestDatas;

public static class TestDataTheory
{
    public readonly static IReadOnlyList<int> Data1 = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];

    public static SampleNfaPremise<int, ScanExtraRecord, AggregationSequence, AggregationSequenceUnion>
    CreateGrammar1()
    {
        //    (v0{v0 <= 5})*    (v1{v1 <= 10})*    v2{v2 == 10}
        // N0 ----{ A0 }---> N1 -----{ A2 }---> N2 ---{ A4 }--> N3
        //    <---{ A1 }----    <----{ A3 }----

        Dictionary<NodeArrowId, NodeArrowIdToNodeIdMapItem> nodeMap = new()
        {
            {new("A0"), new(new("A0"), new("N0"), new("N1")) },
            {new("A1"), new(new("A1"), new("N1"), new("N0")) },

            {new("A2"), new(new("A2"), new("N1"), new("N2")) },
            {new("A3"), new(new("A3"), new("N2"), new("N1")) },

            {new("A4"), new(new("A4"), new("N2"), new("N3")) }
        };

        Dictionary<NodeArrowId, NodeArrowIdToScanPremiseMapItem<int, ScanExtraRecord>> scanMap = new()
        {
            { new("A0"), new(new("A0"), new ScanPremise([static v0 => v0 <= 5])) },
            { new("A1"), new(new("A1"), new ScanPremise([])) },

            { new("A2"), new(new("A2"), new ScanPremise([static v1 => v1 <= 10])) },
            { new("A3"), new(new("A3"), new ScanPremise([])) },

            { new("A4"), new(new("A4"), new ScanPremise([static v2 => v2 == 10])) },
        };

        SampleNfaPremise<int, ScanExtraRecord, AggregationSequence, AggregationSequenceUnion> result = new
        (
            nodeMap: nodeMap,
            scanMap: scanMap,
            sequenceAggregator: new SequenceAggregator1<int>(),
            sequenceUnionAggregator: new SequenceUnionAggregator1<int>(),
            sequenceCloner: static a => a,
            sequenceToSequenceUnionCaster: static (canon, seq) => new(IsFullConvered(canon, seq) ? [seq] : [])
        );

        return result;

        static bool IsFullConvered(IReadOnlyList<int> canon, AggregationSequence seq)
        {
            if (!(seq.InternalAggregationSequence.Count > 0)) { return false; }
            if (!(seq.InternalAggregationSequence[0].Offset == 0)) { return false; }
            if (!(seq.InternalAggregationSequence.Sum(static r => r.Length) == canon.Count)) { return false; }

            return true;
        }
    }

    private class ScanPremise : IScanPremise<int, ScanExtraRecord>
    {
        private readonly Func<int, bool>[] _predicates;

        public ScanPremise(Func<int, bool>[] predicates)
        {
            _predicates = predicates;
        }

        public ScanResult<int, ScanExtraRecord> Scan(NodeArrowId arrow, SequenceLattice<int> ideal)
        {
            for (int i = 0; i < _predicates.Length; i++)
            {
                int index = ideal.LeastUpperBound + i;

                if (!(index < ideal.GreatestLowerBound)) { goto Fail; }
                if (!_predicates[i](ideal.Canon[index])) { goto Fail; }
            }

            int nextLowerBound = ideal.LeastUpperBound + _predicates.Length;
            return new(ScanResultStatus.ScanSuccess, nextLowerBound, new(arrow, nextLowerBound));

        Fail:
            return new(ScanResultStatus.ScanFail, default, default);
        }
    }
    
}

public readonly record struct ScanExtraRecord(NodeArrowId NodeArrowId, int GreatestLowerBound);

public class SequenceAggregator1<T> :
    IInitialSourceGivenAggregator<AggregationSequence, SequenceLatticeSnapshot<T, ScanExtraRecord>>
{
    public SequenceAggregator1() { }

    public AggregationSequence InitialSource => new([]);

    public AggregationSequence Aggregate(AggregationSequence source, SequenceLatticeSnapshot<T, ScanExtraRecord> value)
    {
        //--- convert ---
        AggregationUnit v = new(value.Extra.NodeArrowId, value.Sequence.LeastUpperBound, value.Extra.GreatestLowerBound - value.Sequence.LeastUpperBound);
        //---|

        return source.Add(v);
    }
}

public class SequenceUnionAggregator1<T> :
    IInitialSourceGivenAggregator<AggregationSequenceUnion, AggregationSequenceUnion>
{
    public SequenceUnionAggregator1() { }

    public AggregationSequenceUnion InitialSource => new([]);

    public AggregationSequenceUnion Aggregate(AggregationSequenceUnion source, AggregationSequenceUnion value)
    {
        return source.Union(value);
    }
}

public class SequenceEqualityComparer1<T> : IEqualityComparer<ImmutableList<AggregationSequence>>
{
    public static readonly SequenceEqualityComparer1<T> Instance = new();

    public SequenceEqualityComparer1() { }

    public bool Equals(ImmutableList<AggregationSequence>? x, ImmutableList<AggregationSequence>? y)
    {
        if (x is null || y is null) { return false; }

        return x.SequenceEqual(y);
    }

    public int GetHashCode([DisallowNull] ImmutableList<AggregationSequence> obj)
    {
        return obj.Aggregate(new HashCode(), static (a, e) => { a.Add(e); return a; }, static h => h.ToHashCode());
    }
}


