namespace Nemonuri.Trees.Tests;

using TestDatas;

public class TreeTheoryTest
{
    [Theory]
    [MemberData(nameof(Data1))]
    public void All_WhenSourceIsInt32RoseNode
    (
        Int32RoseNodeLabel roseNodeLabel,
        Int32PredicateLabel predicateLabel,
        bool expected
    )
    {
        // Arrange
        var roseNode = TestDataTheory.Int32RoseNodeMap[roseNodeLabel];
        var predicate = TestDataTheory.Int32PredicateMap[predicateLabel];

        // Act
        bool actual = roseNode.All(predicate);

        // Assert
        Assert.Equal(expected, actual);
    }
    public static
    TheoryData<Int32RoseNodeLabel, Int32PredicateLabel, bool> Data1 => new()
    {
        { Int32RoseNodeLabel.Single_0, Int32PredicateLabel.IsEven, true },
        { Int32RoseNodeLabel.Single_0, Int32PredicateLabel.IsOdd, false },
        { Int32RoseNodeLabel.Height1_EvenNumbersIn2To10, Int32PredicateLabel.IsEven, true },
        { Int32RoseNodeLabel.Height1_EvenNumbersIn2To10, Int32PredicateLabel.IsOdd, false },
        { Int32RoseNodeLabel.Height1_3Pow1To3Pow4, Int32PredicateLabel.IsOdd, true },
        { Int32RoseNodeLabel.Height1_3Pow1To3Pow4, Int32PredicateLabel.IsEven, false },
        { Int32RoseNodeLabel.Height3_NumbersIn0To10, Int32PredicateLabel.IsEven, false },
        { Int32RoseNodeLabel.Height3_NumbersIn0To10, Int32PredicateLabel.IsOdd, false }
    };

}
