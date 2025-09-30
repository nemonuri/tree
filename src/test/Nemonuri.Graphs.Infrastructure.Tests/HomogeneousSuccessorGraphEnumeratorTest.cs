using Nemonuri.Graphs.Infrastructure.TestDatas.IntNodes;

namespace Nemonuri.Graphs.Infrastructure.Tests;

public class HomogeneousSuccessorGraphEnumeratorTest
{
    [Theory]
    [MemberData(nameof(Entry1))]
    public void ToArray_ShouldStructurallyEqual
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
        var actual = enumerable.Select(static n => n.Value).ToArray();

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
}
