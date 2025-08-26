
namespace Nemonuri.Trees.Tests;

public class WalkingTheory_Test
{
    public static TheoryData<RoseTreeNode<int>, bool> ForallData => new()
    {
        { RoseTreeNodeTestTheory.CreateFromNodeValueAndChildrenValues(1, [1, 2, 3]), true},
        { RoseTreeNodeTestTheory.CreateFromNodeValueAndChildrenValues(2, [0, 4, 9, 11]), true},
        { RoseTreeNodeTestTheory.CreateFromNodeValueAndChildrenValues(-1, [3, 9, 2]), false},
        { RoseTreeNodeTestTheory.CreateFromNodeValueAndChildrenValues(2, [0, 4, -9, 11]), false},
        {
            new (1, [new(1), new RoseTreeNode<int>(3).WithChildrenValues([6, 11, 8]), new(0)]),
            true
        },
        {
            N(0).WithChildren
            (
                N(1).WithChildren
                (
                    N(4),
                    N(5)
                ),
                N(2).WithChildrenValues
                (
                    7,
                    -1,
                    8
                ),
                N(3)
            ),
            false
        }
    };

    private static RoseTreeNode<int> N(int value) => new(value);
    
    [Theory]
    [MemberData(nameof(ForallData))]
    public void TryWalkAsRoot_WhenBaseOnCreateForallPremiseUsingOptionalAggregator
    (
        RoseTreeNode<int> roseTreeNode, bool expected
    )
    {
        // Arrange
        AdHocRoseTreeNodeAggregator<int, bool> aggregatingPremise =
            RoseTreeNodeAggregatorTheory.CreateForallPremiseUsingOptionalAggregator<int>
            (
                static i => i >= 0
            );

        // Act
        bool success = TreeNodeAggregatingTheory.TryAggregateAsRoot
        (
            contextFromRootAggregator: new IndexedRoseTreeNodesFromRootAggregator<int>(),
            treeNodeAggregator: aggregatingPremise,
            childrenProvider: new RoseTreeNodeChildrenProvider<int>(),
            treeNode: roseTreeNode,
            out bool aggregated
        );

        // Assert
        Assert.True(success);
        Assert.Equal(expected, aggregated);
    }
}