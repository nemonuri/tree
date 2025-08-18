
namespace Nemonuri.Trees.Tests;

using System.Linq;

public static class RoseTreeNodeAggregatorTheory
{
    public static AdHocRoseTreeNodeAggregator<T, bool>
    CreateForallPremiseUsingOptionalAggregator<T>
    (
        Func<T?, bool> predicate
    )
    {
        Assert.NotNull(predicate);

        AdHocRoseTreeNodeAggregator<T, bool> aggregator = new
        (
            defaultSeedProvider: static () => true,
            optionalAggregator: (context, childrenAggregated, siblingsAggregated, source) =>
            {
                if (!(childrenAggregated && siblingsAggregated)) { return (false, true); }
                if (!context.TryGetLastTreeNode(out var node)) { return (false, false); }
                return (predicate(node.Value), true);
            }
        );

        return aggregator;
    }
}