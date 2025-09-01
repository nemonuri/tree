using Nemonuri.Trees.Abstractions;

namespace Nemonuri.Trees;

public static partial class TreeTheory
{
    public static ITree<TElement> ToTree<TElement>(TElement root)
        where TElement : ISupportChildren<TElement>
    {
        if (root is ITree<TElement> tree) { return tree; }

        return new Tree<TElement>(root, (TrivialChildrenProvider<TElement>)default);
    }

    public static ITree<IRoseNode<TElement>> ToTree<TElement>(this IRoseNode<TElement> roseNode)
    {
        return ToTree<IRoseNode<TElement>>(roseNode);
    }

    /// <inheritdoc 
    ///     cref="AggregatingTheory.Aggregate{_,_,_,_,_,_,_}(_,_,_,_)" 
    ///     path="/*[not(self::param)]"/>
    public static TAggregation Aggregate
    <TElement, TAggregation, TAncestor, TAncestorsAggregation>
    (
        this ITree<TElement> tree,
        ITreeAggregator<TElement, TAggregation, TAncestor, TAncestorsAggregation> treeWalker
    )
#if NET9_0_OR_GREATER
        where TElement : allows ref struct
        where TAggregation : allows ref struct
        where TAncestor : allows ref struct
        where TAncestorsAggregation : allows ref struct
#endif
    {
        Debug.Assert(tree is not null);

        return AggregatingTheory.Aggregate(treeWalker, tree.ChildrenProvider, treeWalker, tree.Root);
    }

    public static IEnumerable<ITree<TElement>> GetChildren<TElement>(this ITree<TElement> tree)
    {
        Guard.IsNotNull(tree);

        if (tree is ISupportChildren<ITree<TElement>> supportChildren)
        {
            return supportChildren.Children;
        }

        return tree.GetChildrenCore();
    }

    internal static IEnumerable<ITree<TElement>> GetChildrenCore<TElement>(this ITree<TElement> tree)
    {
        return tree.ChildrenProvider.GetChildren(tree.Root).Select
        (
            a => new Tree<TElement>(a, tree.ChildrenProvider, tree)
        );
    }

    public static bool All<TElement>
    (
        this ITree<TElement> tree,
        Func<TElement, bool> predicate
    )
    {
        Guard.IsNotNull(tree);
        Guard.IsNotNull(predicate);

        var treeAggregator = TreeAggregatorTheory.Create<TElement, bool>
        (
            initialAggregationImplementation: static () => true,
            aggregateImplementation: (s, c, e) =>
            {
                if ((s || c) is false) { return false; }
                return predicate(e);
            }
        );

        return tree.Aggregate(treeAggregator);
    }

    public static bool All<TElement>
    (
        this IRoseNode<TElement> roseNode,
        Func<TElement, bool> predicate
    )
    {
        return roseNode.ToTree().All(PredicateImpl);

        bool PredicateImpl(IRoseNode<TElement> roseNode) => predicate(roseNode.Value);
    }

    public static bool Any<TElement>
    (
        this ITree<TElement> tree,
        Func<TElement, bool> predicate
    )
    {
        Guard.IsNotNull(tree);
        Guard.IsNotNull(predicate);

        var treeAggregator = TreeAggregatorTheory.Create<TElement, bool>
        (
            initialAggregationImplementation: static () => false,
            aggregateImplementation: (s, c, e) =>
            {
                if ((s || c) is true) { return true; }
                return predicate(e);
            }
        );

        return tree.Aggregate(treeAggregator);
    }

    public static bool Any<TElement>
    (
        this IRoseNode<TElement> roseNode,
        Func<TElement, bool> predicate
    )
    {
        return roseNode.ToTree().Any(PredicateImpl);
        
        bool PredicateImpl(IRoseNode<TElement> roseNode) => predicate(roseNode.Value);
    }


    public static IRoseNode<TResult> Select<TSource, TResult>
    (
        this ITree<TSource> tree,
        Func<TSource, TResult> selector
    )
    {
        Guard.IsNotNull(tree);
        Guard.IsNotNull(selector);

        var treeAggregator = TreeAggregatorTheory.Create<TSource, ImmutableList<IRoseNode<TResult>>>
        (
            initialAggregationImplementation: static () => [],
            aggregateImplementation: (s, c, e) =>
            {
                TResult r = selector(e);
                IRoseNode<TResult> roseNode = new RoseNode<TResult>(r, c);
                return s.Add(roseNode);
            }
        );

        ImmutableList<IRoseNode<TResult>> aggregation = tree.Aggregate(treeAggregator);

        Guard.IsEqualTo(aggregation.Count, 1);
        return aggregation[0];
    }

    


#if REF_TREE
    public static IEnumerable<TTree> GetChildren
    <
        TTree,
        TElement, TAggregation, TAncestor, TAncestorsAggregation
    >
    (
        this TTree tree,
        Func<TElement, ITreeWalker<TElement, TAggregation, TAncestor, TAncestorsAggregation>, TTree> treeFactory
    )
        where TTree : ITree<TElement, TAggregation, TAncestor, TAncestorsAggregation>
#if NET9_0_OR_GREATER
                        , allows ref struct
        where TElement : allows ref struct
        where TAggregation : allows ref struct
        where TAncestor : allows ref struct
        where TAncestorsAggregation : allows ref struct
#endif
    {
        Debug.Assert(treeFactory is not null);

        return new ResultOfGetChildren<TTree, TElement, TAggregation, TAncestor, TAncestorsAggregation>
                    (tree.TreeWalker.GetChildren(tree.Root), tree.TreeWalker, treeFactory);
    }
#endif
}
