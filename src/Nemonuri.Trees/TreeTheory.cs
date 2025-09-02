
namespace Nemonuri.Trees;

using Abstractions;

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
