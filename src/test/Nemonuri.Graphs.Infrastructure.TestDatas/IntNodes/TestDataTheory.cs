namespace Nemonuri.Graphs.Infrastructure.TestDatas.IntNodes;

public static class TestDataTheory
{
    private readonly static Dictionary<NodeLabel, IntNode> _intNodeMap = new()
    {
        { NodeLabel.Single_0, 0 },
        { NodeLabel.Height1_EvenNumbersIn2To10, (2, [6, 8, 10, 4]) },
        { NodeLabel.Height1_3Pow1To3Pow4, (9, [3, 27, 81]) },
        { NodeLabel.Height3_NumbersIn0To10, (0, [1, (2, [3, (4, [5]), 6]), 7, (8, [9, 10])]) }
    };
    public static IReadOnlyDictionary<NodeLabel, IntNode> IntNodeMap => _intNodeMap;
}
