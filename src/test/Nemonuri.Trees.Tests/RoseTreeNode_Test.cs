
namespace Nemonuri.Trees.Tests;

public class RoseTreeNode_Test
{
    [Theory]
    [InlineData(0, new int[] { 1, 2, 3, 5, 4 })]
    [InlineData(1, new int[] { })]
    [InlineData(-1, new int[] { 0, 1 })]
    [InlineData(10, new int[] { -1, 2, 3, 2345, 21, 0, -19, 54, 2345 })]
    public void Constructor_ValueAndChildrenShouldStructurallyEqualToSourceValueAndChildren
    (
        int nodeValue,
        int[] childrenValues
    )
    {
        // Arrange
        RoseTreeNodeChildrenProvider<int> childrenProvider = new();

        // Act
        RoseTreeNode<int> roseTreeNode = RoseTreeNodeTestTheory.CreateFromNodeValueAndChildrenValues(nodeValue, childrenValues);

        // Assert
        Assert.Equal(nodeValue, roseTreeNode.Value);
        Assert.Equal(childrenValues, roseTreeNode.Children.Select(a => a.Value));
        Assert.Equal(childrenValues, childrenProvider.GetChildren(roseTreeNode).Select(a => a.Value));
    }
}
