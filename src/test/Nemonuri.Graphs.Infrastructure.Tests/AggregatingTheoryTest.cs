using Nemonuri.Graphs.Infrastructure.TestDatas.IntNodes;

namespace Nemonuri.Graphs.Infrastructure.Tests;

public class AggregatingTheoryTest
{
    [Theory]
    [MemberData(nameof(Entry1))]
    public void AggregateHomogeneousSuccessors_WhenAggeratorIsIntNodeAdder
    (
        NodeLabel nodeLabel,
        int expected
    )
    {
        // Arrange
        IntNodeAdder aggregator = new();
        IntNode node = TestDataTheory.IntNodeMap[nodeLabel];
        NullValue context = default;

        // Act
        var actual = AggregatingTheory.AggregateHomogeneousSuccessors
        <
            IntNodeAdder,
            NullValue, NullValue, int,
            IntNode, IntNodeArrow, IntNodeArrow, IntNodeOutArrowSet
        >
        (aggregator, ref context, node);

        // Assert
        Assert.Equal(expected, actual);
    }

    public static TheoryData<NodeLabel, int> Entry1 => new()
    {
        { NodeLabel.Single_0, 0 /* = 0 */ },
        { NodeLabel.Height1_EvenNumbersIn2To10, 30 /* = 2+6+8+10+4 */ },
        { NodeLabel.Height1_3Pow1To3Pow4, 120 /* = 9+3+27+81 */ },
        { NodeLabel.Height3_NumbersIn0To10, 55 /* = 0+1+2+3+4+5+6+7+8+9+10 */ }
    };
}

