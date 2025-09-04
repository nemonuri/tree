using Nemonuri.Trees.TestDatas;

namespace Nemonuri.Trees.Paths.Tests;

public class TreeNavigaionTheoryTest
{
    [Theory]
    [MemberData(nameof(Data1))]
    public void ToEnumerable
    (
        Int32RoseNodeLabel roseNodeLabel,
        int[] expected
    )
    {
        // Arrange
        RoseNode<int> roseNode = TestDataTheory.Int32RoseNodeMap[roseNodeLabel];

        // Act
        var actual = TreeNavigaionTheory.ToEnumerable(roseNode);

        // Assert
        Assert.Equal(expected, actual);
    }
    public static TheoryData<Int32RoseNodeLabel, int[]> Data1 => new()
    {
        { Int32RoseNodeLabel.Single_0, [0] },
        { Int32RoseNodeLabel.Height1_EvenNumbersIn2To10, [6, 8, 10, 4, 2] },
        { Int32RoseNodeLabel.Height3_NumbersIn0To10, [1,3,5,4,6,2,7,9,10,8,0] }
    };
}
