namespace Nemonuri.Trees.Tests.TestDatas;

public static class TestDataTheory
{
    private readonly static Dictionary<Int32RoseNodeLabel, RoseNode<int>> _int32RoseNodeMap = new()
    {
        { Int32RoseNodeLabel.Single_0, 0 },
        { Int32RoseNodeLabel.Height1_EvenNumbersIn2To10, (2, [6, 8, 10, 4]) },
        { Int32RoseNodeLabel.Height1_3Pow1To3Pow4, (9, [3, 27, 81]) },
        { Int32RoseNodeLabel.Height3_NumbersIn0To10, (0, [1, (2, [3, (4, [5]), 6]), 7, (8, [9, 10])]) }
    };
    public static IReadOnlyDictionary<Int32RoseNodeLabel, RoseNode<int>> Int32RoseNodeMap => _int32RoseNodeMap;

    private readonly static Dictionary<Int32PredicateLabel, Func<int, bool>> _int32PredicateMap = new()
    {
        { Int32PredicateLabel.IsEven, static i => i % 2 == 0 },
        { Int32PredicateLabel.IsOdd, static i => i % 2 == 1 }
    };
    public static IReadOnlyDictionary<Int32PredicateLabel, Func<int, bool>> Int32PredicateMap => _int32PredicateMap;
}
