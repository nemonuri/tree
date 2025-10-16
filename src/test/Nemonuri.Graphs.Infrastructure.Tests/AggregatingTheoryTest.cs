using I = Nemonuri.Graphs.Infrastructure.TestDatas.IntNodes;
using S = Nemonuri.Graphs.Infrastructure.TestDatas.StringNodes;
using Nemonuri.Graphs.Infrastructure.TestDatas;
using System.Collections.Immutable;

namespace Nemonuri.Graphs.Infrastructure.Tests;

public class AggregatingTheoryTest
{
    [Theory]
    [MemberData(nameof(Entry1))]
    public void AggregateHomogeneousSuccessors_WhenAggeratorIsIntNodeAdder
    (
        I::NodeLabel nodeLabel,
        int expected
    )
    {
        // Arrange
        I::IntNodeAdder aggregator = new();
        I::IntNode node = I::TestDataTheory.IntNodeMap[nodeLabel];

        // Act
        var actual = AggregatingTheory.AggregateHomogeneousSuccessors
        <
            I::IntNode, I::IntNodeArrow, I::IntNodeArrow, I::IntNodeOutArrowSet, ValueNull, int,
            ValueNull, ValueNull, ValueNull, ValueNull,
            I::IntNodeAdder
        >
        (aggregator, node);

        // Assert
        Assert.Equal(expected, actual);
    }

    public static TheoryData<I::NodeLabel, int> Entry1 => new()
    {
        { I::NodeLabel.Single_0, 0 /* = 0 */ },
        { I::NodeLabel.Height1_EvenNumbersIn2To10, 30 /* = 2+6+8+10+4 */ },
        { I::NodeLabel.Height1_3Pow1To3Pow4, 120 /* = 9+3+27+81 */ },
        { I::NodeLabel.Height3_NumbersIn0To10, 55 /* = 0+1+2+3+4+5+6+7+8+9+10 */ }
    };


    [Theory]
    [MemberData(nameof(Entry2))]
    public void AggregateHomogeneousSuccessors_WhenAggeratorIsIntNodeStringfier
    (
        I::NodeLabel nodeLabel,
        string expected
    )
    {
        // Arrange
        I::IntNodeStringfier aggregator = new();
        I::IntNode node = I::TestDataTheory.IntNodeMap[nodeLabel];

        // Act
        var actual = AggregatingTheory.AggregateHomogeneousSuccessors
        <
            I::IntNode, I::IntNodeArrow, I::IntNodeArrow, I::IntNodeOutArrowSet, ValueNull, string,
            ValueNull, ValueNull, ValueNull, ValueNull,
            I::IntNodeStringfier
        >
        (aggregator, node);

        // Assert
        Assert.Equal(expected, actual);
    }

    public static TheoryData<I::NodeLabel, string> Entry2 => new()
    {
        { I::NodeLabel.Single_0, "0" },
        { I::NodeLabel.Height1_EvenNumbersIn2To10, "2,6,8,10,4" },
        { I::NodeLabel.Height1_3Pow1To3Pow4, "9,3,27,81" },
        { I::NodeLabel.Height3_NumbersIn0To10, "0,1,2,3,4,5,6,7,8,9,10" }
    };


