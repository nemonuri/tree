using Nemonuri.Trees.Implicit;

namespace Nemonuri.Trees.TestDatas;

public static class TestDataTheory
{
    private readonly static Dictionary<Int32TreeLabel, ImplicitBottomUpRoseTree<int>> _int32TreeMap = new()
    {
        { Int32TreeLabel.Single_0, 0 },
        { Int32TreeLabel.Height1_EvenNumbersIn2To10, (2, [6, 8, 10, 4]) },
        { Int32TreeLabel.Height1_3Pow1To3Pow4, (9, [3, 27, 81]) },
        { Int32TreeLabel.Height3_NumbersIn0To10, (0, [1, (2, [3, (4, [5]), 6]), 7, (8, [9, 10])]) }
    };
    public static IReadOnlyDictionary<Int32TreeLabel, ImplicitBottomUpRoseTree<int>> Int32TreeMap => _int32TreeMap;

    private readonly static Dictionary<Int32PredicateLabel, Func<int, bool>> _int32PredicateMap = new()
    {
        { Int32PredicateLabel.IsEven, static i => i % 2 == 0 },
        { Int32PredicateLabel.IsOdd, static i => i % 2 == 1 },
        { Int32PredicateLabel.IsZero, static i => i == 0 }
    };
    public static IReadOnlyDictionary<Int32PredicateLabel, Func<int, bool>> Int32PredicateMap => _int32PredicateMap;

    private readonly static Dictionary<Int32SelectorLabel, Func<int, object>> _int32SelectorMap = new()
    {
        { Int32SelectorLabel.AddOne, static i => i+1 },
        { Int32SelectorLabel.MultiplyTwo, static i => i*2 },
        { Int32SelectorLabel.ConvertToString, static i => i.ToString() },
        { Int32SelectorLabel.IsEven, static i => i % 2 == 0 }
    };
    public static IReadOnlyDictionary<Int32SelectorLabel, Func<int, object>> Int32SelectorMap => _int32SelectorMap;
}
