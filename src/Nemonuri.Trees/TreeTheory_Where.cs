using static Nemonuri.Trees.FunctionTheory;

namespace Nemonuri.Trees;

public static partial class TreeTheory
{
    public static ImmutableList<IGeneralBottomUpRoseTree<TResultValue>> Where<TSourceTree, TResultValue>
    (
        this ITree<TSourceTree> tree,
        Func<TResultValue, bool> predicate,
        Func<TSourceTree, TResultValue> selector
    )
        where TSourceTree : ITree<TSourceTree>
    {
        Guard.IsNotNull(tree);
        Guard.IsNotNull(predicate);

        var treeAggregator = TreeAggregatorTheory.Create<TSourceTree, ImmutableList<IGeneralBottomUpRoseTree<TResultValue>>>
        (
            initialAggregationImplementation: static () => [],
            aggregateImplementation: (s, c, e) =>
            {
                TResultValue r = selector(e);
                if (predicate(r))
                {
                    var newTree = CreateBranch(r, c);
                    return s.Add(newTree);
                }
                else
                {
                    return s.AddRange(c);
                }
            }
        );

        return tree.Aggregate(treeAggregator);
    }

    public static ImmutableList<IGeneralBottomUpRoseTree<TValue>> Where<TValue, TTree>
    (
        this IRoseTree<TValue, TTree> tree,
        Func<TValue, bool> predicate
    )
        where TTree : IRoseTree<TValue, TTree>
    {
        return tree.Where<TTree, TValue>(predicate, static t => t.Value);
    }

    public static ImmutableList<IGeneralBottomUpRoseTree<TResultValue>> Where<TSourceValue, TSourceTree, TResultValue>
    (
        this IRoseTree<TSourceValue, TSourceTree> tree,
        Func<TResultValue, bool> predicate,
        Func<TSourceValue, TResultValue> selector
    )
        where TSourceTree : IRoseTree<TSourceValue, TSourceTree>
    {
        return tree.Where(predicate, WarpParameterInTree<TSourceTree, TSourceValue, TResultValue>(selector));
    }
}
