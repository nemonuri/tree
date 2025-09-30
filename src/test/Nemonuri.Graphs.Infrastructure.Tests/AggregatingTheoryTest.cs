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
        ValueNull context = default;

        // Act
        var actual = AggregatingTheory.AggregateHomogeneousSuccessors
        <
            IntNodeAdder,
            ValueNull, ValueNull, ValueNull, int,
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


    [Theory]
    [MemberData(nameof(Entry2))]
    public void AggregateHomogeneousSuccessors_WhenAggeratorIsIntNodeStringfier
    (
        NodeLabel nodeLabel,
        string expected
    )
    {
        // Arrange
        IntNodeStringfier aggregator = new();
        IntNode node = TestDataTheory.IntNodeMap[nodeLabel];
        ValueNull context = default;

        // Act
        var actual = AggregatingTheory.AggregateHomogeneousSuccessors
        <
            IntNodeStringfier,
            ValueNull, ValueNull, ValueNull, string,
            IntNode, IntNodeArrow, IntNodeArrow, IntNodeOutArrowSet
        >
        (aggregator, ref context, node);

        // Assert
        Assert.Equal(expected, actual);
    }

    public static TheoryData<NodeLabel, string> Entry2 => new()
    {
        { NodeLabel.Single_0, "0" },
        { NodeLabel.Height1_EvenNumbersIn2To10, "2,6,8,10,4" },
        { NodeLabel.Height1_3Pow1To3Pow4, "9,3,27,81" },
        { NodeLabel.Height3_NumbersIn0To10, "0,1,2,3,4,5,6,7,8,9,10" }
    };


    [Theory]
    [MemberData(nameof(Entry3))]
    public void AggregateHomogeneousSuccessors_WhenAggeratorIsIntNodeParenthesizedStringfier1
    (
        NodeLabel nodeLabel,
        string expected
    )
    {
        // Arrange
        IntNodeParenthesizedStringfier1 aggregator = new();
        IntNode node = TestDataTheory.IntNodeMap[nodeLabel];
        ValueNull context = default;

        // Act
        var actual = AggregatingTheory.AggregateHomogeneousSuccessors
        <
            IntNodeParenthesizedStringfier1,
            ValueNull, ValueNull, ValueNull, string,
            IntNode, IndexedIntNodeArrow, IndexedIntNodeArrow, IndexedIntNodeOutArrowSet
        >
        (aggregator, ref context, node);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [MemberData(nameof(Entry3))]
    public void AggregateHomogeneousSuccessors_WhenAggeratorIsIntNodeParenthesizedStringfier2
    (
        NodeLabel nodeLabel,
        string expected
    )
    {
        // Arrange
        IntNodeParenthesizedStringfier2 aggregator = new();
        IntNode node = TestDataTheory.IntNodeMap[nodeLabel];
        ValueNull context = default;

        // Act
        var actual = AggregatingTheory.AggregateHomogeneousSuccessors
        <
            IntNodeParenthesizedStringfier2,
            ValueNull, int, ValueNull, string,
            IntNode, IntNodeArrow, IntNodeArrow, IntNodeOutArrowSet
        >
        (aggregator, ref context, node);

        // Assert
        Assert.Equal(expected, actual);
    }

    public static TheoryData<NodeLabel, string> Entry3 => new()
    {
        { NodeLabel.Single_0, "0" },
        { NodeLabel.Height1_EvenNumbersIn2To10, "(2 (6 8 10 4))" },
        { NodeLabel.Height1_3Pow1To3Pow4, "(9 (3 27 81))" },
        { NodeLabel.Height3_NumbersIn0To10, "(0 (1 (2 (3 (4 (5)) 6)) 7 (8 (9 10))))" }
    };
}

