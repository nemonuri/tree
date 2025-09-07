using Nemonuri.Trees.TestDatas;

namespace Nemonuri.Trees.Tests;

public class TreeTheoryTest
{
    [Theory]
    [MemberData(nameof(Data1))]
    public void All_WhenSourceIsInt32RoseNode
    (
        Int32TreeLabel treeLabel,
        Int32PredicateLabel predicateLabel,
        bool expected
    )
    {
        // Arrange
        var tree = TestDataTheory.Int32TreeMap[treeLabel];
        var predicate = TestDataTheory.Int32PredicateMap[predicateLabel];

        // Act
        bool actual = tree.All(predicate);

        // Assert
        Assert.Equal(expected, actual);
    }
    public static
    TheoryData<Int32TreeLabel, Int32PredicateLabel, bool> Data1 => new()
    {
        { Int32TreeLabel.Single_0, Int32PredicateLabel.IsEven, true },
        { Int32TreeLabel.Single_0, Int32PredicateLabel.IsOdd, false },
        { Int32TreeLabel.Height1_EvenNumbersIn2To10, Int32PredicateLabel.IsEven, true },
        { Int32TreeLabel.Height1_EvenNumbersIn2To10, Int32PredicateLabel.IsOdd, false },
        { Int32TreeLabel.Height1_3Pow1To3Pow4, Int32PredicateLabel.IsOdd, true },
        { Int32TreeLabel.Height1_3Pow1To3Pow4, Int32PredicateLabel.IsEven, false },
        { Int32TreeLabel.Height3_NumbersIn0To10, Int32PredicateLabel.IsEven, false },
        { Int32TreeLabel.Height3_NumbersIn0To10, Int32PredicateLabel.IsOdd, false }
    };

    [Theory]
    [MemberData(nameof(Data2))]
    public void Any_WhenSourceIsInt32RoseNode
    (
        Int32TreeLabel treeLabel,
        Int32PredicateLabel predicateLabel,
        bool expected
    )
    {
        // Arrange
        var tree = TestDataTheory.Int32TreeMap[treeLabel];
        var predicate = TestDataTheory.Int32PredicateMap[predicateLabel];

        // Act
        bool actual = tree.Any(predicate);

        // Assert
        Assert.Equal(expected, actual);
    }
    public static
    TheoryData<Int32TreeLabel, Int32PredicateLabel, bool> Data2 => new()
    {
        { Int32TreeLabel.Single_0, Int32PredicateLabel.IsEven, true },
        { Int32TreeLabel.Single_0, Int32PredicateLabel.IsOdd, false },
        { Int32TreeLabel.Height1_EvenNumbersIn2To10, Int32PredicateLabel.IsEven, true },
        { Int32TreeLabel.Height1_EvenNumbersIn2To10, Int32PredicateLabel.IsOdd, false },
        { Int32TreeLabel.Height1_3Pow1To3Pow4, Int32PredicateLabel.IsOdd, true },
        { Int32TreeLabel.Height1_3Pow1To3Pow4, Int32PredicateLabel.IsEven, false },
        { Int32TreeLabel.Height3_NumbersIn0To10, Int32PredicateLabel.IsEven, true },
        { Int32TreeLabel.Height3_NumbersIn0To10, Int32PredicateLabel.IsOdd, true },

        { Int32TreeLabel.Single_0, Int32PredicateLabel.IsZero, true },
        { Int32TreeLabel.Height1_EvenNumbersIn2To10, Int32PredicateLabel.IsZero, false },
        { Int32TreeLabel.Height1_3Pow1To3Pow4, Int32PredicateLabel.IsZero, false },
        { Int32TreeLabel.Height3_NumbersIn0To10, Int32PredicateLabel.IsZero, true }
    };

    [Theory]
    [MemberData(nameof(Data3))]
    public void ToArray_WhenSourceIsInt32RoseNode
    (
        Int32TreeLabel treeLabel,
        int[] expected
    )
    {
        // Arrange
        var tree = TestDataTheory.Int32TreeMap[treeLabel];

        // Act
        var actual = tree.ToArray();

        // Assert
        Assert.Equal(expected, actual);
    }
    public static TheoryData<Int32TreeLabel, int[]> Data3 => new()
    {
        { Int32TreeLabel.Single_0, [0] },
        { Int32TreeLabel.Height1_EvenNumbersIn2To10, [6,8,10,4,2] },
        { Int32TreeLabel.Height1_3Pow1To3Pow4, [3,27,81,9] },
        { Int32TreeLabel.Height3_NumbersIn0To10, [1,3,5,4,6,2,7,9,10,8,0] }
    };

    [Theory]
    [MemberData(nameof(Data4))]
    public void Select_WhenSourceIsInt32RoseNode
    (
        Int32TreeLabel treeLabel,
        Int32SelectorLabel selectorLabel,
        object[] expected
    )
    {
        // Arrange
        var roseNode = TestDataTheory.Int32TreeMap[treeLabel];
        var selector = TestDataTheory.Int32SelectorMap[selectorLabel];

        // Act
        var actual = roseNode.Select(selector).ToArray();

        // Assert
        Assert.Equal(expected, actual);

    }
    public static TheoryData<Int32TreeLabel, Int32SelectorLabel, object[]> Data4 => new()
    {
        { Int32TreeLabel.Single_0, Int32SelectorLabel.MultiplyTwo, [0] },
        { Int32TreeLabel.Height1_EvenNumbersIn2To10, Int32SelectorLabel.AddOne, [7,9,11,5,3] },
        { Int32TreeLabel.Height1_3Pow1To3Pow4, Int32SelectorLabel.ConvertToString, ["3","27","81","9"] },
        {
            Int32TreeLabel.Height3_NumbersIn0To10, Int32SelectorLabel.IsEven,
          //[1,     3,      5,      4,      6,      2,      7,      9,      10,     8,      0]
            [false, false,  false,  true,   true,   true,   false,  false,  true,   true,   true]
        }
    };
}
