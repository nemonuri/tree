
namespace Nemonuri.Trees;

/// <summary>The default implementation of <see cref="ITree{_,_,_,_}"/></summary>
/// <inheritdoc cref="ITree{_,_,_,_}" path="/typeparam"/>
public class Tree<TElement, TAggregation, TAncestor, TAncestorsAggregation> :
    ITree<TElement, TAggregation, TAncestor, TAncestorsAggregation>,
    IEquatable<Tree<TElement, TAggregation, TAncestor, TAncestorsAggregation>>,
    ISupportChildren<ITree<TElement, TAggregation, TAncestor, TAncestorsAggregation>>,
    ISupportParent<ITree<TElement, TAggregation, TAncestor, TAncestorsAggregation>>
{
    private IEnumerable<ITree<TElement, TAggregation, TAncestor, TAncestorsAggregation>>? _children;
    private readonly ITree<TElement, TAggregation, TAncestor, TAncestorsAggregation>? _parent;

    public Tree
    (
        TElement root,
        ITreeWalker<TElement, TAggregation, TAncestor, TAncestorsAggregation> treeWalker,
        ITree<TElement, TAggregation, TAncestor, TAncestorsAggregation>? parent
    )
    {
        Guard.IsNotNull(root);
        Guard.IsNotNull(treeWalker);

        Root = root;
        TreeWalker = treeWalker;
        _parent = parent;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Tree{_,_,_,_}"/> class.
    /// </summary>
    /// <param name="root"></param>
    /// <param name="treeWalker"></param>
    public Tree(TElement root, ITreeWalker<TElement, TAggregation, TAncestor, TAncestorsAggregation> treeWalker) :
        this(root, treeWalker, default)
    { }

    /// <inheritdoc cref="ITree{_,_,_,_}.Root" />
    public TElement Root { get; }

    /// <inheritdoc cref="ITree{_,_,_,_}.TreeWalker" />
    public ITreeWalker<TElement, TAggregation, TAncestor, TAncestorsAggregation> TreeWalker { get; }

    /// <inheritdoc cref="IEquatable{_}.Equals(_)" />
    public bool Equals(Tree<TElement, TAggregation, TAncestor, TAncestorsAggregation>? other)
    {
        if (other is null) { return false; }

        return
            EqualityComparer<TElement>.Default.Equals(Root, other.Root) &&
            TreeWalker.Equals(other.TreeWalker);
    }

    /// <inheritdoc cref="Object.Equals(object?)" />
    public override bool Equals(object? obj)
    {
        if (obj is not Tree<TElement, TAggregation, TAncestor, TAncestorsAggregation> tree)
        {
            return false;
        }

        return this.Equals(tree);
    }

    /// <summary>
    /// Gets the hash code of this <see cref="Tree{_,_,_,_}"/> instance.
    /// </summary>
    /// <inheritdoc cref="int.GetHashCode()" path="/returns"/>
    public override int GetHashCode()
    {
        return HashCode.Combine(Root, TreeWalker);
    }

    /// <inheritdoc cref="ISupportChildren{_}.Children" />
    public IEnumerable<ITree<TElement, TAggregation, TAncestor, TAncestorsAggregation>> Children =>
        _children ??= TreeWalker.GetChildren(Root).Select
        (
            a => new Tree<TElement, TAggregation, TAncestor, TAncestorsAggregation>
            (
                a, TreeWalker, this
            )
        );

    /// <inheritdoc cref="ISupportParent{_}.TryGetParent(out _?)" />
    public bool TryGetParent([NotNullWhen(true)] out ITree<TElement, TAggregation, TAncestor, TAncestorsAggregation>? parent)
    {
        parent = _parent;
        return parent is not null;
    }
}
