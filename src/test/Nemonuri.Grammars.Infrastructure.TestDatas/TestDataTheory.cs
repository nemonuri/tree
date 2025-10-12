using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using Nemonuri.Graphs.Infrastructure;

namespace Nemonuri.Grammars.Infrastructure.TestDatas;

public static class TestDataTheory
{
    public readonly static IReadOnlyList<int> Data1 = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];

    public static SampleNfaPremise<int, ScanExtraRecord, ImmutableList<AggregationSequenceRecord<int>>, ImmutableHashSet<ImmutableList<AggregationSequenceRecord<int>>>>
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

        SampleNfaPremise<int, ScanExtraRecord, ImmutableList<AggregationSequenceRecord<int>>, ImmutableHashSet<ImmutableList<AggregationSequenceRecord<int>>>> result = new
        (
            nodeMap: nodeMap,
            scanMap: scanMap,
            sequenceAggregator: new SequenceAggregator1<int>(),
            sequenceUnionAggregator: new SequenceUnionAggregator1<int>(),
            sequenceCloner: static a => a,
            sequenceToSequenceUnionCaster: static seq => ImmutableHashSet.Create
            (
                SequenceEqualityComparer1<int>.Instance,
                IsFullConvered(seq) ? [seq] : []
            )
        );

        return result;

        static bool IsFullConvered(ImmutableList<AggregationSequenceRecord<int>> list)
        {
            if (!(list.Count > 0)) { return false; }
            if (!(list[0].Offset == 0)) { return false; }
            if (!(list.Sum(static r => r.Length) == list[0].Canon.Count)) { return false; }

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

public readonly record struct AggregationSequenceRecord<T>
(
    IReadOnlyList<T> Canon, NodeArrowId NodeArrowId, int Offset, int Length
); //ImmutableArray<T> Segment

public class SequenceAggregator1<T> :
    IInitialSourceGivenAggregator<ImmutableList<AggregationSequenceRecord<T>>, SequenceLatticeSnapshot<T, ScanExtraRecord>>
{
    public SequenceAggregator1() { }

    public ImmutableList<AggregationSequenceRecord<T>> InitialSource => [];

    public ImmutableList<AggregationSequenceRecord<T>> Aggregate(ImmutableList<AggregationSequenceRecord<T>> source, SequenceLatticeSnapshot<T, ScanExtraRecord> value)
    {
        //--- convert ---
        AggregationSequenceRecord<T> v = new(value.Sequence.Canon, value.Extra.NodeArrowId, value.Sequence.LeastUpperBound, value.Extra.GreatestLowerBound - value.Sequence.LeastUpperBound);
        //---|

        return source.Add(v);
    }
}

public class SequenceEqualityComparer1<T> : IEqualityComparer<ImmutableList<AggregationSequenceRecord<T>>>
{
    public static readonly SequenceEqualityComparer1<T> Instance = new();

    public SequenceEqualityComparer1() { }

    public bool Equals(ImmutableList<AggregationSequenceRecord<T>>? x, ImmutableList<AggregationSequenceRecord<T>>? y)
    {
        if (x is null || y is null) { return false; }

        return x.SequenceEqual(y);
    }

    public int GetHashCode([DisallowNull] ImmutableList<AggregationSequenceRecord<T>> obj)
    {
        return obj.Aggregate(new HashCode(), static (a, e) => { a.Add(e); return a; }, static h => h.ToHashCode());
    }
}

public class SequenceUnionAggregator1<T> :
    IInitialSourceGivenAggregator<ImmutableHashSet<ImmutableList<AggregationSequenceRecord<T>>>, ImmutableHashSet<ImmutableList<AggregationSequenceRecord<T>>>>
{
    public SequenceUnionAggregator1() { }

    public ImmutableHashSet<ImmutableList<AggregationSequenceRecord<T>>> InitialSource => [];

    public ImmutableHashSet<ImmutableList<AggregationSequenceRecord<T>>> Aggregate
    (
        ImmutableHashSet<ImmutableList<AggregationSequenceRecord<T>>> source, ImmutableHashSet<ImmutableList<AggregationSequenceRecord<T>>> value
    )
    {
        return source.Union(value);
    }
}