    [Theory]
    [MemberData(nameof(Entry3))]
    public void AggregateHomogeneousSuccessors_WhenAggeratorIsIntNodeParenthesizedStringfier1
    (
        I::NodeLabel nodeLabel,
        string expected
    )
    {
        // Arrange
        I::IntNodeParenthesizedStringfier1 aggregator = new();
        I::IntNode node = I::TestDataTheory.IntNodeMap[nodeLabel];

        // Act
        var actual = AggregatingTheory.AggregateHomogeneousSuccessors
        <
            I::IntNode, I::IndexedIntNodeArrow, I::IndexedIntNodeArrow, I::IndexedIntNodeOutArrowSet, ValueNull, string,
            ValueNull, ValueNull, ValueNull, ValueNull, 
            I::IntNodeParenthesizedStringfier1
        >
        (aggregator, node);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [MemberData(nameof(Entry3))]
    public void AggregateHomogeneousSuccessors_WhenAggeratorIsIntNodeParenthesizedStringfier2
    (
        I::NodeLabel nodeLabel,
        string expected
    )
    {
        // Arrange
        I::IntNodeParenthesizedStringfier2 aggregator = new();
        I::IntNode node = I::TestDataTheory.IntNodeMap[nodeLabel];

        // Act
        var actual = AggregatingTheory.AggregateHomogeneousSuccessors
        <
            I::IntNode, I::IntNodeArrow, I::IntNodeArrow, I::IntNodeOutArrowSet, ValueNull, string,
            int, ValueNull, ValueNull, ValueNull,
            I::IntNodeParenthesizedStringfier2
        >
        (aggregator, node);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [MemberData(nameof(Entry3))]
    public void AggregateHomogeneousSuccessors_WhenAggeratorIsIntNodeParenthesizedStringfier3
    (
        I::NodeLabel nodeLabel,
        string expected
    )
    {
        // Arrange
        I::IntNodeParenthesizedStringfier3 aggregator = new();
        I::IntNode node = I::TestDataTheory.IntNodeMap[nodeLabel];

        // Act
        var actual = AggregatingTheory.AggregateHomogeneousSuccessors
        <
            I::IntNode, I::IntNodeArrow, I::IntNodeArrow, I::IntNodeOutArrowSet, ValueNull, string,
            I::IntNodeParenthesizedStringfier3.ChildrenScopeContext,
            ValueNull, ValueNull,
            I::IntNodeParenthesizedStringfier3.GraphScopeContext,
            I::IntNodeParenthesizedStringfier3
        >
        (aggregator, node);

        // Assert
        Assert.Equal(expected, actual);
    }

    public static TheoryData<I::NodeLabel, string> Entry3 => new()
    {
        { I::NodeLabel.Single_0, "0" },
        { I::NodeLabel.Height1_EvenNumbersIn2To10, "(2 (6 8 10 4))" },
        { I::NodeLabel.Height1_3Pow1To3Pow4, "(9 (3 27 81))" },
        { I::NodeLabel.Height3_NumbersIn0To10, "(0 (1 (2 (3 (4 (5)) 6)) 7 (8 (9 10))))" }
    };

    [Theory]
    [MemberData(nameof(Entry4))]
    public void AggregateHomogeneousSuccessors_WhenAggeratorIsIntNodeToStringNodeSelector
    (
        I::NodeLabel nodeLabel,
        S::NodeLabel expectedNodeLabel
    )
    {
        // Arrange
        I::IntNode node = I::TestDataTheory.IntNodeMap[nodeLabel];
        Func<int, string> selector = static i => i.ToString();
        S::IntNodeToStringNodeSelector aggregator = new(selector);

        AdHocSuccessorGraph<S::StringNode, S::StringNode, S::StringNodeArrow, S::StringNodeOutArrowSet> graph = new(static n => new(n));
        S::StringNode expectedNode = S::TestDataTheory.StringNodeMap[expectedNodeLabel];

        IEnumerable<(string Value, int Depth, int Index)> CreateEnumerable(S::StringNode stringNode)
        {
            EnumerableHomogeneousSuccessorGraph
            <
                AdHocSuccessorGraph<S::StringNode, S::StringNode, S::StringNodeArrow, S::StringNodeOutArrowSet>,
                S::StringNode, S::StringNodeArrow, S::StringNodeOutArrowSet
            > enumerable = new(graph, stringNode);

            return enumerable.Select(static pn => (pn.Node?.Value ?? "", pn.Depth, pn.Index));
        }

        // Act
        var actual = AggregatingTheory.AggregateHomogeneousSuccessors
        <
            I::IntNode, I::IntNodeArrow, I::IntNodeArrow, I::IntNodeOutArrowSet, ValueNull, ImmutableList<S::StringNode>,
            ValueNull, ValueNull, ValueNull, ValueNull, 
            S::IntNodeToStringNodeSelector
        >
        (aggregator, node);

        // Assert
        Assert.Single(actual);
        Assert.Equal(CreateEnumerable(expectedNode), CreateEnumerable(actual[0]));
    }

    public static TheoryData<I::NodeLabel, S::NodeLabel> Entry4 => new()
    {
        { I::NodeLabel.Single_0, S::NodeLabel.Single_0 },
        { I::NodeLabel.Height1_EvenNumbersIn2To10, S::NodeLabel.Height1_EvenNumbersIn2To10 },
        { I::NodeLabel.Height1_3Pow1To3Pow4, S::NodeLabel.Height1_3Pow1To3Pow4 },
        { I::NodeLabel.Height3_NumbersIn0To10, S::NodeLabel.Height3_NumbersIn0To10 }
    };
}
