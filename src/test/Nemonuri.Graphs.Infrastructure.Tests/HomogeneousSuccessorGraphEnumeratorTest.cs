using Nemonuri.Graphs.Infrastructure.TestDatas;
using Nemonuri.Graphs.Infrastructure.TestDatas.IntNodes;

namespace Nemonuri.Graphs.Infrastructure.Tests;

public class HomogeneousSuccessorGraphEnumeratorTest
{
    [Theory]
    [MemberData(nameof(Entry1))]
    public void SelectNodeValue_ShouldStructurallyEqual
    (
        NodeLabel nodeLabel,
        int[] expected
    )
    {
        // Arrange
        AdHocSuccessorGraph<IntNode, IntNode, IntNodeArrow, IntNodeOutArrowSet> graph = new(static n => new(n));
        IntNode node = TestDataTheory.IntNodeMap[nodeLabel];
        EnumerableHomogeneousSuccessorGraph
        <
            AdHocSuccessorGraph<IntNode, IntNode, IntNodeArrow, IntNodeOutArrowSet>,
            IntNode, IntNodeArrow, IntNodeOutArrowSet
        > enumerable = new(graph, node);

        // Act
        var actual = enumerable.Select(static n => n.Node?.Value ?? -1).ToArray();

        // Assert
        Assert.Equal(expected, actual);
    }

    public static TheoryData<NodeLabel, int[]> Entry1 => new()
    {
        { NodeLabel.Single_0, [0] },
        { NodeLabel.Height1_EvenNumbersIn2To10, [2,6,8,10,4] },
        { NodeLabel.Height1_3Pow1To3Pow4, [9,3,27,81] },
        { NodeLabel.Height3_NumbersIn0To10, [0,1,2,3,4,5,6,7,8,9,10] }
    };


    [Theory]
    [MemberData(nameof(Entry2))]
    public void SelectPositionedValue_ShouldStructurallyEqual
    (
        NodeLabel nodeLabel,
        (int Value, int Depth, int Index)[] expected
    )
    {
        // Arrange
        AdHocSuccessorGraph<IntNode, IntNode, IntNodeArrow, IntNodeOutArrowSet> graph = new(static n => new(n));
        IntNode node = TestDataTheory.IntNodeMap[nodeLabel];
        EnumerableHomogeneousSuccessorGraph
        <
            AdHocSuccessorGraph<IntNode, IntNode, IntNodeArrow, IntNodeOutArrowSet>,
            IntNode, IntNodeArrow, IntNodeOutArrowSet
        > enumerable = new(graph, node);

        // Act
        var actual = enumerable.Select(static n => (n.Node?.Value ?? -1, n.Depth, n.Index)).ToArray();

        // Assert
        Assert.Equal(expected, actual);
    }

    public static TheoryData<NodeLabel, (int Value, int Depth, int Index)[]> Entry2 => new()
    {
        { NodeLabel.Single_0, [(0, 0, 0)] },
        { NodeLabel.Height1_EvenNumbersIn2To10, [(2, 0, 0),(6, 1, 0),(8, 1, 1),(10, 1, 2),(4, 1, 3)] },
        { NodeLabel.Height1_3Pow1To3Pow4, [(9, 0, 0),(3, 1, 0),(27, 1, 1),(81, 1, 2)] },
        { NodeLabel.Height3_NumbersIn0To10, [(0, 0, 0),(1, 1, 0),(2, 1, 1),(3, 2, 0),(4, 2, 1),(5, 3, 0),(6, 2, 2),(7, 1, 2),(8, 1, 3),(9, 2, 0),(10, 2, 1)] }
    };
}
