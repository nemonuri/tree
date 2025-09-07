using Nemonuri.Trees.TestDatas;

namespace Nemonuri.Trees.Paths.Tests;

public class TreeNavigaionTheoryTest
{
    [Theory]
    [MemberData(nameof(Data1))]
    public void ToEnumerable
    (
        Int32TreeLabel treeLabel,
        int[] expected
    )
    {
        // Arrange
        var tree = TestDataTheory.Int32TreeMap[treeLabel];

        // Act
        var actual = TreeNavigaionTheory.ToEnumerable(tree);

        // Assert
        Assert.Equal(expected, actual);
    }
    public static TheoryData<Int32TreeLabel, int[]> Data1 => new()
    {
        { Int32TreeLabel.Single_0, [0] },
        { Int32TreeLabel.Height1_EvenNumbersIn2To10, [6, 8, 10, 4, 2] },
        { Int32TreeLabel.Height3_NumbersIn0To10, [1,3,5,4,6,2,7,9,10,8,0] }
    };
}
